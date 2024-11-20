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

        public ICollection<Facture> CreerFacturesParEncan(int numeroEncan)
        {
            var idEncan = _context.Encans.First(e => e.NumeroEncan == numeroEncan).Id;
            var lots = _context.EncanLots
                .Include(el => el.Lot)
                .Include(el => el.Lot.ClientMise)
                .Where(el => el.IdEncan == idEncan)
                .Select(el => el.Lot).ToList();
            var factures = _context.Factures.Include(f => f.Lots).Where(f => f.NumeroEncan == numeroEncan).ToList();

            foreach (var lot in lots)
            {
                if (lot.EstVendu && lot.IdFacture == null)
                {
                    var facture = factures.FirstOrDefault(f => f.IdClient == lot.IdClientMise);

                    if (facture != null && !facture.estPaye)
                    {
                        facture.Lots.Add(lot);
                    }
                    else
                    {
                        factures.Add(new Facture
                        {
                            IdClient = lot.IdClientMise,
                            DateAchat = lot.DateFinVente.Value,
                            Lots = [lot],
                            Client = lot.ClientMise,
                            NumeroEncan = numeroEncan
                        });
                    }
                }
            }

            factures.ForEach(f => f.CalculerFacture());
            _context.Factures.UpdateRange(factures);
            _context.SaveChanges();

            return factures;
        }

        public bool PayerFacture(int idFacture)
        {
            var facture = _context.Factures.FirstOrDefault(f => f.Id == idFacture);

            if (facture == null)
            {
                return false;
            }

            facture.estPaye = true;
            _context.SaveChanges();
            return true;
        }

        public Facture ChercherFacture(int idFacture)
        {
            var facture = _context.Factures.Include(f => f.Lots).FirstOrDefault(f => f.Id == idFacture);

            if (facture == null)
            {
                return null;
            }
            return facture;
        }
    }
}
