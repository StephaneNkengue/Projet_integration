using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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
        public DbSet<CarteCredit> CartesCredit { get; set; } = default!;
        public DbSet<Adresse> Adresses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurations des relations
            ConfigureRelationships(builder);

            // Création des rôles et utilisateurs
            CreateRolesAndUsers(builder);
        }

        private void ConfigureRelationships(ModelBuilder builder)
        {
            
            builder.Entity<EncanLot>()
                .HasKey(el => new { el.NumeroEncan, el.IdLot });

            builder.Entity<EncanLot>()
                .HasOne(el => el.Encan)
                .WithMany(e => e.EncanLots)
                .HasForeignKey(el => el.NumeroEncan);

            builder.Entity<EncanLot>()
                .HasOne(el => el.Lot)
                .WithMany(l => l.EncanLots)
                .HasForeignKey(el => el.IdLot);

            builder.Entity<EncanLot>()
            .HasOne(el => el.Encan)
            .WithMany()
            .HasForeignKey(el => el.NumeroEncan)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EncanLot>()
                        .HasOne(el => el.Lot)
                        .WithMany()
                        .HasForeignKey(el => el.IdLot)
                        .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Lot>()
                        .HasOne(l => l.Categorie)
                        .WithMany(c => c.Lots)
                        .HasForeignKey(l => l.IdCategorie)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Lot>()
                        .HasOne(l => l.Vendeur)
                        .WithMany(v => v.Lots)
                        .HasForeignKey(l => l.IdVendeur)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Photo>()
                .HasOne(p => p.Lot)
                .WithMany(l => l.Photos)
                .HasForeignKey(p => p.IdLot)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Administrateur>()
                .HasOne(a => a.Compte)
                .WithOne(c => c.Administrateur)
                .HasForeignKey<Administrateur>(a => a.IdCompte);

            builder.Entity<Client>()
                .HasOne(c => c.Compte)
                .WithOne(c => c.Client)
                .HasForeignKey<Client>(c => c.IdCompte);  

                      

  
  
          builder.Entity<Personne>()
                .HasOne(p => p.Adresse)
                .WithOne()
                .HasForeignKey<Adresse>(a => a.IdPersonne)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Personne>()
                .HasOne(p => p.Client)
                .WithOne(c => c.Personne)
                .HasForeignKey<Client>(c => c.IdPersonne)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Personne>()
                .HasOne(p => p.Vendeur)
                .WithOne(v => v.Personne)
                .HasForeignKey<Vendeur>(v => v.IdPersonne)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Personne>()
                .HasOne(p => p.Administrateur)
                .WithOne()
                .HasForeignKey<Administrateur>(a => a.IdPersonne)
                .OnDelete(DeleteBehavior.Cascade);
            

        }

        private static void CreateRolesAndUsers(ModelBuilder builder)
        {
            string adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
            string clientId = "1d8ac862-e54d-4f10-b6f8-638808c02967";

            // Création des rôles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = ApplicationRoles.ADMINISTRATEUR, NormalizedName = ApplicationRoles.ADMINISTRATEUR.ToUpper() },
                new IdentityRole { Id = "2", Name = ApplicationRoles.CLIENT, NormalizedName = ApplicationRoles.CLIENT.ToUpper() }
            );

            // Création des utilisateurs
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminId,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "AdminPass123!"),
                    SecurityStamp = string.Empty
                },
                new ApplicationUser
                {
                    Id = clientId,
                    UserName = "client@example.com",
                    NormalizedUserName = "CLIENT@EXAMPLE.COM",
                    Email = "client@example.com",
                    NormalizedEmail = "CLIENT@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "ClientPass123!"),
                    SecurityStamp = string.Empty
                }
            );

            // Attribution des rôles aux utilisateurs
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminId, RoleId = "1" },
                new IdentityUserRole<string> { UserId = clientId, RoleId = "2" }
            );
        }
    }
}
