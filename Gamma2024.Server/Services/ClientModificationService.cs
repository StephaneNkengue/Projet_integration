using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Gamma2024.Server.Validations;
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

        public async Task<(bool Success, string Message)> MettreAJourClient(string userId, UpdateClientInfoVM model)
        {
            // Vérification des données
            var (isValid, errorMessage) = ClientValidation.ValidateClientUpdate(model);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return (false, "Utilisateur non trouvé.");
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
                    return (false, "La date d'expiration de la carte est invalide ou expirée.");
                }
                carteCredit.MoisExpiration = moisExpiration;
                carteCredit.AnneeExpiration = anneeExpiration;
            }

            // Mettre à jour les informations d'adresse
            var adresse = await _context.Adresses.FirstOrDefaultAsync(a => a.IdApplicationUser == user.Id && a.EstDomicile);
            if (adresse != null)
            {
                adresse.Numero = int.Parse(model.CivicNumber);
                adresse.Rue = model.Street;
                adresse.Appartement = model.Apartment;
                adresse.Ville = model.City;
                adresse.Province = model.Province;
                adresse.Pays = model.Country;
                adresse.CodePostal = model.PostalCode;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return (false, "Erreur lors de la mise à jour des informations : " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _context.SaveChangesAsync();

            return (true, "Informations mises à jour avec succès.");
        }
    }
}