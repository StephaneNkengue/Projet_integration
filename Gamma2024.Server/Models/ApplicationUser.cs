using Microsoft.AspNetCore.Identity;

namespace Gamma2024.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? PersonneId { get; set; }
        public Personne? Personne { get; set; }
        public int IdCompte { get; set; }
        public Compte Compte { get; set; } = null!;
        public ICollection<CarteCredit> CarteCredits { get; set; } = null!;
    }
}