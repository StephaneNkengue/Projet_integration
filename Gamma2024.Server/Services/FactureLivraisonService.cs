using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

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
                IdCharite = 1
            };
            factureLivraisonOriginal.CalculerFacture();

            var factureLivraison = new FactureLivraisonVM
            {
                IdFacture = factureLivraisonOriginal.IdFacture,
                SousTotal = factureLivraisonOriginal.SousTotal,
                PrixFinal = factureLivraisonOriginal.PrixFinal,
                TPS = factureLivraisonOriginal.TPS,
                TVQ = factureLivraisonOriginal.TVQ,
                Don = factureLivraisonOriginal.Don.Value,
                PrixFinalSansDon = double.Round(factureLivraisonOriginal.PrixFinal - factureLivraisonOriginal.Don.Value, 2),
                Charites = _context.Charites.Select(c => new ChariteVM
                {
                    Id = c.Id,
                    NomOrganisme = c.NomOrganisme,
                }).ToList()
            };
            return factureLivraison;
        }

        public FactureLivraison ChercherFactureLivraison(int idFactureLivraison)
        {
            var facture = _context.FactureLivraisons.Include(f => f.Facture.Client).FirstOrDefault(f => f.Id == idFactureLivraison);
            return facture;
        }
    }
}
