using Microsoft.AspNetCore.Identity;

namespace Gamma2024.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
        public int IdAdresse { get; set; }
        
        public ICollection<Adresse> Adresses { get; set; } = [];
        public ICollection<CarteCredit> CarteCredits { get; set; } = null!;
    }
}