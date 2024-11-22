namespace Gamma2024.Server.ViewModels
{
    public class MembreVM
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public bool EstBloque { get; set; }
        public List<CarteCreditInfoVM> CartesCredit { get; set; } = null!;
        public List<AdresseInfoVM> Adresses { get; set; } = null!;

    }

    public class CarteCreditInfoVM
    {
        public string Dernier4Numero { get; set; } = null!;
        public string ExpirationDate { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string? PaymentMethodId { get; set; } = null!;
    }

    public class AdresseInfoVM
    {
        public int Id { get; set; }
        public int NumeroCivique { get; set; }
        public string Rue { get; set; } = null!;
        public string Appartement { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Pays { get; set; } = null!;
        public string CodePostal { get; set; } = null!;
    }
}
