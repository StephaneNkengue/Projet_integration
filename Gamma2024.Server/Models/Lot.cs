namespace Gamma2024.Server.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public DateTime DateDepot { get; set; } = DateTime.Now;
        public string Artiste { get; set; } = null!;
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public int IdCategorie { get; set; }
        public string? IdClientMise { get; set; }
        public double? Mise { get; set; }
        public bool EstVendu { get; set; }
        public DateTime? DateFinVente { get; set; }
        public int IdVendeur { get; set; }
        public bool estLivrable { get; set; }
        public string? Dimensions { get; set; }

        public Categorie Categorie { get; set; } = null!;
        public ApplicationUser? ClientMise { get; set; }
        public Vendeur Vendeur { get; set; } = null!;
        public ICollection<Photo> Photos { get; set; } = [];
        public ICollection<EncanLot> EncanLots { get; set; } = [];
        public int IdMedium { get; set; }
        public Medium Medium { get; set; } = null!;
    }
}
