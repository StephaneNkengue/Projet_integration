using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;

namespace Gamma2024.Server.Services
{
    public class FactureLivraisonService
    {
        private readonly ApplicationDbContext _context;

        public FactureLivraisonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public FactureLivraisonVM GenererFactureLivraison(Facture facture)
        {
            var factureLivraisonOriginal = new FactureLivraison
            {
                Id = facture.Id,
                Facture = facture,
            };
            factureLivraisonOriginal.CalculerFacture();

            var factureLivraison = new FactureLivraisonVM
            {
                SousTotal = factureLivraisonOriginal.SousTotal,
                PrixFinal = factureLivraisonOriginal.PrixFinal,
                TPS = factureLivraisonOriginal.TPS,
                TVQ = factureLivraisonOriginal.TVQ,
            };
            return factureLivraison;
        }
    }
}
