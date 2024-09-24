using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UtilisateursController : ControllerBase
    {
        private readonly InscriptionService _inscriptionService;
        private readonly MailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public UtilisateursController(InscriptionService inscriptionService, MailSender emailSender,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _inscriptionService = inscriptionService;
            _emailSender = emailSender;
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("creer")]
        public async Task<IActionResult> Creer([FromBody] InscriptionVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Url.Action("Index", "Home");

            var (success, message) = await _inscriptionService.InscrireUtilisateur(model);
            if (success)
            {

                var client = _context.Users.FirstOrDefault(x => x.UserName == model.GeneralInfo.Pseudo);
                if (client != null)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(client);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    // Construire le lien de confirmation
                    var confirmationLink = Url.Action("ConfirmEmail", "Utilisateurs", new { userId = client.Id, token }, protocol: Request.Scheme);

                    var subject = "Confirmation de votre compte";
                    var messageText = $"Veuillez confirmer votre compte en cliquant sur ce lien : <a href='{confirmationLink}'>Confirmer votre compte</a>";

                    await SendEmailAsync(client.Email, subject, messageText);

                }

                return Ok(new { success = true, message = message });
            }
            else
            {
                return BadRequest(new { success = false, message = message });
            }

        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string idClient, string token)
        {
            if (string.IsNullOrEmpty(idClient) || string.IsNullOrEmpty(token))
            {
                return BadRequest("User ID ou Token manquant");
            }

            var client = _context.Users.FirstOrDefault(x => x.Id == idClient);
            if (client == null)
            {
                return NotFound("Utilisateur introuvable");
            }


            client.EmailConfirmed = true;

            _context.Users.Update(client);
            await _context.SaveChangesAsync();


            return BadRequest("Token invalide.");
        }

        [HttpPost]
        public async Task SendEmailAsync(string email, string subject, string messageText)
        {
            var smtpClient = new SmtpClient
            {
                Host = _configuration["SmtpSettings:Server"],
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                EnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]),
                Credentials = new NetworkCredential(
                    _configuration["SmtpSettings:Username"],
                    _configuration["SmtpSettings:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]),
                Subject = subject,
                Body = messageText,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }


    }

}