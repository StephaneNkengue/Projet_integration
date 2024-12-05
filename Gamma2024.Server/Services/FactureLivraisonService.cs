using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Gamma2024.Server.Services
{
    public class FactureLivraisonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;

        public FactureLivraisonService(ApplicationDbContext context, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _context = context;
            _environment = environment;
            _emailSender = emailSender;
        }

        public FactureLivraisonVM GenererFactureLivraison(Facture facture)
        {
            var factureExistant = _context.FactureLivraisons.FirstOrDefault(fl => fl.IdFacture == facture.Id);
            if (factureExistant == null)
            {
                var factureLivraisonOriginal = new FactureLivraison
                {
                    Id = facture.Id,
                    Facture = facture,
                    IdCharite = 1
                };
                factureLivraisonOriginal.CalculerFacture();

                var factureLivraison = new FactureLivraisonVM
                {
                    IdFacture = factureLivraisonOriginal.IdFacture,
                    Facture = facture,
                    SousTotal = factureLivraisonOriginal.SousTotal,
                    PrixFinal = factureLivraisonOriginal.PrixFinal,
                    TPS = factureLivraisonOriginal.TPS,
                    TVQ = factureLivraisonOriginal.TVQ,
                    Don = factureLivraisonOriginal.Don.Value,
                    PrixFinalSansDon = double.Round(factureLivraisonOriginal.PrixFinal - factureLivraisonOriginal.Don.Value, 2),
                    Charites = _context.Charites.Select(c => new ChariteVM
                    {
                        Id = c.Id,
                        NomOrganisme = c.NomOrganisme,
                    }).ToList()
                };
                return factureLivraison;
            }
            return null;
        }

        public async Task<int> AjouterFactureLivraison(FactureLivraisonAjoutVM choix)
        {
            var factureLivraison = new FactureLivraison();
            AdresseVM adresseTemp = new();
            if (choix.IdAdresse == null)
            {
                var facture = _context.Factures.FirstOrDefault(f => f.Id == choix.IdFacture);
                facture.ChoixLivraison = false;
                _context.Factures.Update(facture);
                _context.SaveChanges();

                return 0;
            }
            else if (choix.IdAdresse == 0)
            {
                factureLivraison = new FactureLivraison
                {
                    IdFacture = choix.IdFacture,
                    Facture = _context.Factures.Include(f => f.Lots).Include(f => f.Client.Adresses).First(f => f.Id == choix.IdFacture),
                    IdCharite = choix.IdCharite,
                    DateAchat = DateTime.Now,
                    FacturePDFPath = ""
                };
                adresseTemp = choix.Adresse;
            }
            else
            {
                factureLivraison = new FactureLivraison
                {
                    IdFacture = choix.IdFacture,
                    Facture = _context.Factures.Include(f => f.Lots).Include(f => f.Client.Adresses).First(f => f.Id == choix.IdFacture),
                    IdAdresse = choix.IdAdresse.Value,
                    IdCharite = choix.IdCharite,
                    DateAchat = DateTime.Now,
                    FacturePDFPath = ""
                };
            }

            _context.FactureLivraisons.Add(factureLivraison);
            _context.SaveChanges();

            factureLivraison.CalculerFacture();

            _context.FactureLivraisons.Update(factureLivraison);
            _context.SaveChanges();

            factureLivraison.Facture.ChoixLivraison = true;
            _context.Factures.Update(factureLivraison.Facture);
            _context.SaveChanges();

            ChargerFactureLivraison(factureLivraison, choix.PmId);

            return factureLivraison.Id;
        }

        public FactureLivraison ChercherFactureLivraison(int idFactureLivraison)
        {
            var facture = _context.FactureLivraisons.Include(f => f.Facture.Client).FirstOrDefault(f => f.Id == idFactureLivraison);
            return facture;
        }

        public bool PayerFactureLivraison(int idFactureLivraison)
        {
            var factureLivraison = _context.FactureLivraisons.FirstOrDefault(f => f.Id == idFactureLivraison);

            if (factureLivraison == null)
            {
                return false;
            }

            factureLivraison.EstPaye = true;
            _context.FactureLivraisons.Update(factureLivraison);
            _context.SaveChanges();
            return true;
        }

        private string ChargerFactureLivraison(FactureLivraison factureLivraison, string pmId)
        {
            var productService = new ProductService();
            var invoiceService = new InvoiceService();
            var invoiceItemService = new InvoiceItemService();

            if (!factureLivraison.EstPaye)
            {
                var customerId = factureLivraison.Facture.Client.StripeCustomer;

                var invoiceOptions = new InvoiceCreateOptions
                {
                    Customer = customerId,
                    CollectionMethod = "charge_automatically",
                    DefaultPaymentMethod = pmId,
                    AutomaticTax = new InvoiceAutomaticTaxOptions(),
                };

                var invoice = invoiceService.Create(invoiceOptions);

                var fraisProdOptions = new ProductCreateOptions
                {
                    Name = "Frais de livraison",
                    DefaultPriceData = new ProductDefaultPriceDataOptions
                    {
                        UnitAmount = (long)(factureLivraison.SousTotal * 100),
                        Currency = "cad",
                    },
                    Expand = new List<string> { "default_price" },
                };
                var fraisProd = productService.Create(fraisProdOptions);

                var fraisInvoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = customerId,
                    Invoice = invoice.Id,
                    Description = "Frais de livraison",
                    Price = fraisProd.DefaultPriceId
                };

                invoiceItemService.Create(fraisInvoiceItemOptions);

                var tpsProdOptions = new ProductCreateOptions
                {
                    Name = "TPS",
                    DefaultPriceData = new ProductDefaultPriceDataOptions
                    {
                        UnitAmount = (long)(factureLivraison.TPS * 100),
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
                        UnitAmount = (long)(factureLivraison.TVQ * 100),
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

                if (factureLivraison.Don.HasValue)
                {
                    var donProdOptions = new ProductCreateOptions
                    {
                        Name = "Don",
                        DefaultPriceData = new ProductDefaultPriceDataOptions
                        {
                            UnitAmount = (long)(factureLivraison.Don * 100),
                            Currency = "cad",
                        },
                        Expand = new List<string> { "default_price" },
                    };
                    var donProd = productService.Create(donProdOptions);

                    var donInvoiceItemOptions = new InvoiceItemCreateOptions
                    {
                        Customer = customerId,
                        Invoice = invoice.Id,
                        Description = "Don",
                        Price = donProd.DefaultPriceId
                    };

                    invoiceItemService.Create(donInvoiceItemOptions);
                }

                invoiceService.Pay(invoice.Id);
                PayerFactureLivraison(factureLivraison.Id);

                return invoice.Id;
            }
            return "";
        }
    }
}
