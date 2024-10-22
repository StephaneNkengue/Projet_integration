using Gamma2024.Server.Services;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
    public class LotsController : ControllerBase
    {
        private readonly LotService _lotService;

        public LotsController(LotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet("tous")]
        public async Task<ActionResult<IEnumerable<LotAffichageVM>>> ObtenirTousLots()
        {
            var lots = await _lotService.ObtenirTousLots();
            return Ok(lots);
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
        public async Task<ActionResult<LotAffichageVM>> CreerLot(LotCreationVM lotVM)
        {
            var resultat = await _lotService.CreerLot(lotVM);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return CreatedAtAction(nameof(ObtenirLot), new { id = resultat.Lot.Id }, resultat.Lot);
        }

        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> ModifierLot(int id, LotModificationVM lotVM)
        {
            var resultat = await _lotService.ModifierLot(id, lotVM);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return NoContent();
        }

        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerLot(int id)
        {
            var resultat = await _lotService.SupprimerLot(id);
            if (!resultat.Success)
            {
                return BadRequest(resultat.Message);
            }
            return NoContent();
        }
    }
}
