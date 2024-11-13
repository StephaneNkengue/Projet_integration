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

        public ICollection<FactureAffichageVM> ChercherFactures()
        {
            var factures = _context.Factures.Select(f => new FactureAffichageVM()
            {
                Id = f.Id,
                IdClient = f.IdClient,
                DateAchat = f.DateAchat,
                Lots = f.Lots,
            }).ToList();

            foreach (FactureAffichageVM facture in factures)
            {
                var client = _context.Users.FirstOrDefault(c => c.Id == facture.IdClient);
                facture.Nom = client.Name;
                facture.Prenom = client.FirstName;
                facture.Pseudonyme = client.UserName;
                facture.Courriel = client.Email;
                facture.Telephone = client.PhoneNumber;

                var numerosEncans = _context.Factures.Where(f => f.Id == facture.Id)
                                                     .SelectMany(f => f.Lots)
                                                     .SelectMany(l => l.EncanLots)
                                                     .Select(el => el.Encan.NumeroEncan)
                                                     .Distinct()
                                                     .ToList();

                foreach (var numero in numerosEncans)
                {
                    facture.Encan = numero.ToString();
                }
            }

            return factures;
        }
    }
}
