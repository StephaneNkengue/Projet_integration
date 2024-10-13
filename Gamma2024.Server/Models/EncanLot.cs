namespace Gamma2024.Server.Models
{
    public class EncanLot
    {
        public int NumeroEncan { get; set; }
        public int IdLot { get; set; }

        public Encan Encan { get; set; } = null!;
        public Lot Lot { get; set; } = null!;
    }
}
