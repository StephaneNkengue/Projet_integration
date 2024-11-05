using Gamma2024.Server.Data;
using Gamma2024.Server.ViewModels;

namespace Gamma2024.Server.Services
{
    public class FactureService
    {
        private readonly ApplicationDbContext _context;

        public FactureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<FactureAffichageParEncanVM> ChercherFacturesParEncan()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageParEncanVM()
            {
                IdClient = f.IdClient,
                Lots = f.Lots,
            }).ToList();

            foreach (FactureAffichageParEncanVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
            }

            return factures;
        }

        public ICollection<FactureAffichageParClientVM> ChercherFacturesParClient()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageParClientVM()
            {
                IdClient = f.IdClient
            }).ToList();

            foreach (FactureAffichageParClientVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
            }

            return factures.OrderBy(f => f.Prenom).ToList();
        }

        public ICollection<FactureAffichageParDateVM> ChercherFacturesParDate()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageParDateVM()
            {
                IdClient = f.IdClient,
                DateAchat = f.DateAchat
            }).ToList();

            foreach (FactureAffichageParDateVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
            }

            return factures.OrderBy(f => f.DateAchat).ToList();
        }
    }
}
