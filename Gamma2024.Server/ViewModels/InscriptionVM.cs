namespace Gamma2024.Server.ViewModels{
   public class InscriptionVM
{
    public InfoGenerale GeneralInfo { get; set; } = null!;  
    public CarteCredit CarteCredit { get; set; } = null!;
    public Adresse Adresse { get; set; } = null!;

    // Classes internes déplacées à l'extérieur de InscriptionVM
}

public class InfoGenerale
{
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string Courriel { get; set; } = null!;

    public string Telephone { get; set; } = null!;
    public string Pseudo { get; set; } = null!;
    public string MotDePasse { get; set; } = null!;

    public string ConfirmMotPasse { get; set; } = null!;
}

public class CarteCredit
{
    public string NomProprio { get; set; } = null!;
    public string NumeroCarte { get; set; } = null!;
    public string DateExpiration { get; set; } = null!;

}

public class Adresse
{
    public string NumeroCivique { get; set; } = null!;
    public string Rue { get; set; } = null!;
    public string Appartement { get; set; } = null!;

    public string Ville { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string Pays { get; set; } = null!;
    public string CodePostal { get; set; } = null!;
}
}
