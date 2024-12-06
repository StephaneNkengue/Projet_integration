using Gamma2024.Seeder;
using Gamma2024.Server.Extensions;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stripe;

var context = DbContextFactory.CreateDbContext();
var builder = WebApplication.CreateBuilder(args);

// Charger les fichiers de configuration selon l'environnement
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

Console.WriteLine("Voulez vous vider la BD? Entrez 1 pour oui et autre pour non");
var choix = Console.ReadLine();

if (choix == "1")
{
    Console.WriteLine("Vider les données BD...");
    context.Encans.RemoveRange(context.Encans);
    context.Lots.RemoveRange(context.Lots);
    context.EncanLots.RemoveRange(context.EncanLots);
    context.Categories.RemoveRange(context.Categories);
    context.Mediums.RemoveRange(context.Mediums);
    context.Photos.RemoveRange(context.Photos);
    context.FactureLivraisons.RemoveRange(context.FactureLivraisons);
    context.Factures.RemoveRange(context.Factures);
    context.Charites.RemoveRange(context.Charites);
    context.Adresses.RemoveRange(context.Adresses);
    context.Vendeurs.RemoveRange(context.Vendeurs);
    context.Users.RemoveRange(context.Users.Where(c => c.Id != "8e445865-a24d-4543-a6c6-9443d048cdb9" && c.Id != "1d8ac862-e54d-4f10-b6f8-638808c02967"));
    context.Notifications.RemoveRange(context.Notifications);
    context.MiseAutomatiques.RemoveRange(context.MiseAutomatiques);

    context.SaveChanges();
}
else
{
    Console.WriteLine("Garder les données du BD");
}


var customerService = new CustomerService();
var options = new CustomerListOptions { Limit = 100 };
StripeList<Customer> customersTemp = customerService.List(options);
StripeList<Customer> customers = customersTemp;

while (customersTemp.Data.Count == 100)
{
    customers.Data.AddRange(customersTemp.Data);
    customersTemp = customerService.List(new CustomerListOptions { Limit = 100, StartingAfter = customers.Data.Last().Id });
}

Console.WriteLine("Voulez vous supprimer les clients Stripe? Entre 1 pour oui et autre pour non");
choix = Console.ReadLine();

if (choix == "1")
{
    Console.WriteLine("Suppression des clients Stripe...");
    foreach (var customer in customers.Data)
    {
        customerService.Delete(customer.Id);
    }
}
else
{
    Console.WriteLine("Garder les clients Stripe");
}

var usersExistants = context.Users.Include(u => u.Adresses);

foreach (var user in usersExistants)
{
    if (user.StripeCustomer.IsNullOrEmpty())
    {
        var stripeCustomer = customers.Data.Find(c => c.Email.Equals(user.Email));

        if (stripeCustomer != null)
        {
            user.StripeCustomer = stripeCustomer.Id;
        }
        else
        {
            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.Name,
                Description = user.UserName,
                PreferredLocales = ["fr-CA"],
                PaymentMethod = "pm_card_visa",
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = "pm_card_visa",
                },
                Address = new AddressOptions
                {
                    City = user.Adresses.First().Ville,
                    Country = user.Adresses.First().Pays,
                    Line1 = $"{user.Adresses.First().Numero} {user.Adresses.First().Rue}",
                    //Line2 = user.Adresses.First().Appartement,
                    PostalCode = user.Adresses.First().CodePostal,
                    State = user.Adresses.First().Province
                }
            });
            user.StripeCustomer = customer.Id;
            context.Update(user);

        }
    }
}

Console.WriteLine("Début du seed...");

Console.WriteLine("Ajout des utilisateurs");


var passwordHasher = new PasswordHasher<ApplicationUser>();

var utilisateurs = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Acheteurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(x => x.Length > 1)
                        .ToApplicationUser()
                        .Select(u =>
                        {
                            var stripeCustomer = customers.Data.Find(c => c.Email.Equals(u.Email));

                            if (stripeCustomer != null)
                            {
                                u.StripeCustomer = stripeCustomer.Id;
                            }
                            else
                            {
                                var options = new CustomerCreateOptions()
                                {
                                    Email = u.Email,
                                    Name = u.FirstName + " " + u.Name,
                                    Description = u.UserName,
                                    PreferredLocales = ["fr-CA"],
                                    PaymentMethod = "pm_card_visa",
                                    InvoiceSettings = new CustomerInvoiceSettingsOptions
                                    {
                                        DefaultPaymentMethod = "pm_card_visa",
                                    },
                                    Address = new AddressOptions
                                    {
                                        City = u.Adresses.First().Ville,
                                        Country = u.Adresses.First().Pays,
                                        Line1 = $"{u.Adresses.First().Numero} {u.Adresses.First().Rue}",
                                        Line2 = u.Adresses.First().Appartement,
                                        PostalCode = u.Adresses.First().CodePostal,
                                        State = u.Adresses.First().Province
                                    }
                                };
                                var customer = customerService.Create(options);
                                u.StripeCustomer = customer.Id;
                            }

                            u.PasswordHash = passwordHasher.HashPassword(u, u.UserName + u.Adresses.First().Numero);
                            return u;
                        })
                        .ToList();

context.Users.AddRange(utilisateurs);
context.SaveChanges();

Console.WriteLine("Ajout des roles au utilisateurs");

string roleIdClient = "7da4163f-edb4-47b5-86ea-888888888888";

var utilisateursRoles = new List<IdentityUserRole<string>>();
foreach (var item in utilisateurs)
{
    utilisateursRoles.Add(new IdentityUserRole<string>
    {
        RoleId = roleIdClient,
        UserId = item.Id,
    });
}

context.UserRoles.AddRange(utilisateursRoles);
context.SaveChanges();

Console.WriteLine("Ajout des vendeurs");

var vendeurs = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToVendeur()
                .ToList();

context.Vendeurs.AddRange(vendeurs);
context.SaveChanges();

Console.WriteLine("Ajout des categories");

var categoriesLotsUniques = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetCategories()
                        .GroupBy(c => c.Nom)
                        .Select(c => c.First())
                        .ToList();

context.Categories.AddRange(categoriesLotsUniques);
context.SaveChanges();

Console.WriteLine("Ajout des médiums");

var mediumsLotsUniques = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetMediums()
                        .GroupBy(m => m.Type)
                        .Select(m => m.First())
                        .ToList();

context.Mediums.AddRange(mediumsLotsUniques);
context.SaveChanges();

Console.WriteLine("Ajout des lots");

var lotsVendeurs232 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan232()
                .ToList();

var imagesLots232 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetImagesParLotParEncan(232)
                        .ToList();

var lots232 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
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

var acheteurs232 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/AcheteurEncan232.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
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

context.Lots.AddRange(lots232);
context.SaveChanges();

var lotsVendeurs233 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan233()
                .ToList();

var imagesLots233 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                        .Skip(1)
                        .Where(l => l.Length > 1)
                        .GetImagesParLotParEncan(233)
                        .ToList();

var lots233 = System.IO.File.ReadAllLines("CSV/DonneesOriginal/Encan232et233.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
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

for (int i = 0; i < lots233.Count; i++)
{
    foreach (var nomImage in imagesLots233[i])
    {
        if (nomImage.IsNullOrEmpty())
        {
            break;
        }
        else
        {
            var imagePath = Path.Combine("Images", "ImagesEncan233", nomImage);
            lots233[i].Photos.Add(new Photo
            {
                Lien = imagePath
            });
        }
    }
}

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
    EstPublie = true,
    PasLot = 30,
    PasMise = 120
});

encans.Add(new Encan
{
    NumeroEncan = 233,
    DateDebut = new DateTime(2024, 3, 15, 6, 0, 0),
    DateFin = DateTime.Now.AddDays(1),
    DateDebutSoireeCloture = DateTime.Now.AddDays(1),
    EstPublie = true,
    PasLot = 10,
    PasMise = 120
});

encans.Add(new Encan
{
    NumeroEncan = 234,
    DateDebut = new DateTime(2027, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2027, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2027, 3, 18, 6, 0, 1),
    EstPublie = true,
    PasLot = 20,
    PasMise = 120
});

encans.Add(new Encan
{
    NumeroEncan = 235,
    DateDebut = new DateTime(2008, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2008, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2008, 3, 18, 6, 0, 1),
    EstPublie = true,
    PasLot = 30,
    PasMise = 120
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

context.Encans.Update(encans[0]);
context.SaveChanges();

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

context.Encans.Update(encans[1]);

context.EncanLots.AddRange(encanLots);
context.SaveChanges();


///Ajout des decomptes 
var nbLot233 = 0;
foreach (var item in lots233)
{
    nbLot233++;
    item.DateDebutDecompteLot = encans[1].DateDebutSoireeCloture;
    item.DateFinDecompteLot = encans[1].DateDebutSoireeCloture + TimeSpan.FromSeconds(encans[1].PasLot * nbLot233);
}

context.UpdateRange(lots233);
context.SaveChanges();

///// 
var nbLot232 = 0;
foreach (var item in lots232)
{
    nbLot232++;
    item.DateDebutDecompteLot = encans[0].DateDebutSoireeCloture;
    item.DateFinDecompteLot = encans[0].DateDebutSoireeCloture + TimeSpan.FromSeconds(encans[0].PasLot * nbLot232);
}

context.UpdateRange(lots232);
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
var infoFactures = System.IO.File.ReadAllLines("CSV/DonneesOriginal/AcheteurEncan232.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetAcheteursEncan232()
                .ToList();

foreach (var item in utilisateurs)
{
    var achats = infoFactures.FindAll(i => i.Pseudonyme == item.UserName).ToList();

    if (achats.Any())
    {
        var facture = new Facture
        {
            IdClient = item.Id,
            Client = item,
            DateAchat = DateTime.Now,
            PrixLots = 0,
            NumeroEncan = 232,
            estPaye = true
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
        context.Factures.Add(facture);
        context.SaveChanges();

        infoFactures.RemoveAll(i => i.Pseudonyme == item.UserName);
    }
}


context.Lots.UpdateRange(lots232);
context.SaveChanges();

Console.WriteLine("Fin du seed");