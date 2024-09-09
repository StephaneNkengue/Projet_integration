namespace Gamma2024.Server.Models{
    public class Facture
{
    public int Id { get; set; }
    public int NumeroFacture { get; set; }
    public DateTime DateAchat { get; set; }
    public double PrixTotal { get; set; }
    public bool EstLivree { get; set; }
    public int IdLot { get; set; }
    public int IdAdresseFacturation { get; set; }
    
    public  Lot Lot { get; set; } = null!;
    public  Adresse AdresseFacturation { get; set; } = null!;
}
}