using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        [HttpGet]
        public IActionResult Payer(Facture facture)
        {
            var options = new ChargeCreateOptions
            {
                Amount = 2,
                Currency = "cad",
                Source = "tok_amex",
                Description = "Test Payer",
            };
            var service = new ChargeService();
            service.Create(options);
            return Ok();
        }
    }
}
