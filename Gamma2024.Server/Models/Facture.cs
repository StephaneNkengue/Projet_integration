namespace Gamma2024.Server.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime DateAchat { get; set; }
        public double PrixFinal { get; set; }
        public double SousTotal { get; set; }
        public double PrixLots { get; set; }
        public double FraisEncanteur { get; set; }
        public double? FraisLivraison { get; set; }
        public double TPS { get; set; }
        public double TVQ { get; set; }
        public double? Don { get; set; }
        public int IdAdresse { get; set; }
        public string IdClient { get; set; } = null!;
        public int? IdCharite { get; set; }
        public ICollection<Lot> Lots { get; set; } = [];
        public Adresse Adresse { get; set; } = null!;
        public Charite? Charite { get; set; }
        public ApplicationUser Client { get; set; } = null!;

        public void CalculerFacture()
        {
            PrixLots = Lots.Select(l => l.Mise.Value).Sum();
            FraisEncanteur = PrixLots * 0.15;
            SousTotal = PrixLots + FraisEncanteur;
            CalculerFraisLivraison();


            if (IdCharite.HasValue)
            {
                Don = SousTotal * 0.02;
                SousTotal += Don.Value;
            }

            TPS = SousTotal * 0.05;
            TVQ = SousTotal * 0.9975;

            PrixFinal = SousTotal + FraisLivraison.Value + TPS + TVQ;
        }
        public void CalculerFraisLivraison()
        {
            var prix = new List<double>();
            foreach (var item in Lots)
            {
                if (item.Hauteur <= 20 || item.Largeur <= 20)
                {
                    prix.Add(35);
                }
                else
                {
                    prix.Add(65);
                }
            }
            prix.OrderByDescending(p => p);

            FraisLivraison = prix[0];

            prix.RemoveAt(0);

            FraisLivraison += (prix.Sum()) / 2;
        }
    }
}