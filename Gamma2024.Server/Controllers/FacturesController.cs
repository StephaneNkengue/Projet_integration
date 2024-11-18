using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public FacturesController(IJsReportMVCService jsReportService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, FactureService factureService)
        {
            _jsReportService = jsReportService;
            _context = context;
            _userManager = userManager;
            _factureService = factureService;
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

        [HttpPost("CreerFacturesParEncan/{numeroEncan}")]
        public ICollection<Facture> CreerFacturesParEncan(int numeroEncan)
        {
            ICollection<Facture> factures = _factureService.CreerFacturesParEncan(numeroEncan);

            return factures;
        }
    }
}
