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

        [HttpGet("ChercherTousEncansVisibles")]
        public ICollection<EncanAffichageVM> ChercherTousEncansVisibles()
        {
            ICollection<EncanAffichageVM> encans = _encanService.ChercherTousEncansVisibles();
            return encans;
        }

        [HttpGet("ChercherEncanParNumero/{numeroEncan}")]
        public EncanAffichageVM ChercherEncanParNumero(string numeroEncan)
        {
            try
            {
                var numeroEncanInt = int.Parse(numeroEncan);
                var encan = _encanService.ChercherEncanParNumero(numeroEncanInt);
                return encan;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("ChercherEncanEnCours")]
        public EncanAffichageVM ChercherEncanEnCours()
        {
            try
            {
                var encan = _encanService.ChercherEncanEnCours();
                return encan;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("ChercherNumeroEncanEnCours")]
        public int ChercherNumeroEncanEnCours()
        {
            try
            {
                var numero = _encanService.ChercherNumeroEncanEnCours();
                return numero;
            }
            catch
            {
                return 0;
            }
        }

        [HttpGet("ChercherEncansFuturs")]
        public ICollection<EncanAffichageVM> ChercherEncansFuturs()
        {
            try
            {
                var encans = _encanService.ChercherEncansFuturs();
                return encans;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("ChercherEncansPasses")]
        public ICollection<EncanAffichageVM> ChercherEncansPasses()
        {
            try
            {
                var encans = _encanService.ChercherEncansPasses();
                return encans;
            }
            catch
            {
                return null;
            }
        }
    }
}
