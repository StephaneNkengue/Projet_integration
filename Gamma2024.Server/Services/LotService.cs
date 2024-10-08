using Gamma2024.Server.Data;
using Gamma2024.Server.ViewModels;
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

        public ICollection<LotAffichageVM> ChercherTousLotsParEncan(int idEncan)
        {
            var lots = _context.Lots
                .Include(l => l.EncanLots.Where(el => el.IdEncan == idEncan))
                .Include(l => l.Photos)
                .Select(l => new LotAffichageVM()
                {
                    Id = l.Id,
                    Code = l.Numero,
                    ValeurEstimeMax = l.ValeurEstimeMax,
                    ValeurEstimeMin = l.ValeurEstimeMin,
                    Artiste = l.Artiste,
                    Mise = l.Mise,
                    EstVendu = l.EstVendu,
                    DateFinVente = l.DateFinVente,
                    Photos = l.Photos,
                });

            return lots.ToList();
        }
    }
}
