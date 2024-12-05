using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FactureService _factureService;
        private readonly IWebHostEnvironment _environment;

        public FacturesController(IWebHostEnvironment environment, ApplicationDbContext context, UserManager<ApplicationUser> userManager, FactureService factureService, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _factureService = factureService;
            _emailSender = emailSender;
            _environment = environment;
        }

        [HttpGet("chercherFactures")]
        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            ICollection<FactureAffichageVM> factures = _factureService.ChercherFactures();
            return factures;
        }

        [Authorize(Roles = "Client")]
        [HttpGet("chercherFacturesMembre")]
        public ICollection<FactureAffichageMembreVM> ChercherFacturesMembre()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            ICollection<FactureAffichageMembreVM> factures = _factureService.ChercherFacturesMembre(userId);
            return factures;
        }

        [HttpPost("CreerFacturesParEncan/{numeroEncan}")]
        public async Task<IActionResult> CreerFacturesParEncan(int numeroEncan)
        {
            ICollection<Facture> factures = await _factureService.CreerFacturesParEncan(numeroEncan);
            return Ok(factures);
        }

        [HttpGet("chercherFacturesChoixAFaire")]
        public IActionResult ChercherFacturesChoixAFaire()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Aucun utilisateur trouvé.");
            }

            var choix = _context.Factures.Where(f => f.ChoixLivraison == null && f.IdClient == userId);

            return Ok(choix.Select(c => new FactureChoixAFaireVM()
            {
                Id = c.Id,
                NumeroEncan = c.NumeroEncan,
            }).ToList());
        }

        [HttpGet("chercherFacturesParEncan/{numeroEncan}")]
        public IActionResult ChercherFacturesParEncan(int numeroEncan)
        {
            var factures = _factureService.ChercherFacturesParEncan(numeroEncan);

            return Ok(factures);
        }
    }
}
