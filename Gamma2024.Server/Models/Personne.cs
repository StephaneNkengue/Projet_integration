namespace Gamma2024.Server.Models{
    public class Personne
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Courriel { get; set; } = null!;
    public string Telephone { get; set; } = null!;
    
    public  Adresse Adresse { get; set; } = null!;
    public  Client Client { get; set; } = null!;
    public  Vendeur Vendeur { get; set; } = null!;
    public  Administrateur Administrateur { get; set; } = null!;
}    
}