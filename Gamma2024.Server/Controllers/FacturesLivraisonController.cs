using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturesLivraisonController : Controller
    {

        private readonly FactureService _factureService;
        private readonly FactureLivraisonService _facturesLivraisonService;

        public FacturesLivraisonController(FactureService factureService, FactureLivraisonService factureLivraisonService)
        {
            _factureService = factureService;
            _facturesLivraisonService = factureLivraisonService;
        }

        [HttpGet("GenererFactureLivraison/{idFacture}")]
        public IActionResult GenererFactureLivraison(int idFacture)
        {
            var facture = _factureService.ChercherFacture(idFacture);

            if (facture.IdClient != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return BadRequest("Aucun facture trouvé");
            }

            var factureLivraison = _facturesLivraisonService.GenererFactureLivraison(facture);
            return Ok(factureLivraison);
        }

        [HttpPost("EnregistrerChoixLivraison")]
        public IActionResult EnregistrerChoixLivraison([FromBody] FactureLivraisonAjoutVM choixLivraison)
        {
            var factureLivraison = _facturesLivraisonService.AjouterFactureLivraison(choixLivraison);
            if (factureLivraison == null)
            {
                return BadRequest("Erreur d'enregistrement");
            }

            return Ok(factureLivraison.Id);
        }
    }
}
