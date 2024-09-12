namespace Gamma2024.Server.Models{
    public class Personne
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Courriel { get; set; } = null!;
    public string Telephone { get; set; } = null!;
    public int IdAdresse { get; set; }

    
    public ICollection<Adresse> Adresses { get; set; } = [];
    public Vendeur? Vendeur { get; set; }
}    
}