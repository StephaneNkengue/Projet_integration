using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class InscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InscriptionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<(bool Success, string Message)> InscrireUtilisateur(InscriptionVM model)
        {
            // Vérification des données
            var (isValid, errorMessage) = ValidateInscription(model);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            // Vérifier l'unicité du pseudonyme
            if (!await IsUsernameUnique(model.GeneralInfo.Pseudo))
            {
                return (false, "Ce pseudonyme est déjà utilisé. Veuillez en choisir un autre.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Vérifier et convertir la date d'expiration
                var (isValidExpiration, moisExpiration, anneeExpiration) = ValidateAndParseExpirationDate(model.CarteCredit.DateExpiration);
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
                    EmailConfirmed = false,
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
                    Numero = ParseNumeroCarteCredit(model.CarteCredit.NumeroCarte),
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
                    Pays = model.Adresse.Pays,
                    CodePostal = ParseCodePostal(model.Adresse.CodePostal),
                    EstDomicile = true,
                    IdApplicationUser = client.Id
                };
                _context.Adresses.Add(adresse);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Inscription réussie, un email de confirmation a été envoyé.");
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

        private (bool IsValid, int Mois, int Annee) ValidateAndParseExpirationDate(string expirationDate)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(expirationDate, @"^(0[1-9]|1[0-2])\/([0-9]{2})$"))
            {
                return (false, 0, 0);
            }

            var parts = expirationDate.Split('/');
            int mois = int.Parse(parts[0]);
            int annee = int.Parse(parts[1]) + 2000; // Convertir '26' en '2026'

            var dateExpiration = new DateTime(annee, mois, 1).AddMonths(1).AddDays(-1);
            var debutMoisProchain = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1);

            if (dateExpiration < debutMoisProchain)
            {
                return (false, 0, 0);
            }

            // Vérifier que l'année n'est pas trop loin dans le futur (par exemple, pas plus de 10 ans)
            if (annee > DateTime.Today.Year + 10)
            {
                return (false, 0, 0);
            }

            return (true, mois, annee);
        }


        private string ParseCodePostal(string code)
        {
            var parts = code.Split(' ');
            return parts[0] + parts[1];
        }

        private string ParseNumeroCarteCredit(string numeroCarte)
        {
            var parts = numeroCarte.Split(' ');
            return parts[0] + parts[1] + parts[2] + parts[3];
        }

        private (bool IsValid, string ErrorMessage) ValidateInscription(InscriptionVM model)
        {
            if (model == null)
            {
                return (false, "Les données d'inscription sont manquantes.");
            }

            if (model.GeneralInfo == null)
            {
                return (false, "Les informations générales sont manquantes.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.Nom))
            {
                return (false, "Le nom est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.Prenom))
            {
                return (false, "Le prénom est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.Courriel) || !IsValidEmail(model.GeneralInfo.Courriel))
            {
                return (false, "L'adresse courriel est invalide ou manquante.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.Telephone) || !IsValidPhoneNumber(model.GeneralInfo.Telephone))
            {
                return (false, "Le numéro de téléphone est invalide ou manquant.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.Pseudo))
            {
                return (false, "Le pseudo est obligatoire.");
            }

            if (model.GeneralInfo.Pseudo.Length < 3 || model.GeneralInfo.Pseudo.Length > 20)
            {
                return (false, "Le pseudo doit contenir entre 3 et 20 caractères.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(model.GeneralInfo.Pseudo, "^[a-zA-Z0-9_]+$"))
            {
                return (false, "Le pseudo ne peut contenir que des lettres, des chiffres et des underscores.");
            }

            if (string.IsNullOrWhiteSpace(model.GeneralInfo.MotDePasse) || model.GeneralInfo.MotDePasse.Length < 8)
            {
                return (false, "Le mot de passe doit contenir au moins 8 caractères.");
            }



            if (model.GeneralInfo.MotDePasse != model.GeneralInfo.ConfirmMotPasse)
            {
                return (false, "Les mots de passe ne correspondent pas.");
            }

            if (model.CarteCredit == null)
            {
                return (false, "Les informations de carte de crédit sont manquantes.");
            }

            if (string.IsNullOrWhiteSpace(model.CarteCredit.NomProprio))
            {
                return (false, "Le nom du propriétaire de la carte est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.CarteCredit.NumeroCarte) || !IsValidCreditCardNumber(model.CarteCredit.NumeroCarte))
            {
                return (false, "Le numéro de carte de crédit est invalide ou manquant.");
            }

            if (string.IsNullOrWhiteSpace(model.CarteCredit.DateExpiration))
            {
                return (false, "La date d'expiration de la carte est manquante.");
            }

            var (isValidExpiration, _, _) = ValidateAndParseExpirationDate(model.CarteCredit.DateExpiration);
            if (!isValidExpiration)
            {
                return (false, "La date d'expiration de la carte est invalide ou expirée.");
            }

            if (model.Adresse == null)
            {
                return (false, "Les informations d'adresse sont manquantes.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.NumeroCivique))
            {
                return (false, "Le numéro civique est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.Rue))
            {
                return (false, "La rue est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.Ville))
            {
                return (false, "La ville est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.Province))
            {
                return (false, "La province est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.Pays))
            {
                return (false, "Le pays est obligatoire.");
            }

            if (string.IsNullOrWhiteSpace(model.Adresse.CodePostal) || !IsValidPostalCode(model.Adresse.CodePostal))
            {
                return (false, "Le code postal est invalide ou manquant.");
            }

            return (true, null);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        }

        private bool IsValidCreditCardNumber(string cardNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(cardNumber, @"^(?:4[0-9]{3} [0-9]{4} [0-9]{4} [0-9]{4}|5[1-5][0-9]{2} [0-9]{4} [0-9]{4} [0-9]{4}|6(?:011|5[0-9][0-9]) [0-9]{4} [0-9]{4} [0-9]{4}|3[47][0-9]{2} [0-9]{6} [0-9]{5}|3(?:0[0-5]|[68][0-9]) [0-9]{4} [0-9]{6} [0-9]{4}|(?:2131|1800|35\d{3}) [0-9]{4} [0-9]{4} [0-9]{4})$");
        }

        private bool IsValidPostalCode(string postalCode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(postalCode,
                @"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvwxyz]\d[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvwxyz][ -]?\d[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvwxyz]\d$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}
