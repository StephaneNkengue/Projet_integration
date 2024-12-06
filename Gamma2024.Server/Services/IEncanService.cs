using Gamma2024.Server.ViewModels;
using Gamma2024.Server.Models;

namespace Gamma2024.Server.Services
{
    public interface IEncanService
    {
        // Méthodes de gestion des encans
        ICollection<EncanAffichageAdminVM> ChercherTousEncans();
        ICollection<EncanAffichageAdminVM> ChercherTousEncansVisibles();
        Task<(bool Success, string Message)> CreerEncan(EncanVM vm);
        Task<(bool Success, string Message)> MettreAJourEncanPublie(EncanPublieVM vm);
        Encan GetEncanByNumber(int numero);
        Encan GetEncanById(int idEncan);
        Task<(bool success, string message)> ModifierEncan(int id, EncanVM model);
        EncanAffichageVM ChercherEncanParNumero(int numero);
        EncanAffichageVM ChercherEncanEnCours();
        int ChercherNumeroEncanEnCours();
        ICollection<EncanAffichageVM> ChercherEncansFuturs();
        ICollection<EncanAffichageVM> ChercherEncansPasses();
        Task<(string type, Encan encan)> GetEtatCourant();
        Task<bool> EstEnSoireeCloture(int numeroEncan);
    }
}