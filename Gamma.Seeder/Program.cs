using Gamma2024.Seeder;
using Gamma2024.Server.Extensions;

var context = DbContextFactory.CreateDbContext();

Console.WriteLine("Début du seed...");

//context.Vendeurs.RemoveRange();
//context.SaveChanges();

var vendeurs = File.ReadAllLines("CSV/Vendeurs.csv")
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToVendeur()
                .ToList();

//context.Vendeurs.AddRange(vendeurs);
//context.SaveChanges();

Console.WriteLine("Fin du seed");