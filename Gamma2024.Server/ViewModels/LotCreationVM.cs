namespace Gamma2024.Server.ViewModels
{
    public class LotCreationVM
    {
        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public string Artiste { get; set; } = null!;
        public int IdCategorie { get; set; }
        public int IdVendeur { get; set; }
        public bool EstLivrable { get; set; }
        public string? Dimensions { get; set; }
        public int IdMedium { get; set; }
    }
}

