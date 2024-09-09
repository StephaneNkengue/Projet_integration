namespace Gamma2024.Server.Models{
    public class Compte
{
    public int Id { get; set; }
    public string NomUtilisateur { get; set; } = null!;
    public string Pseudonyme { get; set; } = null!;  
    public string MotPasse { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    
    public  Client Client { get; set; } = null!;
    public  Administrateur Administrateur { get; set; } = null!;
}
}