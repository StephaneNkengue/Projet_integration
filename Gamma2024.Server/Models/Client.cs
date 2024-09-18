namespace Gamma2024.Server.Models{
    public class Client : ApplicationUser
{
    public bool EstBloque { get; set; }
    public Charite? Charite { get; set; }
    
}
}
