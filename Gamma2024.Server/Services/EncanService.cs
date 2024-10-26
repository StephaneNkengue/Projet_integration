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
        public EncanAffichageVM ChercherEncanParNumero(int numero)
        {
            var encan = _context.Encans
                .FirstOrDefault(e => e.NumeroEncan == numero);

            if (encan != null && encan.EstPublie)
            {
                return new EncanAffichageVM()
                {
                    Id = encan.Id,
                    NumeroEncan = encan.NumeroEncan,
                    DateDebut = encan.DateDebut,
                    DateFin = encan.DateFin,
                    DateFinSoireeCloture = encan.DateFinSoireeCloture,
                    DateDebutSoireeCloture = encan.DateDebutSoireeCloture
                };

            }

            return null;
        }

        public EncanAffichageVM ChercherEncanEnCours()
        {
            var encan = _context.Encans
                .FirstOrDefault(e => DateTime.Now < e.DateFinSoireeCloture && DateTime.Now > e.DateDebut);

            if (encan != null && encan.EstPublie)
            {
                return new EncanAffichageVM()
                {
                    Id = encan.Id,
                    NumeroEncan = encan.NumeroEncan,
                    DateDebut = encan.DateDebut,
                    DateFin = encan.DateFin,
                    DateFinSoireeCloture = encan.DateFinSoireeCloture,
                    DateDebutSoireeCloture = encan.DateDebutSoireeCloture
                };

            }

            return null;
        }

        public int ChercherNumeroEncanEnCours()
        {
            var encan = _context.Encans
                .FirstOrDefault(e => DateTime.Now < e.DateFinSoireeCloture && DateTime.Now > e.DateDebut);

            if (encan != null && encan.EstPublie)
            {
                return encan.NumeroEncan;

            }

            return 0;
        }

        public ICollection<EncanAffichageVM> ChercherEncansFuturs()
        {
            var encans = _context.Encans
                .Where(e => DateTime.Now < e.DateDebut)
                .Where(e => e.EstPublie == true)
                .OrderBy(e => e.DateDebut)
                .ToList()
                .Select(e => new EncanAffichageVM
                {
                    Id = e.Id,
                    NumeroEncan = e.NumeroEncan,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    DateFinSoireeCloture = e.DateFinSoireeCloture,
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture
                })
                .ToList();

            return encans;
        }

        public ICollection<EncanAffichageVM> ChercherEncansPasses()
        {
            var encans = _context.Encans
                .Where(e => DateTime.Now > e.DateFinSoireeCloture)
                .Where(e => e.EstPublie == true)
                .OrderByDescending(e => e.DateFinSoireeCloture)
                .ToList()
                .Select(e => new EncanAffichageVM
                {
                    Id = e.Id,
                    NumeroEncan = e.NumeroEncan,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    DateFinSoireeCloture = e.DateFinSoireeCloture,
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture
                })
                .ToList();

            return encans;
        }
    }
}
