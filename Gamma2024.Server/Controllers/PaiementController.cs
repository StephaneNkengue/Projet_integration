using Gamma2024.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaiementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreerPaymentIntent/{idFacture}")]
        public ActionResult CreerPaymentIntent(int idFacture)
        {
            var facture = _context.Factures.FirstOrDefault(f => f.Id == idFacture);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (facture == null || facture.IdClient != userId)
            {
                return NotFound();
            }

            if (facture.estPaye)
            {
                return NoContent();
            }

            var prix = (long)facture.PrixFinal * 100;

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = prix,
                Currency = "cad",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new
            {
                clientSecret = paymentIntent.ClientSecret,
            });
        }
    }
}
