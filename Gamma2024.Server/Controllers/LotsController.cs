using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly LotService _lotService;

        public LotsController(LotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet("ChercherTousLotsParEncan/{idEncan}")]
        public ICollection<LotAffichageVM> ChercherTousLotsParEncan(string idEncan)
        {
            try
            {
                var idEncanInt = int.Parse(idEncan);
                ICollection<LotAffichageVM> lots = _lotService.ChercherTousLotsParEncan(idEncanInt);
                return lots;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("chercherTousLots")]
        public ICollection<LotAffichageAdministrateurVM> ChercherTousLots()
        {
            ICollection<LotAffichageAdministrateurVM> lots = _lotService.ChercherTousLots();
            return lots;
        }
    }
}
