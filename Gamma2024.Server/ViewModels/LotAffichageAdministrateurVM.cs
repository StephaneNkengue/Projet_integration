using Gamma2024.Server.Models;

namespace Gamma2024.Server.ViewModels
{
	public class LotAffichageAdministrateurVM
	{
		public int Id { get; set; }
		public ICollection<EncanLot> EncanLots { get; set; } = [];
		public string Numero { get; set; } = null!;
		public double PrixOuverture { get; set; }
		public double ValeurMinPourVente { get; set; }
		public double ValeurEstimeMin { get; set; }
		public double ValeurEstimeMax { get; set; }
		public int IdCategorie { get; set; }
		public string Artiste { get; set; } = null!;
		public string Dimension { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Medium { get; set; } = null!;
		public Vendeur Vendeur { get; set; } = null!;
		public bool EstVendu { get; set; }
		public bool estLivrable { get; set; }
	}
}
