namespace Gamma2024.Server.Models
{
    public class LotFacture
    {
        public int IdFacture { get; set; }
        public int IdLot { get; set; }
        public Lot Lot { get; set; } = default!;
        public Facture Facture { get; set; } = default!;
        public bool Livraison { get; set; }
    }
}
