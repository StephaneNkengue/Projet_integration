using Gamma2024.Server.Models;
using System.Collections.Generic;

namespace Gamma2024.Server.ViewModels
{
    public class LotAffichageVM
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public string Artiste { get; set; } = null!;
        public double? Mise { get; set; }
        public bool EstVendu { get; set; }
        public DateTime? DateFinVente { get; set; }
        public ICollection<PhotoVM> Photos { get; set; } = [];
    }

    public class PhotoVM
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        // Autres propriétés nécessaires, mais sans référence au Lot
    }
}
