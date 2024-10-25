using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
    public class LotsController : ControllerBase
    {
        private readonly LotService _lotService;
        private readonly ApplicationDbContext _context;

        public LotsController(LotService lotService, ApplicationDbContext context)
        {
            _lotService = lotService;
            _context = context;
        }

        [HttpGet("tous")]
        public async Task<ActionResult<IEnumerable<LotAffichageVM>>> ObtenirTousLots()
        {
            var lots = await _lotService.ObtenirTousLots();
            return Ok(lots.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LotAffichageVM>> ObtenirLot(int id)
        {
            var lot = await _lotService.ObtenirLot(id);
            if (lot == null)
            {
                return NotFound();
            }
            return Ok(lot);
        }

        [HttpPost("creer")]
        public async Task<ActionResult<LotAffichageVM>> CreerLot([FromForm] LotCreationVM lotVM)
        {
            if (Request.Form.Files.Count > 0)
            {
                lotVM.Photos = Request.Form.Files.ToList();
            }

            var resultat = await _lotService.CreerLot(lotVM);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return Ok(new { success = true, message = "Lot crée avec succès" });
        }

        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> ModifierLot(int id, LotModificationVM lotVM)
        {
            var resultat = await _lotService.ModifierLot(id, lotVM);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return Ok(new { success = true, message = "Lot modifié avec succès" });
        }

        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerLot(int id)
        {
            var resultat = await _lotService.SupprimerLot(id);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return Ok(new { success = true, message = "Lot supprimé avec succès" });
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Categorie>>> ObtenirCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("vendeurs")]
        public async Task<ActionResult<IEnumerable<Vendeur>>> ObtenirVendeurs()
        {
            var vendeurs = await _context.Vendeurs.ToListAsync();
            return Ok(vendeurs);
        }

        [HttpGet("mediums")]
        public async Task<ActionResult<IEnumerable<Medium>>> ObtenirMediums()
        {
            var mediums = await _context.Mediums.ToListAsync();
            return Ok(mediums);
        }

        [HttpGet("encans")]
        public async Task<ActionResult<IEnumerable<Encan>>> ObtenirEncans()
        {
            var encans = await _context.Encans.ToListAsync();
            return Ok(encans);
        }
    }
}
