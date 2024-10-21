namespace Gamma2024.Server.Models
{
    public class CarteCredit
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public int MoisExpiration { get; set; }
        public int AnneeExpiration { get; set; }
        public string IdApplicationUser { get; set; } = null!;

        public ApplicationUser ApplicationUser { get; set; } = null!;

    }
}