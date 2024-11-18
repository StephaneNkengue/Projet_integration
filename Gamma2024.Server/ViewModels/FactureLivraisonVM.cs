using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
    public class FactureLivraisonVM
    {
        public int Id { get; set; }

        private double _prixFinal;
        public double PrixFinal { get => _prixFinal; set => _prixFinal = double.Round(value, 2, MidpointRounding.ToZero); }

        private double _sousTotal;
        public double SousTotal { get => _sousTotal; set => _sousTotal = double.Round(value, 2, MidpointRounding.ToZero); }
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
        public int? IdCharite { get; set; }
        public Adresse Adresse { get; set; } = null!;
        public Charite? Charite { get; set; }
        public int IdFacture { get; set; }
        public Facture Facture { get; set; } = null!;
    }
}
