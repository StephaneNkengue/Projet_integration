using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController(LotService lotService) : ControllerBase
    {
        private readonly LotService _lotService;
        public ICollection<LotAffichageVM> ChercherTousLotsParEncan(int idEncan)
        {
            ICollection<LotAffichageVM> lots = _lotService.ChercherTousLotsParEncan(idEncan);
            return lots;
        }
    }
}
