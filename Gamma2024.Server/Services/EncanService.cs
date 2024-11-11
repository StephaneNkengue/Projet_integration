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
                }).OrderByDescending(e => e.DateDebut.Year).ThenByDescending(e => e.DateDebut.Month).ThenByDescending(e => e.DateDebut.Day).ToList();

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

        public async Task<(bool Success, string Message)> CreerEncan(EncanVM vm)
        {
            var (isValid, errorMessage) = EncanValidation.ValidateEncan(vm);

            if (!isValid)
            {
                return (false, errorMessage);
            }

            var listeEncan = _context.Encans.OrderBy(e => e.NumeroEncan).ToList();

            bool estVide = false;
            if (listeEncan.Count == 0)
            {
                estVide = true;
            }

            var encan = new Encan()
            {
                Id = 0,
                NumeroEncan = estVide ? 1 : listeEncan.LastOrDefault().NumeroEncan + 1,
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

        public Encan GetEncanByNumber(int numero)
        {
            return _context.Encans.FirstOrDefault(e => e.NumeroEncan == numero);
        }

        public Encan GetEncanById(int idEncan)
        {
            return _context.Encans.FirstOrDefault(e => e.Id == idEncan);
        }

        public async Task<(bool success, string message)> ModifierEncan(int id, EncanVM model)
        {
            var (isValid, message) = EncanValidation.ValidateEncan(model);
            if (!isValid)
            {
                return (false, message);
            }

            var encan = await _context.Encans.FindAsync(id);
            if (encan == null)
            {
                return (false, "Encan non trouvé");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
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
