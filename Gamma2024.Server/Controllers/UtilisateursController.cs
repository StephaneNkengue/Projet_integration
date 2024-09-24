using Gamma2024.Server.Services;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ClientInscriptionService _inscriptionService;
        private readonly ClientModificationService _modificationService;
        private readonly UserManager<IdentityUser> _userManager;

        public UtilisateursController(ClientInscriptionService inscriptionService, ClientModificationService modificationService, UserManager<IdentityUser> userManager)
        {
            _inscriptionService = inscriptionService;
            _modificationService = modificationService;
            _userManager = userManager;
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
                return Ok(new { success = true, message = message, user = model, roles = new List<string> { "Client" } });
            }
            else
            {
                return BadRequest(new { success = false, message = message });
            }
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
            }

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

}
