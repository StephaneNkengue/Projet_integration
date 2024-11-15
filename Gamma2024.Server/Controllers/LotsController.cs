using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LotsController : ControllerBase
	{
		private readonly LotService _lotService;
		private readonly ApplicationDbContext _context;

		public LotsController(LotService lotService, ApplicationDbContext context)
		{
			_lotService = lotService;
			_context = context;
		}

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
		[HttpGet("tous")]
		public async Task<ActionResult<IEnumerable<LotAffichageVM>>> ObtenirTousLots()
		{
			var lots = await _lotService.ObtenirTousLots();
			return Ok(lots.ToList());
		}

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
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

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
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

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
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

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
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

		[HttpGet("artistes")]
		public async Task<ActionResult<ICollection<ArtisteVM>>> ObtenirTousArtistes()
		{
			try
			{
				var artistes = _lotService.ObtenirTousArtistes();
				return Ok(artistes);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erreur d'obtenir tous les artistes: {ex.Message}");
			}
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<Categorie>>> ObtenirCategories()
		{
			var categories = await _context.Categories.ToListAsync();
			return Ok(categories);
		}

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
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

		[Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
		[HttpGet("encans")]
		public async Task<ActionResult<IEnumerable<Encan>>> ObtenirEncans()
		{
			var encans = await _context.Encans.ToListAsync();
			return Ok(encans);
		}

		[HttpGet("cherchertouslotsrecherche")]
		[AllowAnonymous]
		public async Task<ActionResult<ICollection<LotEncanAffichageVM>>> ChercherTousLotsRecherche()
		{
			try
			{
				var lots = _lotService.ChercherTousLotsRecherche();
				return Ok(lots);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erreur: {ex.Message}");
			}
		}

		[HttpGet("cherchertouslotsparencan/{idEncan}")]
		[AllowAnonymous]
		public async Task<ActionResult<ICollection<LotEncanAffichageVM>>> ChercherTousLotsParEncan(string idEncan)
		{
			try
			{
				var idEncanInt = int.Parse(idEncan);
				var lots = _lotService.ChercherTousLotsParEncan(idEncanInt);
				return Ok(lots);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erreur: {ex.Message}");
			}
		}

		[AllowAnonymous]
		[HttpGet("chercherTousLots")]
		public ICollection<LotAffichageAdministrateurVM> ChercherTousLots()
		{
			ICollection<LotAffichageAdministrateurVM> lots = _lotService.ChercherTousLots();
			return lots;
		}

		[AllowAnonymous]
		[HttpGet("chercherDetailsLotParId/{idLot}")]
		public LotDetailsVM ChercherDetailsLotParId(string idLot)
		{
			try
			{
				var idLotInt = int.Parse(idLot);
				LotDetailsVM lot = _lotService.ChercherDetailsLotParId(idLotInt);
				return lot;
			}
			catch
			{
				return null;
			}
		}

		[Authorize(Roles = ApplicationRoles.CLIENT)]
		[HttpPost("placerMise")]
		public async Task<IActionResult> PlacerMise([FromBody] MiseVM mise)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Récupérer l'ID de l'utilisateur connecté
			mise.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(mise.UserId))
			{
				return Unauthorized();
			}

			var (success, message) = await _lotService.PlacerMise(mise);
			if (success)
			{
				return Ok(new { success = true, message = message });
			}
			else
			{
				return BadRequest(new { success = false, message = message });
			}
		}

		[Authorize(Roles = ApplicationRoles.CLIENT)]
		[HttpGet("userBids/{userId}")]
		public async Task<IActionResult> GetUserBids(string userId)
		{
			if (userId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
			{
				return Unauthorized();
			}

			var userBids = await _lotService.GetUserBids(userId);
			return Ok(userBids);
		}

	}
}
