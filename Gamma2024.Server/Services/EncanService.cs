using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Validations;
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

        public ICollection<EncanAffichageAdminVM> ChercherTousEncans()
        {
            var encans = _context.Encans
                .Select(e => new EncanAffichageAdminVM()
                {
                    Id = e.Id,
                    NumeroEncan = e.NumeroEncan,
                    DateDebut = e.DateDebut,
                    DateFin = e.DateFin,
                    DateFinSoireeCloture = e.DateFinSoireeCloture,
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture,
                    EstPublie = e.EstPublie,
                    NbLots = _context.EncanLots.Count(el => el.IdEncan == e.Id)
                }).ToList();

            return encans;
        }

        public ICollection<EncanAffichageAdminVM> ChercherTousEncansVisibles()
        {
            var encans = _context.Encans
                .Where(e => e.EstPublie == true)
                .Select(e => new EncanAffichageAdminVM()
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

        public async Task<(bool Success, string Message)> CreerEncan(EncanCreerVM vm)
        {
            var (isValid, errorMessage) = EncanValidation.ValidateEncan(vm);

            if (!isValid)
            {
                return (false, errorMessage);
            }

            var numeroEncanExist = _context.Encans.FirstOrDefault(e => e.NumeroEncan == vm.NumeroEncan);
            if (numeroEncanExist != null)
            {
                return (false, "Le numéro d'encan existe déjà.");
            }

            var encan = new Encan()
            {
                Id = 0,
                NumeroEncan = vm.NumeroEncan,
                DateDebut = vm.DateDebut,
                DateFin = vm.DateFin,
                DateDebutSoireeCloture = vm.DateFin,
                DateFinSoireeCloture = vm.DateFin.AddDays(1), //à modifier, doit être dynamique
                EncanLots = [],
                EstPublie = false,
            };

            _context.Encans.Add(encan);
            _context.SaveChanges();
            return (true, encan.Id.ToString());
        }

        public async Task<(bool Success, string Message)> MettreAJourEncanPublie(EncanPublieVM vm)
        {
            var numeroEncan = _context.Encans.FirstOrDefault(e => e.NumeroEncan.Equals(vm.NumeroEncan));
            if (numeroEncan != null)
            {
                numeroEncan.NumeroEncan = vm.NumeroEncan;
                numeroEncan.EstPublie = vm.EstPublie;
                _context.Encans.Update(numeroEncan);
                _context.SaveChanges();
                return (true, numeroEncan.Id.ToString());
            }

            return (false, "Il n'y a aucun encan à ce numéro.");
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
    }
}