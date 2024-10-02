using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncansController : ControllerBase
    {
        private readonly EncanService _encanService;

        public EncansController(EncanService encanService)
        {
            _encanService = encanService;
        }

        [HttpGet]
        public ICollection<EncanAffichageVM> ChercherTousEncans()
        {
            ICollection<EncanAffichageVM> encans = _encanService.ChercherTousEncans();
            return encans;
        }
    }
}
