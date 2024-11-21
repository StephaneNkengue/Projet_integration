namespace Gamma2024.Server.ViewModels
{
    public class FactureGenererVM
    {
        public int Id { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixFinal { get; set; }
        public double SousTotal { get; set; }
        public double FraisEncanteur { get; set; }
        public double TPS { get; set; }
        public double TVQ { get; set; }
        public ICollection<LotFactureVM> Lots { get; set; } = [];
        public ClientFactureVM Client { get; set; } = null!;
        public int NumeroEncan { get; set; }
        public string UrlInvoiceStripe { get; set; } = null!;
    }

    public class ClientFactureVM
    {
        public string Nom { get; set; } = default!;
        public string AdresseLigne1 { get; set; } = default!;
        public string? AdresseLigne2 { get; set; }
        public string AdresseLigne3 { get; set; } = default!;
        public string CodePostal { get; set; } = default!;
        public string Courriel { get; set; } = default!;
        public string Telephone { get; set; } = default!;
        public string ClientId { get; set; } = default!;
    }

    public class LotFactureVM
    {
        public string Description { get; set; } = default!;
        public string Prix { get; set; } = default!;
    }
}
