namespace Gamma2024.Server.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime DateAchat { get; set; }

        private double _prixFinal;
        public double PrixFinal { get => _prixFinal; set => _prixFinal = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _sousTotal;
        public double SousTotal { get => _sousTotal; set => _sousTotal = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _prixLots;
        public double PrixLots { get => _prixLots; set => _prixLots = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _fraisEncanteur;
        public double FraisEncanteur { get => _fraisEncanteur; set => _fraisEncanteur = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _fraisLivraison;
        public double FraisLivraison { get => _fraisLivraison; set => _fraisLivraison = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _tPS;
        public double TPS { get => _tPS; set => _tPS = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _tVQ;
        public double TVQ { get => _tVQ; set => _tVQ = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _don;
        public double? Don { get => _don; set => _don = double.Round(value.Value, 2, MidpointRounding.ToZero); }

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
            TVQ = SousTotal * 0.09975;

            PrixFinal = SousTotal + FraisLivraison + TPS + TVQ;
        }
        private void CalculerFraisLivraison()
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