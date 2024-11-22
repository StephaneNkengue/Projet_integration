using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Gamma2024.Server.Services
{
    public class FactureService
    {
        private readonly ApplicationDbContext _context;

        public FactureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageVM()
            {
                Id = f.Id,
                IdClient = f.IdClient,
                DateAchat = f.DateAchat,
                Lots = f.Lots,
                Encan = f.NumeroEncan,
                PdfPath = f.FacturePDFPath
            }).ToList();

            foreach (FactureAffichageVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
                facture.Pseudonyme = client.UserName;
                facture.Courriel = client.Email;
                facture.Telephone = client.PhoneNumber;
            }

            return factures;
        }

        public ICollection<Facture> ChercherFacturesParEncan(int numeroEncan)
        {
            var factures = _context.Factures.Include(f => f.Client).Include(f => f.Lots).Where(f => f.NumeroEncan == numeroEncan);
            return factures.ToList();
        }

        public ICollection<FactureAffichageMembreVM> ChercherFacturesMembre(string id)
        {
            var factures = _context.Factures.Where(c => c.IdClient == id).Select(f => new FactureAffichageMembreVM()
            {
                Id = f.Id,
                IdClient = id,
                DateAchat = f.DateAchat,
                Lots = f.Lots,
                Encan = f.NumeroEncan,
                PdfPath = f.FacturePDFPath
            }).ToList();

            return factures;
        }

        public ICollection<Facture> CreerFacturesParEncan(int numeroEncan)
        {
            var idEncan = _context.Encans.First(e => e.NumeroEncan == numeroEncan).Id;
            var lots = _context.EncanLots
                .Include(el => el.Lot)
                .Include(el => el.Lot.ClientMise.Adresses)
                .Where(el => el.IdEncan == idEncan)
                .Select(el => el.Lot).ToList();
            var factures = _context.Factures.Include(f => f.Lots).Where(f => f.NumeroEncan == numeroEncan).ToList();

            foreach (var lot in lots)
            {
                if (lot.EstVendu && lot.IdFacture == null)
                {
                    var facture = factures.FirstOrDefault(f => f.IdClient == lot.IdClientMise);

                    if (facture != null && !facture.estPaye)
                    {
                        facture.Lots.Add(lot);
                    }
                    else
                    {
                        factures.Add(new Facture
                        {
                            IdClient = lot.IdClientMise,
                            DateAchat = lot.DateFinVente.Value,
                            Lots = [lot],
                            Client = lot.ClientMise,
                            NumeroEncan = numeroEncan,
                            FacturePDFPath = ""
                        });
                    }
                }
            }

            factures.ForEach(f => f.CalculerFacture());
            _context.Factures.UpdateRange(factures);
            _context.SaveChanges();

            return factures;
        }

        public string ChargerFacture(Facture facture)
        {
            var productService = new ProductService();
            var invoiceService = new InvoiceService();
            var invoiceItemService = new InvoiceItemService();
            var paymentMethodService = new PaymentMethodService();
            var chargeService = new ChargeService();

            if (!facture.estPaye)
            {
                var customerId = facture.Client.StripeCustomer;
                var paymentMethodOptions = new PaymentMethodListOptions
                {
                    Type = "card",
                    Limit = 1,
                    Customer = customerId,
                };

                StripeList<PaymentMethod> paymentMethods = paymentMethodService.List(paymentMethodOptions);

                var invoiceOptions = new InvoiceCreateOptions
                {
                    Customer = customerId,
                    CollectionMethod = "charge_automatically",
                    DefaultPaymentMethod = paymentMethods.Data[0].Id,
                    AutomaticTax = new InvoiceAutomaticTaxOptions(),
                };

                var invoice = invoiceService.Create(invoiceOptions);

                foreach (var lot in facture.Lots)
                {
                    var productOptions = new ProductCreateOptions
                    {
                        Name = lot.Description,
                        DefaultPriceData = new ProductDefaultPriceDataOptions
                        {
                            UnitAmount = (long)(lot.Mise.Value * 100),
                            Currency = "cad",
                        },
                        Expand = new List<string> { "default_price" },
                    };
                    var product = productService.Create(productOptions);

                    var invoiceItemOptions = new InvoiceItemCreateOptions
                    {
                        Customer = customerId,
                        Invoice = invoice.Id,
                        Description = lot.Description,
                        Price = product.DefaultPriceId
                    };

                    invoiceItemService.Create(invoiceItemOptions);
                }

                var fraisProdOptions = new ProductCreateOptions
                {
                    Name = "Frais d'encanteur",
                    DefaultPriceData = new ProductDefaultPriceDataOptions
                    {
                        UnitAmount = (long)(facture.FraisEncanteur * 100),
                        Currency = "cad",
                    },
                    Expand = new List<string> { "default_price" },
                };
                var fraisProd = productService.Create(fraisProdOptions);

                var fraisInvoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = customerId,
                    Invoice = invoice.Id,
                    Description = "Frais d'encanteur",
                    Price = fraisProd.DefaultPriceId
                };

                invoiceItemService.Create(fraisInvoiceItemOptions);

                var tpsProdOptions = new ProductCreateOptions
                {
                    Name = "TPS",
                    DefaultPriceData = new ProductDefaultPriceDataOptions
                    {
                        UnitAmount = (long)(facture.TPS * 100),
                        Currency = "cad",
                    },
                    Expand = new List<string> { "default_price" },
                };
                var tpsProd = productService.Create(tpsProdOptions);

                var tpsInvoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = customerId,
                    Invoice = invoice.Id,
                    Description = "TPS",
                    Price = tpsProd.DefaultPriceId
                };

                invoiceItemService.Create(tpsInvoiceItemOptions);

                var tvqProdOptions = new ProductCreateOptions
                {
                    Name = "TVQ",
                    DefaultPriceData = new ProductDefaultPriceDataOptions
                    {
                        UnitAmount = (long)(facture.TVQ * 100),
                        Currency = "cad",
                    },
                    Expand = new List<string> { "default_price" },
                };
                var tvqProd = productService.Create(tvqProdOptions);

                var tvqInvoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = customerId,
                    Invoice = invoice.Id,
                    Description = "TVQ",
                    Price = tvqProd.DefaultPriceId
                };

                invoiceItemService.Create(tvqInvoiceItemOptions);

                var invoicePaye = invoiceService.Pay(invoice.Id);

                var charge = chargeService.Get(invoicePaye.ChargeId);

                PayerFacture(facture.Id);

                return charge.ReceiptUrl;
            }

            return "";
        }

        public bool PayerFacture(int idFacture)
        {
            var facture = _context.Factures.FirstOrDefault(f => f.Id == idFacture);

            if (facture == null)
            {
                return false;
            }

            facture.estPaye = true;
            _context.SaveChanges();
            return true;
        }

        public bool SauvegarderPDFFacture(int idFacture, string pathFacture)
        {
            var facture = _context.Factures.FirstOrDefault(f => f.Id == idFacture);
            if (facture == null)
            {
                return false;
            }
            facture.FacturePDFPath = pathFacture;
            _context.Factures.Update(facture);
            _context.SaveChanges();
            return true;
        }

        public Facture ChercherFacture(int idFacture)
        {
            var facture = _context.Factures.Include(f => f.Lots).FirstOrDefault(f => f.Id == idFacture);

            if (facture == null)
            {
                return null;
            }
            return facture;
        }
    }
}
