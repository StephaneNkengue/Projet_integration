using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Gamma2024.Server.ViewModels;
using Gamma2024.Server.Validations;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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
                    PrixOuverture = l.PrixOuverture,
                    PrixMinPourVente = l.PrixMinPourVente,
                    ValeurEstimeMin = l.ValeurEstimeMin,
                    ValeurEstimeMax = l.ValeurEstimeMax,
                    Categorie = l.Categorie.Nom,
                    Artiste = l.Artiste,
                    Dimension = $"{l.Hauteur} x {l.Largeur}",
                    Description = l.Description,
                    Medium = l.Medium.Type,
                    EstLivrable = l.estLivrable,
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

        public async Task<LotAffichageVM> ObtenirLot(int id)
        {
            var lot = await _context.Lots
                .Include(l => l.Photos)
                .Include(l => l.Categorie)
                .Include(l => l.Medium)
                .Include(l => l.Vendeur)
                .Include(l => l.EncanLots)
                .ThenInclude(el => el.Encan)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lot == null)
            {
                return null;
            }

            return new LotAffichageVM
            {
                Id = lot.Id,
                NumeroEncan = lot.EncanLots.FirstOrDefault()?.Encan.NumeroEncan.ToString(),
                Code = lot.Numero,
                PrixOuverture = lot.PrixOuverture,
                PrixMinPourVente = lot.PrixMinPourVente,
                ValeurEstimeMin = lot.ValeurEstimeMin,
                ValeurEstimeMax = lot.ValeurEstimeMax,
                Categorie = lot.Categorie.Nom,
                Artiste = lot.Artiste,
                Dimension = $"{lot.Hauteur} x {lot.Largeur}",
                Description = lot.Description,
                Medium = lot.Medium.Type,
                EstLivrable = lot.estLivrable,
                Vendeur = $"{lot.Vendeur.Prenom} {lot.Vendeur.Nom}",
                Mise = lot.Mise,
                EstVendu = lot.EstVendu,
                DateFinVente = lot.DateFinVente,
                DateDepot = lot.DateDepot,
                DateCreation = lot.DateCreation,
                IdClientMise = lot.IdClientMise,
                SeraLivree = lot.SeraLivree,
                Photos = lot.Photos.Select(p => new PhotoVM
                {
                    Id = p.Id,
                    Url = p.Lien
                }).ToList()
            };
        }

        public async Task<(bool Success, string Message, LotAffichageVM Lot)> CreerLot(LotCreationVM lotVM)
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
                estLivrable = lotVM.EstLivrable,
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
            await _context.SaveChangesAsync();

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

                await _context.SaveChangesAsync();
            }

            var lotAffichage = await ObtenirLot(lot.Id);
            return (true, "Lot créé avec succès", lotAffichage);
        }

        public async Task<(bool Success, string Message)> ModifierLot(int id, LotModificationVM lotVM)
        {
            var (isValid, errorMessage) = await LotValidation.ValidateLot(lotVM, _context);
            if (!isValid)
            {
                return (false, errorMessage);
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return (false, "Lot non trouvé");
            }

            // Mettre à jour les propriétés du lot
            lot.Numero = lotVM.Numero;
            lot.Description = lotVM.Description;
            lot.ValeurEstimeMin = lotVM.ValeurEstimeMin;
            lot.ValeurEstimeMax = lotVM.ValeurEstimeMax;
            lot.Artiste = lotVM.Artiste;
            lot.IdCategorie = lotVM.IdCategorie;
            lot.IdVendeur = lotVM.IdVendeur;
            lot.estLivrable = lotVM.EstLivrable;
            lot.Largeur = lotVM.Largeur;
            lot.Hauteur = lotVM.Hauteur;
            lot.IdMedium = lotVM.IdMedium;

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
                    }
                }
            }

            await _context.SaveChangesAsync();

            return (true, "Lot modifié avec succès");
        }

        public async Task<(bool Success, string Message)> SupprimerLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return (false, "Lot non trouvé");
            }

            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();

            return (true, "Lot supprimé avec succès");
        }
    }
}
