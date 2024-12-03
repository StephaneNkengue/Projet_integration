using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Stripe;

namespace Gamma2024.Server.Services
{
    public class FactureService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJsReportMVCService _jsReportService;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<InvoiceSettings> _invoiceSettings;

        public FactureService(ApplicationDbContext context, IJsReportMVCService jsReportService, IWebHostEnvironment environment,
            IEmailSender emailSender, IOptions<InvoiceSettings> invoiceSettings)
        {
            _context = context;
            _jsReportService = jsReportService;
            _environment = environment;
            _emailSender = emailSender;
            _invoiceSettings = invoiceSettings;
        }

        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            var factures = _context.Factures.Include(f => f.FactureLivraison).Include(f => f.Lots).AsEnumerable().Select(f =>
            {
                var factureAffichage = new FactureAffichageVM()
                {
                    Id = f.Id,
                    IdClient = f.IdClient,
                    DateAchat = f.DateAchat,
                    Lots = f.Lots,
                    Encan = f.NumeroEncan,
                    PdfPath = f.FacturePDFPath,
                    PdfPathLivraison = "",
                    Livraison = f.ChoixLivraison
                };
                if (f.IdFactureLivraison.HasValue)
                {
                    factureAffichage.PdfPathLivraison = f.FactureLivraison.FacturePDFPath;
                }
                return factureAffichage;
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

            return factures.OrderByDescending(f => f.Encan).ToList();
        }

        public ICollection<Facture> ChercherFacturesParEncan(int numeroEncan)
        {
            var factures = _context.Factures.Include(f => f.Client).Include(f => f.Lots).Where(f => f.NumeroEncan == numeroEncan);
            return factures.ToList();
        }

        public ICollection<FactureAffichageMembreVM> ChercherFacturesMembre(string id)
        {
            var factures = _context.Factures.Include(f => f.Lots).Include(f => f.FactureLivraison).Where(c => c.IdClient == id).AsEnumerable().Select(f =>
            {
                var factureAffichage = new FactureAffichageMembreVM()
                {
                    Id = f.Id,
                    IdClient = id,
                    DateAchat = f.DateAchat,
                    Lots = f.Lots,
                    Encan = f.NumeroEncan,
                    PdfPath = f.FacturePDFPath,
                    PdfPathLivraison = "",
                    Livraison = f.ChoixLivraison
                };
                if (f.IdFactureLivraison.HasValue)
                {
                    factureAffichage.PdfPathLivraison = f.FactureLivraison.FacturePDFPath;
                }
                return factureAffichage;
            }
            ).ToList();

            return factures;
        }

        public async Task<ICollection<Facture>> CreerFacturesParEncan(int numeroEncan)
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

            foreach (var f in factures)
            {
                if (!f.estPaye)
                {
                    ChargerFacture(f);
                }

                if (f.FacturePDFPath.IsNullOrEmpty())
                {
                    var factureGenerer = new FactureGenererVM
                    {
                        DateAchat = f.DateAchat,
                        FraisEncanteur = f.FraisEncanteur,
                        NumeroEncan = numeroEncan,
                        Id = f.Id,
                        TPS = f.TPS,
                        TVQ = f.TVQ,
                        Lots = f.Lots.Select(l => new LotFactureVM
                        {
                            Description = l.Description,
                            Prix = l.Mise.Value.ToString(),
                        }).ToList(),
                        Client = new ClientFactureVM
                        {
                            AdresseLigne1 = $"{f.Client.Adresses.First().Numero} {f.Client.Adresses.First().Rue}",
                            AdresseLigne2 = $"{f.Client.Adresses.First().Appartement}",
                            AdresseLigne3 = $"{f.Client.Adresses.First().Ville}, {f.Client.Adresses.First().Province}, {f.Client.Adresses.First().Pays}",
                            CodePostal = f.Client.Adresses.First().CodePostal,
                            Courriel = f.Client.Email,
                            Nom = $"{f.Client.FirstName} {f.Client.Name}",
                            Telephone = f.Client.PhoneNumber,
                            ClientId = f.Client.Id,
                        },
                        PrixFinal = f.PrixFinal,
                        SousTotal = f.SousTotal
                    };

                    var invoiceGen = await GenerateInvoice(factureGenerer);
                    SauvegarderPDFFacture(f.Id, invoiceGen);
                }
            }
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

        private async Task<string> GenerateInvoice([FromBody] FactureGenererVM model)
        {
            var baseUrl = _invoiceSettings.Value.BaseUrl;
            var uploadPath = Path.Combine(_environment.ContentRootPath, _invoiceSettings.Value.InvoicePath);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var report = await _jsReportService.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Content = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    " +
                    $"<meta\r\n      name=\"viewport\"\r\n      content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"\r\n    />\r\n   " +
                    $" <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css\"\r\n      integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\"\r\n      crossorigin=\"anonymous\"\r\n    />\r\n  " +
                    $"</head>\r\n  <body>\r\n    <div class=\"container my-2\">\r\n      <div class=\"d-flex align-items-center\">\r\n        " +
                    $"<h1 class=\"col-9\">Les Encans de Nantes au Québec</h1>\r\n        <div class=\"col-3\">\r\n          " +
                    $"<img\r\n            class=\"img-fluid\"\r\n            src=\"https://sqlinfocg.cegepgranby.qc.ca/2162067/images/Logo.png\"\r\n          />\r\n        " +
                    $"</div>\r\n      </div>\r\n      <hr />\r\n      <div class=\"d-flex\">\r\n        <div class=\"d-flex flex-column col-6 justify-content-between\">\r\n          " +
                    $"<h5>Facturer à</h5>\r\n          <span>{model.Client.Nom}</span>\r\n          <span>{model.Client.AdresseLigne1}</span>\r\n          <span>{model.Client.AdresseLigne2}</span>\r\n          " +
                    $"<span>{model.Client.AdresseLigne3}</span>\r\n          <span>{model.Client.CodePostal}</span>\r\n          <span>{model.Client.Courriel}</span>\r\n          <span>{model.Client.Telephone}</span>\r\n        " +
                    $"</div>\r\n\r\n        <div class=\"col-6 d-flex justify-content-end\">\r\n          <h5>{model.DateAchat}</h5>\r\n        </div>\r\n      </div>\r\n      <br />\r\n      <h3 class=\"text-center\">\r\n        {model.PrixFinal} $ payé le {model.DateAchat}\r\n      </h3>\r\n      " +
                    $"<div class=\"d-flex w-100\">\r\n        <table class=\"table\">\r\n          <tbody id=\"tableProduits\"></tbody>\r\n        </table>\r\n      </div>\r\n      <br />\r\n      <div class=\"d-flex flex-column\">\r\n        <table class=\"table col-6\">\r\n          <tbody>\r\n            <tr>\r\n              " +
                    $"<th scope=\"col\" class=\"text-start\">Sous-total</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.SousTotal} $</td>\r\n            </tr>\r\n           " +
                    $" <tr>\r\n              <th scope=\"col\" class=\"text-start\">TPS</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TPS} $</td>\r\n            " +
                    $"</tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">TVQ</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TVQ} $</td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        " +
                    $"<h4>TOTAL: {model.PrixFinal} $</h4>\r\n      </div>\r\n    </div>\r\n\r\n    <!-- Optional JavaScript -->\r\n    <script\r\n      src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\"\r\n      integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    " +
                    $"<script\r\n      src=\"https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js\"\r\n      integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    " +
                    $"<script\r\n      src=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js\"\r\n      integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    " +
                    $"<script>\r\n      var table = document.getElementById(\"tableProduits\");\r\n\r\n      var lots = {model.Lots.ToJson()}\r\n\r\n      lots.forEach(element => {{\r\n        var tr = document.createElement(\"tr\")\r\n\r\n        var th = document.createElement(\"th\")\r\n        th.scope= \"col\"\r\n        th.classList.add(\"text-start\")\r\n        th.innerHTML = element.Description\r\n\r\n        var td = document.createElement(\"td\")\r\n        td.scope=\"col\"\r\n        td.classList.add(\"text-end\")\r\n        td.innerHTML = element.Prix +\" $\"\r\n\r\n        tr.appendChild(th)\r\n        tr.appendChild(td)\r\n\r\n        table.appendChild(tr)\r\n      }});\r\n\r\n      var trFrais = document.createElement(\"tr\")\r\n\r\n        var thFrais = document.createElement(\"th\")\r\n        thFrais.scope= \"col\"\r\n        thFrais.classList.add(\"text-start\")\r\n        thFrais.innerHTML = \"Frais d'encanteur\"\r\n\r\n        var tdFrais = document.createElement(\"td\")\r\n        tdFrais.scope=\"col\"\r\n        tdFrais.classList.add(\"text-end\")\r\n        tdFrais.innerHTML = {model.FraisEncanteur} +\" $\"\r\n\r\n        trFrais.appendChild(thFrais)\r\n        trFrais.appendChild(tdFrais)\r\n\r\n        table.appendChild(trFrais)\r\n    </script>\r\n  </body>\r\n</html>\r\n",
                    Engine = Engine.Handlebars,
                    Recipe = Recipe.ChromePdf
                },
                Data = new { idFacture = model.Id }
            });

            var memoryStream = new MemoryStream();
            await report.Content.CopyToAsync(memoryStream);

            string fileName = $"F232_{model.Id}_{Guid.NewGuid()}.pdf";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(memoryStream.ToArray());
            }

            var pdfBytes = memoryStream.ToArray();
            var client = await _context.Users.FindAsync(model.Client.ClientId);
            if (client != null)
            {
                _emailSender.Send(
                    client.Email,
                    "Confirmation de paiement",
                    "Merci d'avoir magasiné chez Encans de Nantes au Québec. Vous trouverez votre facture en pièce jointe.",
                    pdfBytes,
                    fileName
                );

                return $"{baseUrl}/Factures/{fileName}";
            }

            return string.Empty;
        }


    }
}

