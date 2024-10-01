using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Validations;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class ClientModificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientModificationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(bool Success, string Message, object UpdatedUser)> MettreAJourClient(string userId, UpdateClientInfoVM model)
        {
            // Vérification des données
            var (isValid, errorMessage) = ClientValidation.ValidateClientUpdate(model);
            if (!isValid)
            {
                return (false, errorMessage, null);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (false, "Utilisateur non trouvé.", null);
            }

            // Mettre à jour les informations générales
            user.Name = model.Name;
            user.FirstName = model.FirstName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Pseudonym;

            // Mettre à jour les informations de carte de crédit
            var carteCredit = await _context.CartesCredits.FirstOrDefaultAsync(c => c.IdClient == user.Id);
            if (carteCredit != null)
            {
                carteCredit.Nom = model.CardOwnerName;
                carteCredit.Numero = model.CardNumber;
                var (isValidExpiration, moisExpiration, anneeExpiration) = ClientValidation.ValidateAndParseExpirationDate(model.CardExpiryDate);
                if (!isValidExpiration)
                {
                    return (false, "La date d'expiration de la carte est invalide ou expirée.", null);
                }
                carteCredit.MoisExpiration = moisExpiration;
                carteCredit.AnneeExpiration = anneeExpiration;
            }
            else
            {
                return (false, "Aucune carte de crédit trouvée pour cet utilisateur.", null);
            }

            // Mettre à jour les informations d'adresse
            var adresse = await _context.Adresses.FirstOrDefaultAsync(a => a.IdApplicationUser == user.Id && a.EstDomicile);
            if (adresse != null)
            {
                if (!int.TryParse(model.CivicNumber, out int numeroCivique))
                {
                    return (false, "Le numéro civique doit être un nombre entier.", null);
                }
                adresse.Numero = numeroCivique;
                adresse.Rue = model.Street;
                adresse.Appartement = model.Apartment;
                adresse.Ville = model.City;
                adresse.Province = model.Province;
                adresse.Pays = model.Country;
                adresse.CodePostal = model.PostalCode;
            }
            else
            {
                return (false, "Aucune adresse trouvée pour cet utilisateur.", null);
            }

            // Gestion du mot de passe
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (string.IsNullOrEmpty(model.CurrentPassword))
                {
                    return (false, "Le mot de passe actuel est requis pour changer le mot de passe.", null);
                }

                var passwordCheckResult = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (!passwordCheckResult)
                {
                    return (false, "Mot de passe actuel incorrect.", null);
                }

                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);

                if (!passwordValidationResult.Succeeded)
                {
                    return (false, string.Join(", ", passwordValidationResult.Errors.Select(e => e.Description)), null);
                }

                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    return (false, "Erreur lors de la mise à jour du mot de passe : " + string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description)), null);
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return (false, "Erreur lors de la mise à jour des informations : " + string.Join(", ", result.Errors.Select(e => e.Description)), null);
            }

            await _context.SaveChangesAsync();

            var updatedUser = new
            {
                user.Name,
                user.FirstName,
                user.Email,
                user.PhoneNumber,
                user.UserName,
                CardOwnerName = carteCredit.Nom,
                CardNumber = carteCredit.Numero,
                CardExpiryDate = $"{carteCredit.MoisExpiration:D2}/{carteCredit.AnneeExpiration}",
                CivicNumber = adresse.Numero.ToString(),
                Street = adresse.Rue,
                Apartment = adresse.Appartement,
                City = adresse.Ville,
                Province = adresse.Province,
                Country = adresse.Pays,
                PostalCode = adresse.CodePostal
            };

            return (true, "Informations mises à jour avec succès.", updatedUser);
        }
    }
}