namespace Gamma2024.Server.ViewModels
{
    public class LotAffichageAdministrateurVM
    {
        public int Id { get; set; }
        public int Encan { get; set; }
        public string Numero { get; set; } = null!;
        public string PrixOuverture { get; set; }
        public string? ValeurMinPourVente { get; set; }
        public string ValeurEstimeMin { get; set; }
        public string ValeurEstimeMax { get; set; }
        public string Categorie { get; set; } = null!;
        public string Artiste { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Medium { get; set; } = null!;
        public string Vendeur { get; set; } = null!;
        public bool EstVendu { get; set; }
        public bool EstLivrable { get; set; }
        public double Largeur { get; set; }
        public double Hauteur { get; set; }
    }
}
