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

        public FacturesController(IJsReportMVCService jsReportService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, FactureService factureService, IEmailSender emailSender)
        {
            _jsReportService = jsReportService;
            _context = context;
            _userManager = userManager;
            _factureService = factureService;
            _emailSender = emailSender;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateInvoice([FromBody] Facture model)
        {
            var report = await _jsReportService.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Content = "<h1>Invoice for {{name}}</h1><p>Amount: ${{amount}}</p>",
                    Engine = Engine.Handlebars,
                    Recipe = Recipe.ChromePdf
                },
                Data = new
                {
                    idFacture = model.Id,
                    adresse = _context.Adresses.FindAsync(model.Client.Adresses.First().Id),
                    user = _context.Users.FindAsync(model.IdClient),

                }
            });

            //File(report.Content, "application/pdf", "invoice.pdf");
            var memoryStream = new MemoryStream();

            await report.Content.CopyToAsync(memoryStream);
            var pdfBytes = memoryStream.ToArray();

            var client = await _context.Users.FindAsync(model.IdClient);
            if (client != null)
            {

                var subject = "Confirmation de courriel";
                var messageText = $"Merci d'avoir magasiné chez Encans de Nantes au Québec. Vous trouverez votre facture en pièce jointe.";
                var attachmentName = "invoice.pdf";


                _emailSender.Send(client.Email, subject, messageText, pdfBytes, attachmentName);
                return Ok("Facture envoyé avec succès");


            }
            else
            {
                return BadRequest("Erreur lors de l'envoi de la facture");
            }

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
        public ICollection<FactureGenererVM> CreerFacturesParEncan(int numeroEncan)
        {
            ICollection<Facture> factures = _factureService.CreerFacturesParEncan(numeroEncan);

            var facturesGen = factures.Select(f => new FactureGenererVM
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
                SousTotal = f.SousTotal
            });

            return facturesGen.ToList();
        }
    }
}
