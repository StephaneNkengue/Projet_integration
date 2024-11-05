namespace Gamma2024.Server.ViewModels
{
    public class FactureAffichageParDateVM
    {
        public string IdClient { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public DateTime DateAchat { get; set; }
    }
}
