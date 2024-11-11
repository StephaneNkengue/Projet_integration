using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaiementController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        [Authorize(Roles = "Client")]
        [HttpPost("CreerSetupIntent")]
        public async Task<ActionResult> CreerSetupIntent()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("ID utilisateur non trouvé");
            }

            var user = await _userManager.Users
                .OfType<ApplicationUser>()
                .Include(u => u.Adresses.Where(a => a.EstDomicile))
                .FirstOrDefaultAsync(u => u.Id == userId);

            var options = new SetupIntentCreateOptions
            {
                Customer = user.StripeCustomer,
                AutomaticPaymentMethods = new SetupIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new SetupIntentService();
            SetupIntent intent = service.Create(options);
            return Ok(new { clientSecret = intent.ClientSecret });
        }
    }
}
