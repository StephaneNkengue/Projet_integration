namespace Gamma2024.Server.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixFinal { get; set; }
        public double PrixLots { get; set; }
        public double FraisEncanteur { get; set; }
        public double? FraisLivraison { get; set; }
        public double Taxes { get; set; }
        public double? Don { get; set; }
        public int IdAdresse { get; set; }
        public string IdClient { get; set; } = null!;
        public int? IdCharite { get; set; }
        public ICollection<Lot> Lots { get; set; } = null!;
        public Adresse Adresse { get; set; } = null!;
        public Charite? Charite { get; set; }
        public ApplicationUser Client { get; set; } = null!;
    }
}