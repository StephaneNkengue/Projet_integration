namespace Gamma2024.Server.ViewModels
{
    public class LotAffichageVM
    {
        public int Id { get; set; }
        public string NumeroEncan { get; set; } = null!; // ENCAN #
        public string Code { get; set; } = null!; // LOT #
        public double PrixOuverture { get; set; } // PRIX OUVERTURE
        public double? PrixMinPourVente { get; set; } // VALEUR MIN POUR VENDRE
        public double ValeurEstimeMin { get; set; } // ESTIMATION MIN
        public double ValeurEstimeMax { get; set; } // ESTIMATION MAX
        public string Categorie { get; set; } = null!; // GROUPE-CATÉGORIE
        public string Artiste { get; set; } = null!; // ARTISTE
        public string Dimension { get; set; } = null!; // DIMENSION (en po), sera formaté comme "Hauteur x Largeur"
        public string Description { get; set; } = null!; // DESCRIPTION
        public string Medium { get; set; } = null!; // MÉDIUM
        public bool EstLivrable { get; set; } // LIVRAISON
        public string Vendeur { get; set; } = null!; // Formaté comme "Prénom Nom"
        public double? Mise { get; set; }
        public bool EstVendu { get; set; }
        public DateTime? DateFinVente { get; set; }
        public DateTime DateDepot { get; set; }
        public DateTime DateCreation { get; set; }
        public string? IdClientMise { get; set; }
        public bool? SeraLivree { get; set; }
        public ICollection<PhotoVM> Photos { get; set; } = new List<PhotoVM>();
    }

    public class PhotoVM
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
    }
}
