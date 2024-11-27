using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Gamma2024.Server.Services
{
    public class FactureLivraisonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJsReportMVCService _jsReportService;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;

        public FactureLivraisonService(ApplicationDbContext context, IJsReportMVCService jsReportService, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _context = context;
            _jsReportService = jsReportService;
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

        public async Task<FactureLivraison> AjouterFactureLivraison(FactureLivraisonAjoutVM choix)
        {
            var factureLivraison = new FactureLivraison();
            AdresseVM adresseTemp = new();
            if (choix.IdAdresse == null)
            {
                var facture = _context.Factures.FirstOrDefault(f => f.Id == choix.IdFacture);
                facture.ChoixLivraison = false;
                _context.Factures.Update(facture);
                _context.SaveChanges();

                return null;
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

            var factureLivraisonGen = new FactureLivraisonGenererVM
            {
                DateAchat = DateTime.Now,
                FraisLivraison = factureLivraison.SousTotal,
                SousTotal = factureLivraison.SousTotal,
                Id = factureLivraison.Id,
                PrixFinal = factureLivraison.PrixFinal,
                TPS = factureLivraison.TPS,
                TVQ = factureLivraison.TVQ,
                NumeroEncan = factureLivraison.Facture.NumeroEncan,
                Client = new ClientFactureVM
                {
                    AdresseLigne1 = $"{factureLivraison.Facture.Client.Adresses.First().Numero} {factureLivraison.Facture.Client.Adresses.First().Rue}",
                    AdresseLigne2 = $"{factureLivraison.Facture.Client.Adresses.First().Appartement}",
                    AdresseLigne3 = $"{factureLivraison.Facture.Client.Adresses.First().Ville}, {factureLivraison.Facture.Client.Adresses.First().Province}, {factureLivraison.Facture.Client.Adresses.First().Pays}",
                    CodePostal = factureLivraison.Facture.Client.Adresses.First().CodePostal,
                    Courriel = factureLivraison.Facture.Client.Email,
                    Nom = $"{factureLivraison.Facture.Client.FirstName} {factureLivraison.Facture.Client.Name}",
                    Telephone = factureLivraison.Facture.Client.PhoneNumber,
                    ClientId = factureLivraison.Facture.Client.Id
                },
                Don = 0
            };

            if (adresseTemp != null)
            {
                factureLivraisonGen.Client.AdresseLigne1 = $"{adresseTemp.NumeroCivique} {adresseTemp.Rue}";
                factureLivraisonGen.Client.AdresseLigne2 = $"{adresseTemp.Appartement}";
                factureLivraisonGen.Client.AdresseLigne3 = $"{adresseTemp.Ville}, {adresseTemp.Province}, {adresseTemp.Pays}";
                factureLivraisonGen.Client.CodePostal = $"{adresseTemp.CodePostal}";
            }
            else
            {
                var adresse = _context.Adresses.First(a => a.Id == choix.IdAdresse);
                factureLivraisonGen.Client.AdresseLigne1 = $"{adresse.Numero} {adresse.Rue}";
                factureLivraisonGen.Client.AdresseLigne2 = $"{adresse.Appartement}";
                factureLivraisonGen.Client.AdresseLigne3 = $"{adresse.Ville}, {adresse.Province}, {adresse.Pays}";
                factureLivraisonGen.Client.CodePostal = $"{adresse.CodePostal}";
            }

            if (choix.Don.HasValue && choix.Don != false)
            {
                factureLivraisonGen.Don = factureLivraison.Don;
            }

            var path = await GenerateInvoice(factureLivraisonGen);
            factureLivraison.FacturePDFPath = path;
            _context.FactureLivraisons.Update(factureLivraison);
            _context.SaveChanges();

            return factureLivraison;
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

        private async Task<string> GenerateInvoice(FactureLivraisonGenererVM model)
        {
            var report = await _jsReportService.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Content = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    <meta\r\n      name=\"viewport\"\r\n      content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"\r\n    />\r\n    <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css\"\r\n      integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\"\r\n      crossorigin=\"anonymous\"\r\n    />\r\n  </head>\r\n  <body>\r\n    <div class=\"container my-2\">\r\n      <div class=\"d-flex align-items-center\">\r\n        <h1 class=\"col-9\">Les Encans de Nantes au Québec</h1>\r\n        <div class=\"col-3\">\r\n          <img\r\n            class=\"img-fluid\"\r\n            src=\"https://sqlinfocg.cegepgranby.qc.ca/2162067/images/Logo.png\"\r\n          />\r\n        </div>\r\n      </div>\r\n      <hr />\r\n      <div class=\"d-flex\">\r\n        <div class=\"d-flex flex-column col-6 justify-content-between\">\r\n          <h5>Facturer à</h5>\r\n          <span>{model.Client.Nom}</span>\r\n          <span>{model.Client.AdresseLigne1}</span>\r\n          <span>{model.Client.AdresseLigne2}</span>\r\n          <span>{model.Client.AdresseLigne3}</span>\r\n          <span>{model.Client.CodePostal}</span>\r\n          <span>{model.Client.Courriel}</span>\r\n          <span>{model.Client.Telephone}</span>\r\n        </div>\r\n\r\n        <div class=\"col-6 d-flex justify-content-end\">\r\n          <h5>{model.DateAchat}</h5>\r\n        </div>\r\n      </div>\r\n      <br />\r\n      <h3 class=\"text-center\">\r\n        {model.PrixFinal} $ payé le {model.DateAchat}\r\n      </h3>\r\n      <div class=\"d-flex w-100\">\r\n        <table class=\"table\">\r\n          <tbody id=\"tableProduits\">\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">Frais de livraison</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.FraisLivraison} $</td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </div>\r\n      <br />\r\n      <div class=\"d-flex flex-column\">\r\n        <table class=\"table col-6\">\r\n          <tbody>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">Sous-total</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.SousTotal} $</td>\r\n            </tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">TPS</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TPS} $</td>\r\n            </tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">TVQ</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TVQ} $</td>\r\n            </tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">Don</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.Don} $</td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>TOTAL: {model.PrixFinal} $</h4>\r\n      </div>\r\n    </div>\r\n\r\n    <!-- Optional JavaScript -->\r\n    <script\r\n      src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\"\r\n      integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    <script\r\n      src=\"https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js\"\r\n      integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    <script\r\n      src=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js\"\r\n      integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n  </body>\r\n</html>\r\n",
                    Engine = Engine.Handlebars,
                    Recipe = Recipe.ChromePdf,
                },
                Data = new
                {
                    idFacture = model.Id
                }
            });

            var memoryStream = new MemoryStream();
            await report.Content.CopyToAsync(memoryStream);

            string uploadPath = Path.Combine(_environment.WebRootPath, "Factures");

            string fileName = $"FL232_{model.Id}_{Guid.NewGuid()}.pdf";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(memoryStream.ToArray());
            }

            var pdfBytes = memoryStream.ToArray();

            var client = await _context.Users.FindAsync(model.Client.ClientId);
            if (client != null)
            {
                var subject = "Confirmation de paiement - Livraison";
                var messageText = $"Merci d'avoir magasiné chez Encans de Nantes au Québec. Vous trouverez votre facture en pièce jointe.";
                var attachmentName = $"FactureLivraisonEncan{model.NumeroEncan}-EncansDeNantes.pdf";
                _emailSender.Send(client.Email, subject, messageText, pdfBytes, attachmentName);
                return $"Factures/{fileName}";
            }
            else
            {
                return "";
            }

        }
    }
}
