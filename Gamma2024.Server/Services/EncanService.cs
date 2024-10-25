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
                    DateDebutSoireeCloture = e.DateDebutSoireeCloture,
                    EstPublie = e.EstPublie

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

        public async Task<(bool Success, string Message)> CreerEncan(EncanVM vm)
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

        public Encan GetEncanByNumber(int numero)
        {
            return _context.Encans.FirstOrDefault(e => e.NumeroEncan == numero);
        }

        public Encan GetEncanById(int idEncan)
        {
            return _context.Encans.FirstOrDefault(e => e.Id == idEncan);
        }

        public async Task<(bool success, object message)> ModifierEncan(int id, EncanVM model)
        {
            var (isValid, errorMessage) = EncanValidation.ValidateEncan(model);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            var encan = await _context.Encans.FindAsync(id);
            if (encan == null)
            {
                return (false, "Encan non trouvé");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                encan.NumeroEncan = model.NumeroEncan;
                encan.DateDebut = model.DateDebut;
                encan.DateFin = model.DateFin;


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (true, "Encan modifié avec succès");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Une erreur est survenue lors de la modification de l'encan : {ex.Message}");
            }
        }
    }
}
