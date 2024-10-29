using Gamma2024.Seeder;
using Gamma2024.Server.Extensions;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var context = DbContextFactory.CreateDbContext();

Console.WriteLine("Début du seed...");

Console.WriteLine("Ajout des utilisateurs");

var passwordHasher = new PasswordHasher<ApplicationUser>();

var utilisateurs = File.ReadAllLines("CSV/Acheteurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(x => x.Length > 1)
                        .ToApplicationUser()
                        .Select(u =>
                        {
                            u.CarteCredits = new CarteCredit[]
                            {
                                new() {
                                AnneeExpiration=(DateTime.Now.Year)+2,
                                MoisExpiration=DateTime.Now.Month,
                                Nom = u.FirstName + " " + u.Name,
                                Numero="4242424242424242"
                                }
                            };
                            u.PasswordHash = passwordHasher.HashPassword(u, u.UserName + u.Adresses.First().Numero);
                            return u;
                        })
                        .ToList();

// Vérifier et ajouter uniquement les nouveaux utilisateurs
foreach (var utilisateur in utilisateurs)
{
    if (!context.Users.Any(u => u.UserName == utilisateur.UserName))
    {
        context.Users.Add(utilisateur);
    }
}

context.SaveChanges();

Console.WriteLine("Ajout des rôles aux utilisateurs");

string roleIdClient = "7da4163f-edb4-47b5-86ea-888888888888";

var utilisateursExistants = context.Users.Select(u => u.Id).ToList();

var utilisateursRoles = new List<IdentityUserRole<string>>();
foreach (var item in utilisateurs)
{
    if (utilisateursExistants.Contains(item.Id))
    {
        utilisateursRoles.Add(new IdentityUserRole<string>
        {
            RoleId = roleIdClient,
            UserId = item.Id,
        });
    }
    else
    {
        Console.WriteLine($"L'utilisateur avec l'ID {item.Id} n'existe pas dans la base de données et sera ignoré pour l'attribution de rôle.");
    }
}

context.UserRoles.AddRange(utilisateursRoles);
context.SaveChanges();

Console.WriteLine("Ajout des vendeurs");

var vendeurs = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToVendeur()
                .ToList();

// Vérifier et ajouter uniquement les nouveaux vendeurs
foreach (var vendeur in vendeurs)
{
    if (vendeur.Adresse != null)
    {
        var adresseExistante = context.Adresses.FirstOrDefault(a => a.Id == vendeur.Adresse.Id);
        if (adresseExistante == null)
        {
            context.Adresses.Add(vendeur.Adresse);
        }
        else
        {
            vendeur.Adresse = adresseExistante;
        }
        vendeur.AdresseId = vendeur.Adresse.Id;
    }
}

// Ajout des vendeurs uniques
foreach (var vendeur in vendeurs)
{
    if (!context.Vendeurs.Any(v => v.Id == vendeur.Id))
    {
        context.Vendeurs.Add(vendeur);
    }
}

context.SaveChanges();

Console.WriteLine("Ajout des catégories");

var categoriesLotsUniques = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetCategories()
                        .GroupBy(c => c.Nom)
                        .Select(c => c.First())
                        .ToList();

// Ajout des catégories uniques
foreach (var category in categoriesLotsUniques)
{
    if (!context.Categories.Any(c => c.Nom == category.Nom))
    {
        context.Categories.Add(category);
    }
}

context.SaveChanges();

Console.WriteLine("Ajout des médiums");

var mediumsLotsUniques = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetMediums()
                        .GroupBy(m => m.Type)
                        .Select(m => m.First())
                        .ToList();

// Ajout des médiums uniques
foreach (var medium in mediumsLotsUniques)
{
    if (!context.Mediums.Any(m => m.Type == medium.Type))
    {
        context.Mediums.Add(medium);
    }
}

context.SaveChanges();

Console.WriteLine("Ajout des lots");

// Processus similaire pour les lots encan 232
var lotsVendeurs232 = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan232()
                .ToList();

var imagesLots232 = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetImagesParLotParEncan(232)
                        .ToList();

var lots232 = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .ToLotParEncan(232)
                        .Select(l =>
                        {
                            foreach (var item in categoriesLotsUniques)
                            {
                                if (l.Categorie.Nom == item.Nom)
                                {
                                    l.IdCategorie = item.Id;
                                    l.Categorie = item;
                                    break;
                                }
                            }
                            foreach (var item in mediumsLotsUniques)
                            {
                                if (l.Medium.Type == item.Type)
                                {
                                    l.IdMedium = item.Id;
                                    l.Medium = item;
                                    break;
                                }
                            }
                            var position = 0;
                            foreach (var item in lotsVendeurs232)
                            {
                                if (item.Contains(l.Numero))
                                {
                                    l.IdVendeur = vendeurs[position].Id;
                                    l.Vendeur = vendeurs[position];
                                    break;
                                }
                                position++;
                            }
                            return l;
                        })
                        .ToList();

// Ajout des lots uniques
foreach (var lot in lots232)
{
    if (!context.Lots.Any(l => l.Numero == lot.Numero))
    {
        context.Lots.Add(lot);
    }
}

context.SaveChanges();

var acheteurs232 = File.ReadAllLines("CSV/AcheteurEncan232.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetAcheteursEncan232()
                        .ToList();

for (int i = 0; i < lots232.Count; i++)
{
    foreach (var nomImage in imagesLots232[i])
    {
        if (nomImage.IsNullOrEmpty())
        {
            break;
        }
        else
        {
            var imagePath = Path.Combine("Images", "ImagesEncan232", nomImage);
            lots232[i].Photos.Add(new Photo
            {
                Lien = imagePath
            });
        }
    }
}

context.SaveChanges();

var lotsVendeurs233 = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan233()
                .ToList();

var imagesLots233 = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetImagesParLotParEncan(233)
                        .ToList();

var lots233 = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .ToLotParEncan(233)
                        .Select(l =>
                        {
                            foreach (var item in categoriesLotsUniques)
                            {
                                if (l.Categorie.Nom == item.Nom)
                                {
                                    l.IdCategorie = item.Id;
                                    l.Categorie = item;
                                    break;
                                }
                            }
                            foreach (var item in mediumsLotsUniques)
                            {
                                if (l.Medium.Type == item.Type)
                                {
                                    l.IdMedium = item.Id;
                                    l.Medium = item;
                                    break;
                                }
                            }
                            var position = 0;
                            foreach (var item in lotsVendeurs233)
                            {
                                if (item.Contains(l.Numero))
                                {
                                    l.IdVendeur = vendeurs[position].Id;
                                    l.Vendeur = vendeurs[position];
                                    break;
                                }
                                position++;
                            }
                            return l;
                        })
                        .ToList();

// Ajout des lots uniques pour l'encan 233
foreach (var lot in lots233)
{
    if (!context.Lots.Any(l => l.Numero == lot.Numero))
    {
        context.Lots.Add(lot);
    }
}

context.SaveChanges();
Console.WriteLine("Fin du seed");
