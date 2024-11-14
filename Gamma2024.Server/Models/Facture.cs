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

        private double? _don;
        public double? Don
        {
            get => _don;
            set => _don = value.HasValue ? Math.Round(value.Value, 2, MidpointRounding.ToZero) : null;
        }

        public int IdAdresse { get; set; }
        public string IdClient { get; set; } = null!;
        public int? IdCharite { get; set; }
        public ICollection<Lot> Lots { get; set; } = [];
        public Adresse Adresse { get; set; } = null!;
        public Charite? Charite { get; set; }
        public ApplicationUser Client { get; set; } = null!;
        public bool estPaye { get; set; } = false;
        public int NumeroEncan { get; set; }

        public void CalculerFacture()
        {
            PrixLots = Lots.Select(l => l.Mise.Value).Sum();
            FraisEncanteur = PrixLots * 0.15;
            SousTotal = PrixLots + FraisEncanteur;
            CalculerFraisLivraison();

            if (IdCharite.HasValue)
            {
                double donCalcule = SousTotal * 0.02;
                Don = Math.Round(donCalcule, 2, MidpointRounding.ToZero);
                SousTotal += Don.Value; // Utilisation de .Value car on est sûr que Don a une valeur ici
            }
            else
            {
                Don = null;
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
            if (prix.Any())
            {
                FraisLivraison = prix[0];
                prix.RemoveAt(0);
                FraisLivraison += (prix.Sum()) / 2;
            }
            else
            {
                FraisLivraison = 0;
            }
        }
    }
}
