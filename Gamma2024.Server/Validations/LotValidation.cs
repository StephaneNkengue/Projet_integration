using Gamma2024.Server.ViewModels;
using Gamma2024.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gamma2024.Server.Validations
{
    public static class LotValidation
    {
        public static async Task<(bool IsValid, string ErrorMessage)> ValidateLot(LotCreationVM lot, ApplicationDbContext context)
        {
            if (string.IsNullOrWhiteSpace(lot.Code))
                return (false, "Le code du lot est obligatoire.");

            // La validation pour Nom a été supprimée

            if (string.IsNullOrWhiteSpace(lot.Description))
                return (false, "La description du lot est obligatoire.");

            if (lot.ValeurEstimeMin <= 0)
                return (false, "La valeur estimée minimale doit être supérieure à zéro.");

            if (lot.ValeurEstimeMax <= lot.ValeurEstimeMin)
                return (false, "La valeur estimée maximale doit être supérieure à la valeur minimale.");

            if (string.IsNullOrWhiteSpace(lot.Artiste))
                return (false, "Le nom de l'artiste est obligatoire.");

            if (lot.IdCategorie <= 0)
                return (false, "L'ID de la catégorie est invalide.");

            // Vérification de l'existence du vendeur
            var vendeurExists = await context.Vendeurs.AnyAsync(v => v.Id == lot.IdVendeur);
            if (!vendeurExists)
                return (false, "Le vendeur spécifié n'existe pas.");

            if (lot.IdMedium <= 0)
                return (false, "L'ID du medium est invalide.");

            return (true, null);
        }

        // Vous pouvez ajouter une méthode similaire pour LotModificationVM si nécessaire
    }
}
