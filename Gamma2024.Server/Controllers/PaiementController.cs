using Gamma2024.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaiementController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Payer(int idFacture)
        {
            var facture = _context.Factures.FirstOrDefault(f => f.Id == idFacture);

            if (facture == null)
            {
                return NotFound();
            }

            var prix = (long)facture.PrixFinal * 100;

            var options = new ChargeCreateOptions
            {
                Amount = prix,
                Currency = "cad",
                Source = "tok_amex",
                Description = "Test",
            };
            var service = new ChargeService();
            service.Create(options);
            return Ok();
        }
    }
}
