using Gamma2024.Server.Data;
using Gamma2024.Server.ViewModels;

namespace Gamma2024.Server.Services
{
    public class EncanService
    {
        private readonly ApplicationDbContext _context;

        public EncanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<EncanAffichageVM> ChercherTousEncans()
        {
            var encans = _context.Encans
                .Select(e => new EncanAffichageVM()
                {
                    Id = e.Id,
                    NumeroEncan = e.NumeroEncan,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    DateFinSoireeCloture = e.DateFinSoireeCloture,
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture
                }).ToList();

            return encans;
        }

        public ICollection<EncanAffichageVM> ChercherTousEncansVisibles()
        {
            var encans = _context.Encans
                .Where(e => e.EstPublie == true)
                .Select(e => new EncanAffichageVM()
                {
                    Id = e.Id,
                    NumeroEncan = e.NumeroEncan,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    DateFinSoireeCloture = e.DateFinSoireeCloture,
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture
                }).ToList();

            return encans;
        }
    }
}
