using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Gamma2024.Server.Validations;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class LotService
    {
        private readonly ApplicationDbContext _context;

        public LotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LotAffichageVM>> ObtenirTousLots()
        {
            return await _context.Lots
                .Include(l => l.Photos)
                .Select(l => new LotAffichageVM
                {
                    Id = l.Id,
                    Code = l.Code,
                    ValeurEstimeMin = l.ValeurEstimeMin,
                    ValeurEstimeMax = l.ValeurEstimeMax,
                    Artiste = l.Artiste,
                    Mise = l.Mise,
                    EstVendu = l.EstVendu,
                    DateFinVente = l.DateFinVente,
                    Photos = l.Photos
                })
                .ToListAsync();
        }

        public async Task<LotAffichageVM> ObtenirLot(int id)
        {
            var lot = await _context.Lots
                .Include(l => l.Photos)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lot == null)
            {
                return null;
            }

            return new LotAffichageVM
            {
                Id = lot.Id,
                Code = lot.Code,
                ValeurEstimeMin = lot.ValeurEstimeMin,
                ValeurEstimeMax = lot.ValeurEstimeMax,
                Artiste = lot.Artiste,
                Mise = lot.Mise,
                EstVendu = lot.EstVendu,
                DateFinVente = lot.DateFinVente,
                Photos = lot.Photos
            };
        }

        public async Task<(bool Success, string Message, LotAffichageVM Lot)> CreerLot(LotCreationVM lotVM)
        {
            var (isValid, errorMessage) = await LotValidation.ValidateLot(lotVM, _context);
            if (!isValid)
            {
                return (false, errorMessage, null);
            }

            var lot = new Lot
            {
                Code = lotVM.Code,
                Nom = lotVM.Nom,
                Description = lotVM.Description,
                ValeurEstimeMin = lotVM.ValeurEstimeMin,
                ValeurEstimeMax = lotVM.ValeurEstimeMax,
                Artiste = lotVM.Artiste,
                IdCategorie = lotVM.IdCategorie,
                IdVendeur = lotVM.IdVendeur,
                estLivrable = lotVM.EstLivrable,
                Dimensions = lotVM.Dimensions,
                IdMedium = lotVM.IdMedium
            };

            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();

            var lotAffichage = new LotAffichageVM
            {
                Id = lot.Id,
                Code = lot.Code,
                ValeurEstimeMin = lot.ValeurEstimeMin,
                ValeurEstimeMax = lot.ValeurEstimeMax,
                Artiste = lot.Artiste,
                Mise = lot.Mise,
                EstVendu = lot.EstVendu,
                DateFinVente = lot.DateFinVente,
                Photos = new List<Photo>()
            };

            return (true, "Lot créé avec succès", lotAffichage);
        }

        public async Task<(bool Success, string Message)> ModifierLot(int id, LotModificationVM lotVM)
        {
            var (isValid, errorMessage) = LotValidation.ValidateLot(lotVM);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return (false, "Lot non trouvé");
            }

            lot.Code = lotVM.Code;
            lot.Nom = lotVM.Nom;
            lot.Description = lotVM.Description;
            lot.ValeurEstimeMin = lotVM.ValeurEstimeMin;
            lot.ValeurEstimeMax = lotVM.ValeurEstimeMax;
            lot.Artiste = lotVM.Artiste;
            lot.IdCategorie = lotVM.IdCategorie;
            lot.IdVendeur = lotVM.IdVendeur;
            lot.estLivrable = lotVM.EstLivrable;
            lot.Dimensions = lotVM.Dimensions;
            lot.IdMedium = lotVM.IdMedium;

            await _context.SaveChangesAsync();

            return (true, "Lot modifié avec succès");
        }

        public async Task<(bool Success, string Message)> SupprimerLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return (false, "Lot non trouvé");
            }

            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();

            return (true, "Lot supprimé avec succès");
        }
    }
}
