using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UtilisateursController : ControllerBase
    {
        private readonly InscriptionService _inscriptionService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;


        public UtilisateursController(InscriptionService inscriptionService, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _inscriptionService = inscriptionService;
            _emailSender = emailSender;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("creer")]
        public async Task<IActionResult> Creer([FromBody] InscriptionVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

                    var subject = "Confirmation de courriel";
                    var messageText = $"Veuillez confirmer votre courriel en cliquant sur ce lien : <a href='{confirmationLink}'>Confirmer votre compte</a>";

                    _emailSender.Send(client.Email, subject, messageText);

                }

                return Ok(new { success = true, message = message });
            }
            else
            {
                return BadRequest(new { success = false, message = message });
            }

        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery(Name = "userId")] string idClient, string token)
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


            return Ok("Email confirmé avec succès.");
        }

        [Authorize(Roles = "Client")]
        [HttpGet("me")]
        public async Task<IActionResult> GetClientInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.Users
                .Include(u => u.Adresses)
                .Include(u => u.CarteCredits)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
        

            var adresse = user.Adresses.FirstOrDefault(a => a.EstDomicile);
            var carteCredit = user.CarteCredits.FirstOrDefault();

            var clientInfo = new UpdateClientInfoVM
            {
                Name = user.Name,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Pseudonym = user.UserName,
                CardOwnerName = carteCredit?.Nom,
                CardNumber = carteCredit?.Numero,
                CardExpiryDate = carteCredit != null ? $"{carteCredit.MoisExpiration}/{carteCredit.AnneeExpiration}" : null,
                CivicNumber = adresse?.Numero.ToString(),
                Street = adresse?.Rue,
                Apartment = adresse?.Appartement,
                City = adresse?.Ville,
                Province = adresse?.Province,
                Country = adresse?.Pays,
                PostalCode = adresse?.CodePostal,
                Photo = user.Avatar
            };

            return Ok(clientInfo);
        }


            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            _context.Users.Update(client);
            await _context.SaveChangesAsync();


            return Ok("Email confirmé avec succès.");
        }


    }




    [Authorize(Roles = "Client")]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateClientInfo([FromBody] UpdateClientInfoVM model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        user.Name = model.Name;
        user.FirstName = model.FirstName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.Pseudonym = model.Pseudonym;
        // Ajoutez les champs supplémentaires ici
        user.CardOwnerName = model.CardOwnerName;
        user.CardNumber = model.CardNumber;
        user.CardExpiryDate = model.CardExpiryDate;
        user.CivicNumber = model.CivicNumber;
        user.Street = model.Street;
        user.Apartment = model.Apartment;
        user.City = model.City;
        user.Province = model.Province;
        user.Country = model.Country;
        user.PostalCode = model.PostalCode;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest("Erreur lors de la mise à jour des informations.");
        }

        return Ok(new
        {
            user.Name,
            user.FirstName,
            user.Email,
            user.PhoneNumber,
            user.Pseudonym,
            user.CardOwnerName,
            user.CardNumber,
            user.CardExpiryDate,
            user.CivicNumber,
            user.Street,
            user.Apartment,
            user.City,
            user.Province,
            user.Country,
            user.PostalCode
        });
    }

}
