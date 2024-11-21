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

        private async Task<IActionResult> GenerateInvoice([FromBody] FactureGenererVM model)
        {
            var report = await _jsReportService.RenderAsync(new RenderRequest
            {
                Template = new Template
                {
                    Content = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"utf-8\" />\r\n    <meta\r\n      name=\"viewport\"\r\n      content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"\r\n    />\r\n    <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css\"\r\n      integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\"\r\n      crossorigin=\"anonymous\"\r\n    />\r\n  </head>\r\n  <body>\r\n    <div class=\"container my-2\">\r\n      <div class=\"d-flex align-items-center\">\r\n        <h1 class=\"col-9\">Les Encans de Nantes au Québec</h1>\r\n        <div class=\"col-3\">\r\n          <img\r\n            class=\"img-fluid\"\r\n            src=\"https://sqlinfocg.cegepgranby.qc.ca/2162067/images/Logo.png\"\r\n          />\r\n        </div>\r\n      </div>\r\n      <hr />\r\n      <div class=\"d-flex\">\r\n        <div class=\"d-flex flex-column col-6 justify-content-between\">\r\n          <h5>Facturer à</h5>\r\n          <span>{model.Client.Nom}</span>\r\n          <span>{model.Client.AdresseLigne1}</span>\r\n          <span>{model.Client.AdresseLigne2}</span>\r\n          <span>{model.Client.AdresseLigne3}</span>\r\n          <span>{model.Client.CodePostal}</span>\r\n          <span>{model.Client.Courriel}</span>\r\n          <span>{model.Client.Telephone}</span>\r\n        </div>\r\n\r\n        <div class=\"col-6 d-flex justify-content-end\">\r\n          <h5>{model.DateAchat}</h5>\r\n        </div>\r\n      </div>\r\n      <br />\r\n      <h3 class=\"text-center\">\r\n        {model.PrixFinal} $ payé le {model.DateAchat}\r\n      </h3>\r\n      <div class=\"d-flex w-100\">\r\n        <table class=\"table\">\r\n          <tbody id=\"tableProduits\"></tbody>\r\n        </table>\r\n      </div>\r\n      <br />\r\n      <div class=\"d-flex flex-column\">\r\n        <table class=\"table col-6\">\r\n          <tbody>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">Sous-total</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.SousTotal} $</td>\r\n            </tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">TPS</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TPS} $</td>\r\n            </tr>\r\n            <tr>\r\n              <th scope=\"col\" class=\"text-start\">TVQ</th>\r\n              <td scope=\"col\" class=\"text-end\">{model.TVQ} $</td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <h4>TOTAL: {model.PrixFinal} $</h4>\r\n      </div>\r\n    </div>\r\n\r\n    <!-- Optional JavaScript -->\r\n    <script\r\n      src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\"\r\n      integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    <script\r\n      src=\"https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js\"\r\n      integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    <script\r\n      src=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js\"\r\n      integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\"\r\n      crossorigin=\"anonymous\"\r\n    ></script>\r\n    <script>\r\n      var table = document.getElementById(\"tableProduits\");\r\n\r\n      var lots = {model.Lots.ToJson()}\r\n\r\n      lots.forEach(element => {{\r\n        var tr = document.createElement(\"tr\")\r\n\r\n        var th = document.createElement(\"th\")\r\n        th.scope= \"col\"\r\n        th.classList.add(\"text-start\")\r\n        th.innerHTML = element.Description\r\n\r\n        var td = document.createElement(\"td\")\r\n        td.scope=\"col\"\r\n        td.classList.add(\"text-end\")\r\n        td.innerHTML = element.Prix +\" $\"\r\n\r\n        tr.appendChild(th)\r\n        tr.appendChild(td)\r\n\r\n        table.appendChild(tr)\r\n      }});\r\n\r\n      var trFrais = document.createElement(\"tr\")\r\n\r\n        var thFrais = document.createElement(\"th\")\r\n        thFrais.scope= \"col\"\r\n        thFrais.classList.add(\"text-start\")\r\n        thFrais.innerHTML = \"Frais d'encanteur\"\r\n\r\n        var tdFrais = document.createElement(\"td\")\r\n        tdFrais.scope=\"col\"\r\n        tdFrais.classList.add(\"text-end\")\r\n        tdFrais.innerHTML = {model.FraisEncanteur} +\" $\"\r\n\r\n        trFrais.appendChild(thFrais)\r\n        trFrais.appendChild(tdFrais)\r\n\r\n        table.appendChild(trFrais)\r\n    </script>\r\n  </body>\r\n</html>\r\n",
                    Engine = Engine.Handlebars,
                    Recipe = Recipe.ChromePdf,
                },
                Data = new
                {
                    idFacture = model.Id
                }
            });

            var memoryStream = new MemoryStream();
            await report.Content.CopyToAsync(memoryStream);

            string uploadPath = Path.Combine(_environment.WebRootPath, "Factures");

            string fileName = $"F232_{model.Id}_{Guid.NewGuid()}.pdf";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(memoryStream.ToArray());
            }

            var pdfBytes = memoryStream.ToArray();

            var client = await _context.Users.FindAsync(model.Client.ClientId);
            if (client != null)
            {
                var subject = "Confirmation de paiement";
                var messageText = $"Merci d'avoir magasiné chez Encans de Nantes au Québec. Vous trouverez votre facture en pièce jointe.";
                var attachmentName = $"FactureEncan{model.NumeroEncan}-EncansDeNantes.pdf";
                _emailSender.Send(client.Email, subject, messageText, pdfBytes, attachmentName);
                return Ok(pdfBytes);
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
        public async Task<IActionResult> CreerFacturesParEncan(int numeroEncan)
        {
            ICollection<Facture> factures = _factureService.CreerFacturesParEncan(numeroEncan);

            foreach (var f in factures)
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
                    SousTotal = f.SousTotal
                };

                var invoiceGen = await GenerateInvoice(factureGenerer);
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
    }
}
