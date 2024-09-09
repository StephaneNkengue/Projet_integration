namespace Gamma2024.Server.Models
{
        public class Vendeur
    {
        public int Id { get; set; }
        public int IdPersonne { get; set; }
        
        public  Personne Personne { get; set; } = null!;
        public  ICollection<Lot> Lots { get; set; } = [];
    }
}