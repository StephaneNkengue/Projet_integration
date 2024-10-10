namespace Gamma2024.Server.Models
{
    public class Encan
    {
        public int Id { get; set; }
        public int NumeroEncan { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime DateDebutSoireeCloture { get; set; }
        public DateTime DateFinSoireeCloture { get; set; }
        public ICollection<EncanLot> EncanLots { get; set; } = [];
        public bool EstPublie { get; set; } = false;

    }
}
