using Gamma2024.Seeder;
using Gamma2024.Server.Extensions;
using Gamma2024.Server.Models;

var context = DbContextFactory.CreateDbContext();

Console.WriteLine("Début du seed...");


Console.WriteLine("Ajout des vendeurs");

var vendeurs = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToVendeur()
                .ToList();

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

var lotsVendeurs232 = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan232()
                .ToList();

var lotsVendeurs233 = File.ReadAllLines("CSV/Vendeurs.csv", System.Text.Encoding.GetEncoding("iso-8859-1"))
                .Skip(1)
                .Where(l => l.Length > 1)
                .GetNumeroLotsEncan233()
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
                                }
                                position++;
                            }
                            return l;
                        })
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

context.Lots.AddRange(lots232);
context.SaveChanges();

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
});

encans.Add(new Encan
{
    NumeroEncan = 233,
    DateDebut = new DateTime(2006, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2006, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2006, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2006, 3, 18, 9, 0, 0),
});

encans.Add(new Encan
{
    NumeroEncan = 234,
    DateDebut = new DateTime(2007, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2007, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2007, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2007, 3, 18, 9, 0, 0),
});

encans.Add(new Encan
{
    NumeroEncan = 235,
    DateDebut = new DateTime(2008, 3, 15, 6, 0, 0),
    DateFin = new DateTime(2008, 3, 18, 6, 0, 0),
    DateDebutSoireeCloture = new DateTime(2008, 3, 18, 6, 0, 1),
    DateFinSoireeCloture = new DateTime(2008, 3, 18, 9, 0, 0),
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

Console.WriteLine("Fin du seed");