namespace Gamma2024.Server.ViewModels
{
    public class VendeurVM
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Courriel { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public AdresseVendeurVM Adresse { get; set; } = null!;
    }

    public class AdresseVendeurVM
    {
        public string NumeroCivique { get; set; } = null!;
        public string Rue { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Pays { get; set; } = null!;
        public string CodePostal { get; set; } = null!;
    }
}