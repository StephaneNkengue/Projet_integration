using Gamma2024.Server.Data;
using Gamma2024.Server.Interface;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Gamma2024.Server.Services
{
    public class FactureLivraisonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJsReportMVCService _jsReportService;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;

        public FactureLivraisonService(ApplicationDbContext context, IJsReportMVCService jsReportService, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _context = context;
            _jsReportService = jsReportService;
            _environment = environment;
            _emailSender = emailSender;
        }

        public FactureLivraisonVM GenererFactureLivraison(Facture facture)
        {
            var factureExistant = _context.FactureLivraisons.FirstOrDefault(fl => fl.IdFacture == facture.Id);
            if (factureExistant == null)
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
                    Facture = facture,
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
            return null;
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

            var factureLivraison = new FactureLivraison
            {
                IdFacture = choix.IdFacture,
                Facture = _context.Factures.Include(f => f.Lots).Include(f => f.Client).First(f => f.Id == choix.IdFacture),
                IdAdresse = choix.IdAdresse.Value,
                IdCharite = choix.IdCharite,
                DateAchat = DateTime.Now,
            };
            _context.FactureLivraisons.Add(factureLivraison);
            _context.SaveChanges();

            factureLivraison.CalculerFacture();

            _context.FactureLivraisons.Update(factureLivraison);
            _context.SaveChanges();

            factureLivraison.Facture.ChoixLivraison = true;
            _context.Factures.Update(factureLivraison.Facture);
            _context.SaveChanges();

            return factureLivraison;
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
