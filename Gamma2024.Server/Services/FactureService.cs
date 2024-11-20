using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class FactureService
    {
        private readonly ApplicationDbContext _context;

        public FactureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageVM()
            {
                Id = f.Id,
                IdClient = f.IdClient,
                DateAchat = f.DateAchat,
                Lots = f.Lots,
                Encan = f.NumeroEncan,
            }).ToList();

            foreach (FactureAffichageVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
                facture.Pseudonyme = client.UserName;
                facture.Courriel = client.Email;
                facture.Telephone = client.PhoneNumber;
            }

            return factures;
        }

        public ICollection<Facture> ChercherFacturesParEncan(int numeroEncan)
        {
            var factures = _context.Factures.Include(f => f.Client).Include(f => f.Lots).Where(f => f.NumeroEncan == numeroEncan);
            return factures.ToList();
        }

        public ICollection<FactureAffichageMembreVM> ChercherFacturesMembre(string id)
        {
            var factures = _context.Factures.Where(c => c.IdClient == id).Select(f => new FactureAffichageMembreVM()
            {
                Id = f.Id,
                IdClient = id,
                DateAchat = f.DateAchat,
                Lots = f.Lots,
                Encan = f.NumeroEncan,
            }).ToList();

            return factures;
        }
    }
}
