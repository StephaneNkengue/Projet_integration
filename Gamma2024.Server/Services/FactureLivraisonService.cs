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

        public FactureLivraison AjouterFactureLivraison(FactureLivraisonAjoutVM choix)
        {
            if (choix.IdAdresse == null)
            {
                var facture = _context.Factures.FirstOrDefault(f => f.Id == choix.IdFacture);
                facture.ChoixLivraison = false;
                _context.Factures.Update(facture);
                _context.SaveChanges();

                return null;
            }

            var factureLivraisonOriginal = new FactureLivraison
            {
                IdFacture = choix.IdFacture,
                Facture = _context.Factures.Include(f => f.Lots).First(f => f.Id == choix.IdFacture),
                IdAdresse = choix.IdAdresse.Value,
                IdCharite = choix.IdCharite,
                DateAchat = DateTime.Now,
            };
            _context.FactureLivraisons.Add(factureLivraisonOriginal);
            _context.SaveChanges();

            factureLivraisonOriginal.CalculerFacture();

            _context.FactureLivraisons.Update(factureLivraisonOriginal);
            _context.SaveChanges();

            factureLivraisonOriginal.Facture.ChoixLivraison = true;
            _context.Factures.Update(factureLivraisonOriginal.Facture);
            _context.SaveChanges();

            return factureLivraisonOriginal;
        }

        public FactureLivraison ChercherFactureLivraison(int idFactureLivraison)
        {
            var facture = _context.FactureLivraisons.Include(f => f.Facture.Client).FirstOrDefault(f => f.Id == idFactureLivraison);
            return facture;
        }

        public bool PayerFactureLivraison(int idFactureLivraison)
        {
            var factureLivraison = _context.FactureLivraisons.FirstOrDefault(f => f.Id == idFactureLivraison);

            if (factureLivraison == null)
            {
                return false;
            }

            factureLivraison.EstPaye = true;
            _context.FactureLivraisons.Update(factureLivraison);
            _context.SaveChanges();
            return true;
        }
    }
}
