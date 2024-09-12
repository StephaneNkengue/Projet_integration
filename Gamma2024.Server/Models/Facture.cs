namespace Gamma2024.Server.Models{
    public class Facture
{
    public int Id { get; set; }
    public int NumeroFacture { get; set; }
    public DateTime DateAchat { get; set; }
    public double PrixTotal { get; set; }
    public bool EstLivree { get; set; }
    public int IdLot { get; set; }
    public int IdAdresse { get; set; }
    
    public  Lot Lot { get; set; } = null!;
    public  Adresse Adresse { get; set; } = null!;
    public Charite? Charite { get; set; }
}
}