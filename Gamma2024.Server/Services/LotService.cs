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
                .Include(l => l.EncanLots)
                .Include(l => l.Photos)
                .Where(l => l.EncanLots.FirstOrDefault(el => el.IdEncan == idEncan) != null)
                .Select(l => new LotAffichageVM()
                {
                    Id = l.Id,
                    Numero = l.Numero,
                    ValeurEstimeMax = l.ValeurEstimeMax,
                    ValeurEstimeMin = l.ValeurEstimeMin,
                    Artiste = l.Artiste,
                    Mise = l.Mise,
                    EstVendu = l.EstVendu,
                    DateFinVente = l.DateFinVente,
                    Photos = l.Photos,
                    Description = l.Description,
                    EstLivrable = l.EstLivrable,
                    Hauteur = l.Hauteur,
                    Largeur = l.Largeur
                });

            return lots.ToList();
        }

        public ICollection<LotAffichageAdministrateurVM> ChercherTousLots()
        {
            var lots = _context.Lots
                .Select(l => new LotAffichageAdministrateurVM()
                {
                    Id = l.Id,
                    Encan = _context.Encans.Where(e => e.Id == (_context.EncanLots.Where(e => e.IdLot == l.Id).Max(e => e.IdEncan))).Single().NumeroEncan,
                    Numero = l.Numero,
                    PrixOuverture = l.PrixOuverture.ToString() + " $",
                    ValeurMinPourVente = l.PrixMinPourVente.ToString() + " $",
                    ValeurEstimeMax = l.ValeurEstimeMax.ToString() + " $",
                    ValeurEstimeMin = l.ValeurEstimeMin.ToString() + " $",
                    Categorie = _context.Categories.Where(c => c.Id == l.IdCategorie).Single().Nom,
                    Artiste = l.Artiste,
                    Description = l.Description,
                    Hauteur = l.Hauteur,
                    Largeur = l.Largeur,
                    Medium = _context.Mediums.Where(m => m.Id == l.IdMedium).Single().Type,
                    Vendeur = l.Vendeur.Prenom + " " + l.Vendeur.Nom,
                    EstVendu = l.EstVendu,
                    EstLivrable = l.EstLivrable,
                }).ToList();

            return lots;
        }
    }
}
