using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
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
            _userManager = userManager;
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

        [Authorize(Roles = "Client")]
        [HttpGet("ChercherCartes")]
        public async Task<ActionResult> ChercherCartes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("ID utilisateur non trouvé");
            }

            var user = await _userManager.Users
                .OfType<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var service = new PaymentMethodService();
            var options = new PaymentMethodListOptions
            {
                Type = "card",
                Limit = 100,
                Customer = user.StripeCustomer,
            };

            StripeList<PaymentMethod> paymentMethods = service.List(options);
            List<CarteCreditInfoVM> cartes = new();

            foreach (var paymentMethod in paymentMethods.Data)
            {
                cartes.Add(new CarteCreditInfoVM()
                {
                    Dernier4Numero = paymentMethod.Card.Last4,
                    ExpirationDate = paymentMethod.Card.ExpMonth + "/" + paymentMethod.Card.ExpYear,
                    Marque = paymentMethod.Card.Brand
                });
            }
            return Ok(cartes);
        }
    }
}
