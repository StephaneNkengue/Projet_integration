using System.ComponentModel.DataAnnotations;

namespace Gamma2024.Server.Models{
    public class Administrateur : ApplicationUser
    {
        public int IdCompte { get; set; }
        public int IdPersonne { get; set; }
        
        public  Compte Compte { get; set; } = null!;
        public  Personne Personne { get; set; } = null!;
    }
}
