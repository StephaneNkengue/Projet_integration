namespace Gamma2024.Server.ViewModels
{
    public class LotCreationVM
    {
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public string Artiste { get; set; } = null!;
        public int IdCategorie { get; set; }
        public int IdVendeur { get; set; }
        public bool EstLivrable { get; set; }
        public double Largeur { get; set; }
        public double Hauteur { get; set; }
        public int IdMedium { get; set; }
    }
}
