using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
    public class FactureAffichageVM
    {
        public int Id { get; set; }
        public string IdClient { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Pseudonyme { get; set; } = null!;
        public string Courriel { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public int Encan { get; set; }
        public DateTime DateAchat { get; set; }
        public ICollection<Lot> Lots { get; set; } = [];
        public string PdfPath { get; set; } = null!;
        public string PdfPathLivraison { get; set; } = null!;
        public bool? Livraison { get; set; }
    }
}
