using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
    public class FactureAffichageParEncanVM
    {
        public string IdClient { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Encan { get; set; } = null!;
        public ICollection<Lot> Lots { get; set; } = [];
    }
}
