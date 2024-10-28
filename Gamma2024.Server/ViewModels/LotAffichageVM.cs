using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
    public class LotAffichageVM
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public string Artiste { get; set; } = null!;
        public double? Mise { get; set; }
        public bool EstVendu { get; set; }
        public DateTime? DateFinVente { get; set; }
        public bool EstLivrable { get; set; }
        public double Largeur { get; set; }
        public double Hauteur { get; set; }
        public ICollection<Photo> Photos { get; set; } = [];

    }
}
