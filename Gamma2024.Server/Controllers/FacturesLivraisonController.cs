using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturesLivraisonController : Controller
    {

        private readonly FactureService _factureService;
        private readonly FactureLivraisonService _factureLivraisonService;

        public FacturesLivraisonController(FactureService factureService, FactureLivraisonService factureLivraisonService)
        {
            _factureService = factureService;
            _factureLivraisonService = factureLivraisonService;
        }

        [HttpGet("GenererFactureLivraison/{idFacture}")]
        public IActionResult GenererFactureLivraison(int idFacture)
        {
            var facture = _factureService.ChercherFacture(idFacture);

            if (facture.IdClient != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return BadRequest("Aucun facture trouvé");
            }

            if (facture.ChoixLivraison != null)
            {
                return BadRequest("Le choix de livraison pour cette facture à déja été fait.");
            }

            var factureLivraison = _factureLivraisonService.GenererFactureLivraison(facture);

            if (factureLivraison == null)
            {
                return BadRequest("Erreur lors de la génration de la facture de livraison.");
            }
            return Ok(factureLivraison);
        }

        [HttpPost("EnregistrerChoixLivraison")]
        public IActionResult EnregistrerChoixLivraison([FromBody] FactureLivraisonAjoutVM choixLivraison)
        {
            var factureLivraison = _factureLivraisonService.AjouterFactureLivraison(choixLivraison);
            if (factureLivraison == null)
            {
                return Ok();
            }

            var productService = new ProductService();
            var invoiceService = new InvoiceService();
            var invoiceItemService = new InvoiceItemService();
            var paymentMethodService = new PaymentMethodService();

            if (!factureLivraison.EstPaye)
            {
                var customerId = factureLivraison.Facture.Client.StripeCustomer;

                var invoiceOptions = new InvoiceCreateOptions
                {
                    Customer = customerId,
                    CollectionMethod = "charge_automatically",
                    DefaultPaymentMethod = choixLivraison.PmId,
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

                _factureLivraisonService.PayerFactureLivraison(factureLivraison.Id);
            }

            return Ok();
        }
    }
}
