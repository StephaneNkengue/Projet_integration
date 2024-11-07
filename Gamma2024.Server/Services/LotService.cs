using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.Validations;
using Gamma2024.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
	public class LotService
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public LotService(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
		}

		public async Task<IEnumerable<LotAffichageVM>> ObtenirTousLots()
		{
			return await _context.Lots
				.Include(l => l.Photos)
				.Include(l => l.Categorie)
				.Include(l => l.Medium)
				.Include(l => l.Vendeur)
				.Include(l => l.EncanLots)
				.ThenInclude(el => el.Encan)
				.Select(l => new LotAffichageVM
				{
					Id = l.Id,
					NumeroEncan = l.EncanLots.FirstOrDefault().Encan.NumeroEncan.ToString(),
					Code = l.Numero,
					PrixOuverture = $"{l.PrixOuverture.ToString()} $",
					PrixMinPourVente = $"{l.PrixMinPourVente.ToString()} $",
					ValeurEstimeMin = $"{l.ValeurEstimeMin.ToString()} $",
					ValeurEstimeMax = $"{l.ValeurEstimeMax.ToString()} $",
					Categorie = l.Categorie.Nom,
					Artiste = l.Artiste,
					Dimension = $"{l.Hauteur} x {l.Largeur}",
					Description = l.Description,
					Medium = l.Medium.Type,
					EstLivrable = l.EstLivrable,
					Vendeur = $"{l.Vendeur.Prenom} {l.Vendeur.Nom}",
					Mise = l.Mise,
					EstVendu = l.EstVendu,
					DateFinVente = l.DateFinVente,
					DateDepot = l.DateDepot,
					DateCreation = l.DateCreation,
					IdClientMise = l.IdClientMise,
					SeraLivree = l.SeraLivree,
					Photos = l.Photos.Select(p => new PhotoVM
					{
						Id = p.Id,
						Url = p.Lien
					}).ToList()
				})
				.ToListAsync();
		}

		public async Task<LotModificationVM> ObtenirLot(int id)
		{
			var lot = await _context.Lots
				.Include(l => l.Categorie)
				.Include(l => l.Photos)
				.Include(l => l.Vendeur)
				.Include(l => l.Medium)
				.Include(l => l.EncanLots)
					.ThenInclude(el => el.Encan)
				.Include(l => l.ClientMise)
				.FirstOrDefaultAsync(l => l.Id == id);

			if (lot == null)
			{
				return null;
			}

			var encanLot = lot.EncanLots.FirstOrDefault();

			return new LotModificationVM
			{
				Id = lot.Id,
				Numero = lot.Numero,
				Description = lot.Description,
				ValeurEstimeMin = lot.ValeurEstimeMin,
				ValeurEstimeMax = lot.ValeurEstimeMax,
				PrixOuverture = lot.PrixOuverture,
				PrixMinPourVente = lot.PrixMinPourVente,
				DateDepot = lot.DateDepot,
				Artiste = lot.Artiste,
				DateCreation = lot.DateCreation,
				IdCategorie = lot.IdCategorie,
				Categorie = lot.Categorie?.Nom,
				IdVendeur = lot.IdVendeur,
				Vendeur = $"{lot.Vendeur?.Prenom} {lot.Vendeur?.Nom}",
				EstLivrable = lot.EstLivrable,
				Hauteur = lot.Hauteur,
				Largeur = lot.Largeur,
				IdMedium = lot.IdMedium,
				Medium = lot.Medium?.Type,
				IdEncanModifie = encanLot?.IdEncan,
				NumeroEncan = encanLot?.Encan?.NumeroEncan.ToString(),
				PhotosModifie = lot.Photos.Select(p => new PhotoVM
				{
					Id = p.Id,
					Url = p.Lien
				}).ToList(),
				IdClientMise = lot.IdClientMise,
				NomClientMise = $"{lot.ClientMise?.FirstName} {lot.ClientMise?.Name}",
				Mise = lot.Mise,
				EstVendu = lot.EstVendu,
				SeraLivree = lot.SeraLivree,
				DateFinVente = lot.DateFinVente
			};
		}

		public async Task<(bool Success, string Message, LotAffichageVM Lot)> CreerLot(LotCreationVM lotVM)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var (isValid, errorMessage) = await LotValidation.ValidateLot(lotVM, _context);
				if (!isValid)
				{
					return (false, errorMessage, null);
				}

				var lot = new Lot
				{
					Numero = lotVM.Numero,
					Description = lotVM.Description,
					ValeurEstimeMin = lotVM.ValeurEstimeMin,
					ValeurEstimeMax = lotVM.ValeurEstimeMax,
					PrixOuverture = lotVM.PrixOuverture,
					PrixMinPourVente = lotVM.PrixMinPourVente,
					Artiste = lotVM.Artiste,
					IdCategorie = lotVM.IdCategorie,
					IdVendeur = lotVM.IdVendeur,
					EstLivrable = lotVM.EstLivrable,
					Largeur = lotVM.Largeur,
					Hauteur = lotVM.Hauteur,
					IdMedium = lotVM.IdMedium
				};

				_context.Lots.Add(lot);
				await _context.SaveChangesAsync();

				// Associer le lot à l'encan spécifié
				var encan = await _context.Encans.FindAsync(lotVM.IdEncan);
				if (encan == null)
				{
					return (false, "L'encan spécifié n'existe pas", null);
				}

				var encanLot = new EncanLot
				{
					IdEncan = encan.Id,
					IdLot = lot.Id
				};

				_context.EncanLots.Add(encanLot);

				// Gérer l'enregistrement des photos
				if (lotVM.Photos != null && lotVM.Photos.Any())
				{
					string uploadPath = Path.Combine(_environment.WebRootPath, "Images", $"ImagesEncan{encan.NumeroEncan}");
					Directory.CreateDirectory(uploadPath);

					foreach (var photo in lotVM.Photos)
					{
						if (photo.Length > 0)
						{
							string fileName = $"{lot.Numero}_{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
							string filePath = Path.Combine(uploadPath, fileName);

							using (var stream = new FileStream(filePath, FileMode.Create))
							{
								await photo.CopyToAsync(stream);
							}

							lot.Photos.Add(new Photo { Lien = $"/Images/ImagesEncan{encan.NumeroEncan}/{fileName}" });
						}
					}
				}

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				var lotModification = await ObtenirLot(lot.Id);
				var lotAffichage = ConvertirEnLotAffichageVM(lotModification);
				return (true, "Lot créé avec succès", lotAffichage);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return (false, $"Erreur lors de la création du lot : {ex.Message}", null);
			}
		}

		public async Task<(bool Success, string Message, LotAffichageVM Lot)> ModifierLot(int id, LotModificationVM lotVM)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var (isValid, errorMessage) = await LotValidation.ValidateLot(lotVM, _context);
				if (!isValid)
				{
					return (false, errorMessage, null);
				}

				var lot = await _context.Lots
					.Include(l => l.Photos)
					.Include(l => l.EncanLots)
					.FirstOrDefaultAsync(l => l.Id == id);

				if (lot == null)
				{
					return (false, "Lot non trouvé", null);
				}

				// Mettre à jour les propriétés du lot
				lot.Numero = lotVM.Numero;
				lot.Description = lotVM.Description;
				lot.ValeurEstimeMin = lotVM.ValeurEstimeMin;
				lot.ValeurEstimeMax = lotVM.ValeurEstimeMax;
				lot.PrixOuverture = lotVM.PrixOuverture;
				lot.PrixMinPourVente = lotVM.PrixMinPourVente;
				lot.Artiste = lotVM.Artiste;
				lot.IdCategorie = lotVM.IdCategorie;
				lot.IdVendeur = lotVM.IdVendeur;
				lot.EstLivrable = lotVM.EstLivrable;
				lot.Largeur = lotVM.Largeur;
				lot.Hauteur = lotVM.Hauteur;
				lot.IdMedium = lotVM.IdMedium;

				// Mettre à jour l'encan associé si nécessaire
				if (lotVM.IdEncanModifie.HasValue)
				{
					var encanLot = lot.EncanLots.FirstOrDefault();
					if (encanLot == null || encanLot.IdEncan != lotVM.IdEncanModifie.Value)
					{
						if (encanLot != null)
						{
							_context.EncanLots.Remove(encanLot);
						}
						lot.EncanLots.Add(new EncanLot { IdEncan = lotVM.IdEncanModifie.Value, IdLot = lot.Id });
					}
				}

				// Gérer l'ajout de nouvelles photos
				if (lotVM.NouvellesPhotos != null && lotVM.NouvellesPhotos.Any())
				{
					var encan = await _context.EncanLots
						.Where(el => el.IdLot == lot.Id)
						.Select(el => el.Encan)
						.FirstOrDefaultAsync();

					if (encan != null)
					{
						string uploadPath = Path.Combine(_environment.WebRootPath, "Images", $"ImagesEncan{encan.NumeroEncan}");
						Directory.CreateDirectory(uploadPath);

						foreach (var photo in lotVM.NouvellesPhotos)
						{
							if (photo.Length > 0)
							{
								string fileName = $"{lot.Numero}_{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
								string filePath = Path.Combine(uploadPath, fileName);

								using (var stream = new FileStream(filePath, FileMode.Create))
								{
									await photo.CopyToAsync(stream);
								}

								lot.Photos.Add(new Photo { Lien = $"/Images/ImagesEncan{encan.NumeroEncan}/{fileName}" });
							}
						}
					}
				}

				// Gérer la suppression des photos
				if (lotVM.PhotosASupprimer != null && lotVM.PhotosASupprimer.Any())
				{
					foreach (var photoId in lotVM.PhotosASupprimer)
					{
						var photo = lot.Photos.FirstOrDefault(p => p.Id == photoId);
						if (photo != null)
						{
							string fullPath = Path.Combine(_environment.WebRootPath, photo.Lien.TrimStart('/'));
							if (System.IO.File.Exists(fullPath))
							{
								System.IO.File.Delete(fullPath);
							}
							lot.Photos.Remove(photo);
							_context.Photos.Remove(photo);
						}
					}
				}

				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				var lotModification = await ObtenirLot(lot.Id);
				var lotAffichage = ConvertirEnLotAffichageVM(lotModification);
				return (true, "Lot modifié avec succès", lotAffichage);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return (false, $"Erreur lors de la modification du lot : {ex.Message}", null);
			}
		}

		public ICollection<LotEncanAffichageVM> ChercherTousLotsRecherche()
		{
			try
			{
				var lots = _context.Lots
					.Include(l => l.EncanLots)
					.Include(l => l.Photos)
					.Select(l => new LotEncanAffichageVM()
					{
						Id = l.Id,
						Numero = l.Numero,
						ValeurEstimeMax = l.ValeurEstimeMax,
						ValeurEstimeMin = l.ValeurEstimeMin,
						Artiste = l.Artiste,
						Mise = l.Mise,
						EstVendu = l.EstVendu,
						DateFinVente = l.DateFinVente,
						Photos = l.Photos,
						Description = l.Description,
						EstLivrable = l.EstLivrable,
						Hauteur = l.Hauteur,
						Largeur = l.Largeur
					})
					.ToList();

				Console.WriteLine($"Lots trouvés dans le service: {lots.Count}");
				return lots;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur dans le service: {ex.Message}");
				throw;
			}
		}

		public ICollection<LotEncanAffichageVM> ChercherTousLotsParEncan(int idEncan)
		{
			try
			{
				var lots = _context.Lots
					.Include(l => l.EncanLots)
					.Include(l => l.Photos)
					.Where(l => l.EncanLots.Any(el => el.IdEncan == idEncan)) // Modifié ici
					.Select(l => new LotEncanAffichageVM()
					{
						Id = l.Id,
						Numero = l.Numero,
						ValeurEstimeMax = l.ValeurEstimeMax,
						ValeurEstimeMin = l.ValeurEstimeMin,
						Artiste = l.Artiste,
						Mise = l.Mise,
						EstVendu = l.EstVendu,
						DateFinVente = l.DateFinVente,
						Photos = l.Photos,
						Description = l.Description,
						EstLivrable = l.EstLivrable,
						Hauteur = l.Hauteur,
						Largeur = l.Largeur
					})
					.ToList();

				Console.WriteLine($"Lots trouvés dans le service: {lots.Count}");
				return lots;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur dans le service: {ex.Message}");
				throw;
			}
		}


		public LotDetailsVM ChercherDetailsLotParId(int idLot)

		{

			var lot = _context.Lots

				.Include(l => l.Photos)

				.Include(l => l.Categorie)

				.Include(l => l.Medium)

				.FirstOrDefault(l => l.Id == idLot);


			if (lot != null)

			{

				var retourLot = new LotDetailsVM()

				{

					Id = lot.Id,

					Numero = lot.Numero,

					ValeurEstimeMax = lot.ValeurEstimeMax,

					ValeurEstimeMin = lot.ValeurEstimeMin,

					Artiste = lot.Artiste,

					Mise = lot.Mise,

					EstVendu = lot.EstVendu,

					Photos = lot.Photos.Select(p => p.Lien),

					Description = lot.Description,

					EstLivrable = lot.EstLivrable,

					Hauteur = lot.Hauteur,

					Largeur = lot.Largeur,

					Medium = lot.Medium.Type,

					Categorie = lot.Categorie.Nom

				};


				return retourLot;

			}


			return null;

		}


		public ICollection<LotAffichageAdministrateurVM> ChercherTousLots()

		{

			var lots = _context.Lots

				.Select(l => new LotAffichageAdministrateurVM()

				{

					Id = l.Id,

					Encan = _context.Encans.Where(e => e.Id == (_context.EncanLots.Where(e => e.IdLot == l.Id).Max(e => e.IdEncan))).Single().NumeroEncan,

					Numero = l.Numero,

					PrixOuverture = l.PrixOuverture.ToString() + " $",

					ValeurMinPourVente = l.PrixMinPourVente.ToString() + " $",

					ValeurEstimeMax = l.ValeurEstimeMax.ToString() + " $",

					ValeurEstimeMin = l.ValeurEstimeMin.ToString() + " $",

					Categorie = _context.Categories.Where(c => c.Id == l.IdCategorie).Single().Nom,

					Artiste = l.Artiste,

					Description = l.Description,

					Hauteur = l.Hauteur,

					Largeur = l.Largeur,

					Medium = _context.Mediums.Where(m => m.Id == l.IdMedium).Single().Type,

					Vendeur = l.Vendeur.Prenom + " " + l.Vendeur.Nom,

					EstVendu = l.EstVendu,

					EstLivrable = l.EstLivrable,

				}).ToList();


			return lots;

		}

		public async Task<(bool Success, string Message)> SupprimerLot(int id)
		{
			try
			{
				var lot = await _context.Lots
					.Include(l => l.Photos)
					.Include(l => l.EncanLots)
					.FirstOrDefaultAsync(l => l.Id == id);

				if (lot == null)
				{
					return (false, "Lot non trouvé");
				}


				// Supprimer les EncanLots associés
				foreach (var encanLot in lot.EncanLots)
				{
					_context.EncanLots.Remove(encanLot);
				}

				// Supprimer les photos associées
				foreach (var photo in lot.Photos)
				{
					string fullPath = Path.Combine(_environment.WebRootPath, photo.Lien.TrimStart('/'));
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
					}
					_context.Photos.Remove(photo);
				}

				_context.Lots.Remove(lot);

				await _context.SaveChangesAsync();

				return (true, "Lot supprimé avec succès");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Erreur lors de la suppression du lot: " + ex.Message);
				if (ex.InnerException != null)
				{
					Console.WriteLine("Détails de l'erreur interne: " + ex.InnerException.Message);
				}
				return (false, "Erreur lors de la suppression du lot : " + ex.Message);
			}
		}

		// Ajoutez cette nouvelle méthode pour convertir LotModificationVM en LotAffichageVM
		private LotAffichageVM ConvertirEnLotAffichageVM(LotModificationVM lotModification)
		{
			return new LotAffichageVM
			{
				Id = lotModification.Id,
				NumeroEncan = lotModification.NumeroEncan,
				Code = lotModification.Numero,
				PrixOuverture = $"{lotModification.ValeurEstimeMax.ToString()} $",
				PrixMinPourVente = $"{lotModification.ValeurEstimeMax.ToString()} $",
				ValeurEstimeMin = $"{lotModification.ValeurEstimeMax.ToString()} $",
				ValeurEstimeMax = $"{lotModification.ValeurEstimeMax.ToString()} $",
				Categorie = lotModification.Categorie,
				Artiste = lotModification.Artiste,
				Dimension = $"{lotModification.Hauteur} x {lotModification.Largeur}",
				Description = lotModification.Description,
				Medium = lotModification.Medium,
				EstLivrable = lotModification.EstLivrable,
				Vendeur = lotModification.Vendeur,
				Mise = lotModification.Mise,
				EstVendu = lotModification.EstVendu,
				DateFinVente = lotModification.DateFinVente,
				DateDepot = lotModification.DateDepot,
				DateCreation = lotModification.DateCreation,
				IdClientMise = lotModification.IdClientMise,
				SeraLivree = lotModification.SeraLivree,
				Photos = lotModification.PhotosModifie
			};
		}
	}
}
