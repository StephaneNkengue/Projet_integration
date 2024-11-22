using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
    public class FactureAffichageMembreVM
    {
        public int Id { get; set; }
        public string IdClient { get; set; } = null!;
        public int Encan { get; set; }
        public DateTime DateAchat { get; set; }
        public string PdfPath { get; set; } = null!;
        public ICollection<Lot> Lots { get; set; } = [];
    }
}
