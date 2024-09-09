namespace Gamma2024.Server.Models{
    public class Adresse
{
    public int Id { get; set; }
    public int IdPersonne { get; set; }
    public int Numero { get; set; }
    public string Appartement { get; set; } = null!;
    public string Rue { get; set; } = null!;
    public string Ville { get; set; } = null!;
    public string Pays { get; set; } = null!;
    public string CodePostal { get; set; } = null!;
    public bool EstDomicile { get; set; }
    
    public  ICollection<Personne> Personnes { get; set; } = [];
    public  ICollection<Facture> Factures { get; set; } = [];
}
}
