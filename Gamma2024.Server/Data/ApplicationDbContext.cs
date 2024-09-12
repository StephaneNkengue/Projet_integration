using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Encan> Encans { get; set; } = default!;
        public DbSet<EncanLot> EncanLots { get; set; } = default!;
        public DbSet<Lot> Lots { get; set; } = default!;
        public DbSet<Categorie> Categories { get; set; } = default!;
        public DbSet<Photo> Photos { get; set; } = default!;
        public DbSet<Facture> Factures { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Vendeur> Vendeurs { get; set; } = default!;
        public DbSet<Administrateur> Administrateurs { get; set; } = default!;
        public DbSet<Personne> Personnes { get; set; } = default!;
        public DbSet<Compte> Comptes { get; set; } = default!;
        public DbSet<CarteCredit> CartesCredits { get; set; } = default!;
        public DbSet<Adresse> Adresses { get; set; } = default!;
        public DbSet<Medium> Mediums { get; set; } = default!;
        public DbSet<Charite> Charites { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureRelationships(builder);
            CreateRolesAndUsers(builder);
        }

        private void ConfigureRelationships(ModelBuilder builder)
        {
            // Compte
            builder.Entity<Compte>()
                .HasOne(c => c.ApplicationUser)
                .WithOne()
                .HasForeignKey<Compte>(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasOne(au => au.Personne)
                .WithOne()
                .HasForeignKey<ApplicationUser>(au => au.PersonneId)
                .OnDelete(DeleteBehavior.NoAction);

            // Vendeur
            builder.Entity<Vendeur>()
                .HasOne(v => v.Personne)
                .WithOne(p => p.Vendeur)
                .HasForeignKey<Vendeur>(v => v.IdPersonne)
                .OnDelete(DeleteBehavior.NoAction);

            // Lot
            builder.Entity<Lot>()
                .HasOne(l => l.Categorie)
                .WithMany(c => c.Lots)
                .HasForeignKey(l => l.IdCategorie)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Lot>()
                .HasOne(l => l.ClientMise)
                .WithMany()
                .HasForeignKey(l => l.IdClientMise)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Lot>()
                .HasOne(l => l.Vendeur)
                .WithMany(v => v.Lots)
                .HasForeignKey(l => l.IdVendeur)
                .OnDelete(DeleteBehavior.NoAction);

            // EncanLot
            builder.Entity<EncanLot>()
                .HasKey(el => new { el.NumeroEncan, el.IdLot });

            builder.Entity<EncanLot>()
                .HasOne(el => el.Encan)
                .WithMany(e => e.EncanLots)
                .HasForeignKey(el => el.NumeroEncan);

            builder.Entity<EncanLot>()
                .HasOne(el => el.Lot)
                .WithMany(l => l.EncanLots)
                .HasForeignKey(el => el.IdLot)
                .OnDelete(DeleteBehavior.NoAction);

            // Facture
            builder.Entity<Facture>()
                .HasOne(f => f.Lot)
                .WithMany()
                .HasForeignKey(f => f.IdLot)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Facture>()
                .HasOne(f => f.Adresse)
                .WithMany(a => a.Factures)
                .HasForeignKey(f => f.IdAdresse)
                .OnDelete(DeleteBehavior.NoAction);

            // CarteCredit
            builder.Entity<CarteCredit>()
                .HasOne(cc => cc.Client)
                .WithMany(c => c.CarteCredits)
                .HasForeignKey(cc => cc.IdClient)
                .OnDelete(DeleteBehavior.NoAction);

            // Adresse
            builder.Entity<Adresse>()
                .HasOne(a => a.Personne)
                .WithMany(p => p.Adresses)
                .HasForeignKey(a => a.IdPersonne)
                .OnDelete(DeleteBehavior.NoAction);

            // Photo
            builder.Entity<Photo>()
                .HasOne(p => p.Lot)
                .WithMany(l => l.Photos)
                .HasForeignKey(p => p.IdLot)
                .OnDelete(DeleteBehavior.Cascade);

            // Medium
            builder.Entity<Medium>()
                .HasMany(m => m.Lots)
                .WithOne(l => l.Medium)
                .HasForeignKey(l => l.IdMedium)
                .OnDelete(DeleteBehavior.Restrict);

            // Charite
            builder.Entity<Charite>()
                .HasOne(c => c.Client)
                .WithOne(cl => cl.Charite)
                .HasForeignKey<Charite>(c => c.IdClient)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Charite>()
            .HasOne(c => c.Facture)
            .WithOne(f => f.Charite)
            .HasForeignKey<Charite>(c => c.IdFacture)
            .OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateRolesAndUsers(ModelBuilder builder)
        {
            var adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
            var clientId = "1d8ac862-e54d-4f10-b6f8-638808c02967";

            // Création des rôles
            string roleIdAdmin = "7da4163f-edb4-47b5-86ea-999999999999";
            string roleIdClient = "7da4163f-edb4-47b5-86ea-888888888888";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = ApplicationRoles.ADMINISTRATEUR,
                    NormalizedName = ApplicationRoles.ADMINISTRATEUR.ToUpper(),
                    Id = roleIdAdmin,
                    ConcurrencyStamp = roleIdAdmin
                },
                new IdentityRole
                {
                    Name = ApplicationRoles.CLIENT,
                    NormalizedName = ApplicationRoles.CLIENT.ToUpper(),
                    Id = roleIdClient,
                    ConcurrencyStamp = roleIdClient
                }
            );

            // Création des adresses
            builder.Entity<Adresse>().HasData(
                new Adresse
                {
                    Id = 1,
                    Numero = 123,
                    Rue = "Rue Admin",
                    Ville = "Ville Admin",
                    CodePostal = "12345",
                    Pays = "Pays Admin"
                },
                new Adresse
                {
                    Id = 2,
                    Numero = 456,
                    Rue = "Rue Client",
                    Ville = "Ville Client",
                    CodePostal = "67890",
                    Pays = "Pays Client"
                }
            );

            // Création des personnes
            builder.Entity<Personne>().HasData(
                new Personne
                {
                    Id = 1,
                    Nom = "Admin",
                    Prenom = "Super",
                    Courriel = "admin@example.com",
                    Telephone = "1234567890",
                    IdAdresse = 1
                },
                new Personne
                {
                    Id = 2,
                    Nom = "Client",
                    Prenom = "Test",
                    Courriel = "client@example.com",
                    Telephone = "0987654321",
                    IdAdresse = 2
                }
            );


            // Création de l'administrateur et du client avec des mots de passe hachés
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new Administrateur
            {
                Id = adminId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PersonneId = 1
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "MotDePasseAdmin123!");

            var clientUser = new Client
            {
                Id = clientId,
                UserName = "client@example.com",
                NormalizedUserName = "CLIENT@EXAMPLE.COM",
                Email = "client@example.com",
                NormalizedEmail = "CLIENT@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PersonneId = 2,
                EstBloque = false
            };
            clientUser.PasswordHash = passwordHasher.HashPassword(clientUser, "MotDePasseClient123!");

            builder.Entity<Administrateur>().HasData(adminUser);
            builder.Entity<Client>().HasData(clientUser);

            // Attribution des rôles
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = roleIdAdmin,
                    UserId = adminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = roleIdClient,
                    UserId = clientId
                }
            );

            // Création des comptes
            builder.Entity<Compte>().HasData(
                new Compte
                {
                    Id = 1,
                    NomUtilisateur = "admin@example.com",
                    Pseudonyme = "SuperAdmin",
                    Avatar = "admin.png",
                    MotPasse = "HashedPasswordHere",
                    ApplicationUserId = adminId
                },
                new Compte
                {
                    Id = 2,
                    NomUtilisateur = "client@example.com",
                    Pseudonyme = "TestClient",
                    Avatar = "client.png",
                    MotPasse = "HashedPasswordHere",
                    ApplicationUserId = clientId
                }
            );
        }
    }
}
