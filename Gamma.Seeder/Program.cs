using Gamma2024.Seeder;
using Gamma2024.Server.Extensions;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Identity;

var context = DbContextFactory.CreateDbContext();

Console.WriteLine("Début du seed...");

Console.WriteLine("Ajout des utilisateurs");

var passwordHasher = new PasswordHasher<ApplicationUser>();

// Récupérer tous les noms d'utilisateurs existants
var existingUsernames = context.Users.Select(u => u.UserName).ToList();

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
                        .GroupBy(u => u.UserName) // Regrouper par nom d'utilisateur
                        .Select(g => g.First())   // Prendre le premier de chaque groupe
                        .ToList();

// Vérifier si les utilisateurs existent déjà avant de les ajouter
foreach (var user in utilisateurs)
{
    if (!existingUsernames.Contains(user.UserName))
    {
        context.Users.Add(user);
        Console.WriteLine($"L'utilisateur {user.UserName} a été ajouté.");
    }
    else
    {
        Console.WriteLine($"L'utilisateur {user.UserName} existe déjà et sera ignoré.");
    }
}

context.SaveChanges();

Console.WriteLine("Ajout des roles aux utilisateurs");

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
        Console.WriteLine($"L'utilisateur avec l'ID {item.Id} n'existe pas dans la base de données et sera ignoré pour l'attribution du rôle.");
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

// Ajoutez d'abord les adresses
var adresses = vendeurs.Select(v => v.Adresse).ToList();
context.Adresses.AddRange(adresses);
context.SaveChanges();

// Ensuite, mettez à jour les vendeurs avec les IDs d'adresse générés
foreach (var vendeur in vendeurs)
{
    vendeur.AdresseId = vendeur.Adresse.Id;
    vendeur.Adresse = null; // Détachez l'objet Adresse pour éviter une double insertionx   
}

context.Vendeurs.AddRange(vendeurs);
context.SaveChanges();

Console.WriteLine("Ajout des categories");

var categoriesLotsUniques = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetCategories()
                        .GroupBy(c => c.Nom)
                        .Select(c => c.First())
                        .ToList();

context.Categories.AddRange(categoriesLotsUniques);
context.SaveChanges();

Console.WriteLine("Ajout des médiums");

var mediumsLotsUniques = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetMediums()
                        .GroupBy(m => m.Type)
                        .Select(m => m.First())
                        .ToList();

context.Mediums.AddRange(mediumsLotsUniques);
context.SaveChanges();

Console.WriteLine("Ajout des lots");

// Fonction helper pour ajouter les photos aux lots
void AjouterPhotosAuxLots(List<Lot> lots, IEnumerable<IEnumerable<string>> imagesLots, int numeroEncan)
{
    for (int i = 0; i < lots.Count; i++)
    {
        foreach (var nomImage in imagesLots.ElementAt(i))
        {
            if (string.IsNullOrEmpty(nomImage))
            {
                break;
            }
            else
            {
                var imagePath = $"/Images/ImagesEncan{numeroEncan}/{nomImage}";
                lots[i].Photos.Add(new Photo
                {
                    Lien = imagePath
                });
            }
        }
    }
}

// Utilisation pour l'encan 232
var lotsVendeurs232 = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan232()
                .ToList();

var imagesLots232 = File.ReadAllLines("CSV/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetImagesParLotParEncan(232).Select(x => x.ToList()).ToList();

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

AjouterPhotosAuxLots(lots232, imagesLots232, 232);

context.Lots.AddRange(lots232);
context.SaveChanges();

// Utilisation pour l'encan 233
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

AjouterPhotosAuxLots(lots233, imagesLots233.Select(x => x.ToList()).ToList(), 233);

context.Lots.AddRange(lots233);
context.SaveChanges();

Console.WriteLine("Ajout des encans");
var encans = new List<Encan>();

encans.Add(new Encan
{
    NumeroEncan = 232,
    DateDebut = new DateTime(2005, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2005, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2005, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2005, 3, 18, 9, 0, 0),
    EstPublie = true
});

encans.Add(new Encan
{
    NumeroEncan = 233,
    DateDebut = new DateTime(2024, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2025, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2025, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2025, 3, 18, 9, 0, 0),
    EstPublie = true
});

encans.Add(new Encan
{
    NumeroEncan = 234,
    DateDebut = new DateTime(2007, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2007, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2007, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2007, 3, 18, 9, 0, 0),
    EstPublie = true
});

encans.Add(new Encan
{
    NumeroEncan = 235,
    DateDebut = new DateTime(2008, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2008, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2008, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2008, 3, 18, 9, 0, 0),
    EstPublie = true
});


context.Encans.AddRange(encans);
context.SaveChanges();

Console.WriteLine("Association des lots au encans respectifs");
var encanLots = new List<EncanLot>();

foreach (var item in lots232)
{
    encanLots.Add(new EncanLot
    {
        Lot = item,
        IdLot = item.Id,
        Encan = encans[0],
        IdEncan = encans[0].Id
    });
}

foreach (var item in lots233)
{
    encanLots.Add(new EncanLot
    {
        Lot = item,
        IdLot = item.Id,
        Encan = encans[1],
        IdEncan = encans[1].Id
    });
}

context.EncanLots.AddRange(encanLots);
context.SaveChanges();

Console.WriteLine("Ajout des charités");
var charites = new List<Charite>();

charites.Add(new Charite
{
    NomOrganisme = "Le phare des rives"
});
charites.Add(new Charite
{
    NomOrganisme = "Un petit pas pour l'avenir"
});
charites.Add(new Charite
{
    NomOrganisme = "Rendez-vous dans 30 ans"
});

context.Charites.AddRange(charites);
context.SaveChanges();


Console.WriteLine("Ajout des factures");
var infoFactures = File.ReadAllLines("CSV/AcheteurEncan232.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetAcheteursEncan232()
                .ToList();

var factures = new List<Facture>();

foreach (var item in utilisateurs)
{
    var achats = infoFactures.FindAll(i => i.Pseudonyme == item.UserName).ToList();

    if (achats.Any())
    {
        var facture = new Facture
        {
            IdClient = item.Id,
            Client = item,
            IdAdresse = item.Adresses.First().Id,
            Adresse = item.Adresses.First(),
            DateAchat = DateTime.Now,
            PrixLots = 0    
        };

        foreach (var a in achats)
        {
            var lot = lots232.FirstOrDefault(l => l.Numero == a.Lot);
            lot.EstVendu = true;
            lot.Mise = a.PrixAchete;
            lot.IdClientMise = item.Id;
            lot.ClientMise = item;
            lot.SeraLivree = a.Livraison;
            lot.DateFinVente = DateTime.Now;
            facture.Lots.Add(lot);
        }

        facture.CalculerFacture();
        factures.Add(facture);
        infoFactures.RemoveAll(i => i.Pseudonyme == item.UserName);
    }
}

context.Factures.AddRange(factures);
context.SaveChanges();

context.Lots.UpdateRange(lots232);
context.SaveChanges();

Console.WriteLine("Fin du seed");
