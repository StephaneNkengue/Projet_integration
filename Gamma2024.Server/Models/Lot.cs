namespace Gamma2024.Server.Models{
    public class Lot
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Nom { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double ValeurEstimeMin { get; set; }
    public double ValeurEstimeMax { get; set; }
    public DateTime DateDepot { get; set; } = DateTime.Now;
    public string Artiste { get; set; } = null!;
    public string Medium { get; set; } = null!;  
    public DateTime DateCreation { get; set; } = DateTime.Now;
    public int NumeroEncan { get; set; }
    public int IdCategorie { get; set; }
    public int? IdClientMise { get; set; }
    public double? Mise { get; set; }
    public bool EstVendu { get; set; }
    public DateTime? DateFinVente { get; set; }
    public int IdVendeur { get; set; }
    
    public  Categorie Categorie { get; set; } = null!;
    public  Client ClientMise { get; set; } = null!;
    public  Vendeur Vendeur { get; set; } = null!;
    public  ICollection<Photo> Photos { get; set; } = [];
    public ICollection<EncanLot> EncanLots { get; set; } = [];
}
}
