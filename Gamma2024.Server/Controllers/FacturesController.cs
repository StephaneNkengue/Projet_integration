using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.Security.Claims;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturesController : Controller
    {
        private readonly IJsReportMVCService _jsReportService;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FactureService _factureService;
        private readonly IWebHostEnvironment _environment;

        public FacturesController(IJsReportMVCService jsReportService, IWebHostEnvironment environment, ApplicationDbContext context, UserManager<ApplicationUser> userManager, FactureService factureService, IEmailSender emailSender)
        {
            _jsReportService = jsReportService;
            _context = context;
            _userManager = userManager;
            _factureService = factureService;
            _emailSender = emailSender;
            _environment = environment;
        }

        [HttpGet("chercherFactures")]
        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            ICollection<FactureAffichageVM> factures = _factureService.ChercherFactures();
            return factures;
        }
        [Authorize(Roles = "Client")]
        [HttpGet("chercherFacturesMembre")]
        public ICollection<FactureAffichageMembreVM> ChercherFacturesMembre()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            ICollection<FactureAffichageMembreVM> factures = _factureService.ChercherFacturesMembre(userId);
            return factures;
        }
        [HttpPost("CreerFacturesParEncan/{numeroEncan}")]
        public async Task<IActionResult> CreerFacturesParEncan(int numeroEncan)
        {
            ICollection<Facture> factures = _factureService.CreerFacturesParEncan(numeroEncan);

            foreach (var f in factures)
            {
                var urlRecuStripe = _factureService.ChargerFacture(f);
                if (!urlRecuStripe.IsNullOrEmpty())
                {
                    var factureGenerer = new FactureGenererVM
                    {
                        DateAchat = f.DateAchat,
                        FraisEncanteur = f.FraisEncanteur,
                        NumeroEncan = numeroEncan,
                        Id = f.Id,
                        TPS = f.TPS,
                        TVQ = f.TVQ,
                        Lots = f.Lots.Select(l => new LotFactureVM
                        {
                            Description = l.Description,
                            Prix = l.Mise.Value.ToString(),
                        }).ToList(),
                        Client = new ClientFactureVM
                        {
                            AdresseLigne1 = $"{f.Client.Adresses.First().Numero} {f.Client.Adresses.First().Rue}",
                            AdresseLigne2 = $"{f.Client.Adresses.First().Appartement}",
                            AdresseLigne3 = $"{f.Client.Adresses.First().Ville}, {f.Client.Adresses.First().Province}, {f.Client.Adresses.First().Pays}",
                            CodePostal = f.Client.Adresses.First().CodePostal,
                            Courriel = f.Client.Email,
                            Nom = $"{f.Client.FirstName} {f.Client.Name}",
                            Telephone = f.Client.PhoneNumber,
                            ClientId = f.Client.Id,
                        },
                        PrixFinal = f.PrixFinal,
                        SousTotal = f.SousTotal,
                        UrlInvoiceStripe = urlRecuStripe
                    };

                    var invoiceGen = await GenerateInvoice(factureGenerer);
                    _factureService.SauvegarderPDFFacture(f.Id, invoiceGen);
                }
            }

            return Ok(factures);
        }

        [HttpGet("chercherFacturesChoixAFaire")]
        public IActionResult ChercherFacturesChoixAFaire()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Aucun utilisateur trouvé.");
            }

            var choix = _context.Factures.Where(f => f.ChoixLivraison == null && f.IdClient == userId);

            return Ok(choix.Select(c => new FactureChoixAFaireVM()
            {
                Id = c.Id,
                NumeroEncan = c.NumeroEncan,
            }).ToList());
        }

        [HttpGet("chercherFacturesParEncan/{numeroEncan}")]
        public IActionResult ChercherFacturesParEncan(int numeroEncan)
        {
            var factures = _factureService.ChercherFacturesParEncan(numeroEncan);

            return Ok(factures);
        }
    }
}
