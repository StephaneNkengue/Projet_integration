namespace Gamma2024.Server.Models
{
    public class MiseAutomatique
    {
        public int Id { get; set; }
        public int LotId { get; set; }
        public string UserId { get; set; }
        public decimal MontantMaximal { get; set; }
        public DateTime DateCreation { get; set; }
        public bool EstActive { get; set; }
        
        // Navigation properties
        public virtual Lot Lot { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
} 