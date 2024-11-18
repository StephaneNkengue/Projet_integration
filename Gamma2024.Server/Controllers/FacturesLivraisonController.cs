using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public FactureLivraisonVM GenererFactureLivraison(int idFacture)
        {
            var facture = _factureService.ChercherFacture(idFacture);

            var factureLivraison = _facturesLivraisonService.GenererFactureLivraison(facture);
            return factureLivraison;
        }
    }
}
