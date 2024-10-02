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
using Gamma2024.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UtilisateursController : ControllerBase
    {
        private readonly ClientInscriptionService _inscriptionService;
        private readonly ClientModificationService _modificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly InscriptionService _inscriptionService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UtilisateursController(UserManager<ApplicationUser> userManager, ClientInscriptionService inscriptionService, ClientModificationService modificationService, IWebHostEnvironment webHostEnvironment)

        public UtilisateursController(InscriptionService inscriptionService, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _inscriptionService = inscriptionService;
            _modificationService = modificationService;
            _webHostEnvironment = webHostEnvironment;
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
        [HttpGet("ObtentionInfoClient")]
        public async Task<IActionResult> GetClientInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users
                .OfType<ApplicationUser>() 
                .Include(u => u.Adresses.Where(a => a.EstDomicile))
                .Include(u => u.CarteCredits)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
        

            var clientInfo = new UpdateClientInfoVM
            {
                Name = user.Name,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Pseudonym = user.UserName,
                CardOwnerName = user.CarteCredits.FirstOrDefault()?.Nom,
                CardNumber = user.CarteCredits.FirstOrDefault()?.Numero,
                CardExpiryDate = user.CarteCredits.Any() 
                    ? $"{user.CarteCredits.First().MoisExpiration:D2}/{user.CarteCredits.First().AnneeExpiration % 100:D2}" 
                    : null,
                CivicNumber = user.Adresses.FirstOrDefault()?.Numero.ToString(),
                Street = user.Adresses.FirstOrDefault()?.Rue,
                Apartment = user.Adresses.FirstOrDefault()?.Appartement,
                City = user.Adresses.FirstOrDefault()?.Ville,
                Province = user.Adresses.FirstOrDefault()?.Province,
                Country = user.Adresses.FirstOrDefault()?.Pays,
                PostalCode = user.Adresses.FirstOrDefault()?.CodePostal,
                Photo = string.IsNullOrEmpty(user.Avatar) 
                    ? "/Avatars/default.png" 
                    : $"/Avatars/{user.Avatar}"
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
        [HttpPut("MiseAJourInfoClient")]
        public async Task<IActionResult> UpdateClientInfo([FromBody] UpdateClientInfoVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var (success, message, updatedUser) = await _modificationService.MettreAJourClient(userId, model);

            if (success)
            {
                return Ok(new { success = true, message = message, user = updatedUser });
            }
            else
            {
                return BadRequest(new { success = false, message = message });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpPut("avatar")]
        public async Task<IActionResult> UpdateAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
            {
                return BadRequest("Aucun fichier n'a été envoyé ou le fichier est vide.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);



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

            if (avatar != null && avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Avatars");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }

                user.Avatar = uniqueFileName;
                await _userManager.UpdateAsync(user);
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

                return Ok(new { avatarUrl = $"/Avatars/{uniqueFileName}" });
            }
        var result = await _userManager.UpdateAsync(user);

            return BadRequest("Aucun fichier n'a été envoyé.");
        }
        if (!result.Succeeded)
        {
            return BadRequest("Erreur lors de la mise à jour des informations.");
        }

        [HttpGet("verifier-email")]
        public async Task<IActionResult> VerifierEmail([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new { disponible = user == null });
        }
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
