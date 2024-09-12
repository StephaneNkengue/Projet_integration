namespace Gamma2024.Server.Models
{
    public class Charite
    {
        public int Id { get; set; }
        public double MontantDon { get; set; }
        public string NomOrganisme { get; set; } = null!;
        public string IdClient { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public int IdFacture { get; set; }
        public Facture Facture { get; set; } = null!;
    }
}
