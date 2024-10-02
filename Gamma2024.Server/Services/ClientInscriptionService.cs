using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Validations;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class ClientInscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientInscriptionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(bool Success, string Message)> InscrireUtilisateur(InscriptionVM model)
        {
            // Vérification des données
            var (isValid, errorMessage) = ClientValidation.ValidateInscription(model);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            // Vérifier l'unicité du pseudonyme
            var existingUserByUsername = await _userManager.FindByNameAsync(model.GeneralInfo.Pseudo);
            if (existingUserByUsername != null)
            {
                return (false, "Ce pseudonyme est déjà utilisé. Veuillez en choisir un autre.");
            }

            // Vérifier l'unicité de l'email
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.GeneralInfo.Courriel);
            if (existingUserByEmail != null)
            {
                return (false, "Cette adresse email est déjà utilisée.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Vérifier et convertir la date d'expiration
                var (isValidExpiration, moisExpiration, anneeExpiration) = ClientValidation.ValidateAndParseExpirationDate(model.CarteCredit.DateExpiration);
                if (!isValidExpiration)
                {
                    return (false, "La date d'expiration de la carte est invalide ou expirée.");
                }

                // Créer le client (ApplicationUser)
                var client = new ApplicationUser
                {
                    UserName = model.GeneralInfo.Pseudo,
                    NormalizedUserName = model.GeneralInfo.Pseudo.ToUpper(),
                    Email = model.GeneralInfo.Courriel,
                    EmailConfirmed = true,
                    NormalizedEmail = model.GeneralInfo.Courriel.ToUpper(),
                    Name = model.GeneralInfo.Nom,
                    FirstName = model.GeneralInfo.Prenom,
                    PhoneNumber = model.GeneralInfo.Telephone,
                    PhoneNumberConfirmed = true,
                };

                var result = await _userManager.CreateAsync(client, model.GeneralInfo.MotDePasse);

                if (!result.Succeeded)
                {
                    return (false, "Erreur lors de la création de l'utilisateur : " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                // Ajouter le rôle CLIENT
                await _userManager.AddToRoleAsync(client, "CLIENT");

                // Créer la carte de crédit
                var carteCredit = new Models.CarteCredit
                {
                    Nom = model.CarteCredit.NomProprio,
                    Numero = model.CarteCredit.NumeroCarte,
                    MoisExpiration = moisExpiration,
                    AnneeExpiration = anneeExpiration,
                    IdClient = client.Id
                };
                _context.CartesCredits.Add(carteCredit);

                // Créer l'adresse
                var adresse = new Models.Adresse
                {
                    Numero = int.Parse(model.Adresse.NumeroCivique),
                    Rue = model.Adresse.Rue,
                    Appartement = model.Adresse.Appartement,
                    Ville = model.Adresse.Ville,
                    Province = model.Adresse.Province,
                    Pays = model.Adresse.Pays,
                    CodePostal = model.Adresse.CodePostal,
                    EstDomicile = true,
                    IdApplicationUser = client.Id
                };
                _context.Adresses.Add(adresse);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Inscription réussie");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, "Une erreur est survenue lors de l'inscription : " + ex.Message);
            }
        }

        private async Task<bool> IsUsernameUnique(string username)
        {
            return !await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}