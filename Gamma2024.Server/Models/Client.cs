namespace Gamma2024.Server.Models{
    public class Client : ApplicationUser
{
    public bool EstBloque { get; set; }
    public int IdPersonne { get; set; }
    public int IdCompte { get; set; }
    
    public  Personne Personne { get; set; } = null!;
    public  Compte Compte { get; set; } = null!;
    public  ICollection<CarteCredit> CartesCredit { get; set; } = [];
}
}
