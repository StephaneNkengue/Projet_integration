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

        [HttpGet("cherchertousencans")]
        public ICollection<EncanAffichageAdminVM> ChercherTousEncans()
        {
            ICollection<EncanAffichageAdminVM> encans = _encanService.ChercherTousEncans();
            return encans;
        }

        [HttpGet("ChercherTousEncansVisibles")]
        public ICollection<EncanAffichageAdminVM> ChercherTousEncansVisibles()
        {
            ICollection<EncanAffichageAdminVM> encans = _encanService.ChercherTousEncansVisibles();
            return encans;
        }

        [HttpPost("creerEncan")]
        public async Task<IActionResult> CreerEncan([FromBody] EncanCreerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (sucess, message) = await _encanService.CreerEncan(vm);
            if (sucess)
            {
                return Ok(new { sucess = true, message = message });
            }
            else
            {
                return BadRequest(new { sucess = false, message = message });
            }
        }

        [HttpPut("mettreAJourEncanPublie")]
        public async Task<IActionResult> MettreAJourEncanPublie([FromBody] EncanPublieVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (sucess, message) = await _encanService.MettreAJourEncanPublie(vm);
            if (sucess)
            {
                return Ok(new { sucess = true, message = message });
            }
            else
            {
                return BadRequest(new { sucess = false, message = message });
            }
        }
    }
}
