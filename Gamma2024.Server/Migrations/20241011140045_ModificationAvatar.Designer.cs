﻿// <auto-generated />
using System;
using Gamma2024.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241011140045_ModificationAvatar")]
    partial class ModificationAvatar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gamma2024.Server.Models.Adresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Appartement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<bool>("EstDomicile")
                        .HasColumnType("bit");

                    b.Property<string>("IdApplicationUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IdPersonne")
                        .HasColumnType("int");

                    b.Property<int?>("IdVendeur")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicationUser");

                    b.ToTable("Adresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CodePostal = "12345",
                            EstDomicile = false,
                            IdApplicationUser = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            Numero = 123,
                            Pays = "Pays Admin",
                            Province = "Québec",
                            Rue = "Rue Admin",
                            Ville = "Ville Admin"
                        },
                        new
                        {
                            Id = 2,
                            CodePostal = "67890",
                            EstDomicile = false,
                            IdApplicationUser = "1d8ac862-e54d-4f10-b6f8-638808c02967",
                            Numero = 456,
                            Pays = "Pays Client",
                            Province = "Québec",
                            Rue = "Rue Client",
                            Ville = "Ville Client"
                        });
                });

            modelBuilder.Entity("Gamma2024.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAdresse")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            Avatar = "/Avatars/default.png",
                            ConcurrencyStamp = "f51adc97-086e-4dc9-b9fb-708773f8af06",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            FirstName = "Super",
                            IdAdresse = 1,
                            LockoutEnabled = false,
                            Name = "Admin",
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHwnfctFRZwvdnZSGH/R0bxoUB9ypTjbrGIre5oqaQOc65dt/LLoGOxX/o9n3n3jhA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2c342a40-8273-4d42-9f2a-4b3b0915fe5b",
                            TwoFactorEnabled = false,
                            UserName = "admin@example.com"
                        },
                        new
                        {
                            Id = "1d8ac862-e54d-4f10-b6f8-638808c02967",
                            AccessFailedCount = 0,
                            Avatar = "/Avatars/default.png",
                            ConcurrencyStamp = "18999dd0-c1ee-4380-9cd0-c559cc031f81",
                            Email = "client@example.com",
                            EmailConfirmed = true,
                            FirstName = "Jean",
                            IdAdresse = 2,
                            LockoutEnabled = false,
                            Name = "Dupont",
                            NormalizedEmail = "CLIENT@EXAMPLE.COM",
                            NormalizedUserName = "CLIENT@EXAMPLE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEFss2pWTfS7oIdww59XIVdt9gNPijIjaDnh97MF3t75EqL86AmoysILJMiA1tKRTqQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3400ad4f-b787-41e6-91eb-fb11e581de15",
                            TwoFactorEnabled = false,
                            UserName = "client@example.com"
                        });
                });

            modelBuilder.Entity("Gamma2024.Server.Models.CarteCredit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnneeExpiration")
                        .HasColumnType("int");

                    b.Property<string>("IdClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MoisExpiration")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.ToTable("CartesCredits");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Charite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdFacture")
                        .HasColumnType("int");

                    b.Property<double>("MontantDon")
                        .HasColumnType("float");

                    b.Property<string>("NomOrganisme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdClient")
                        .IsUnique();

                    b.HasIndex("IdFacture")
                        .IsUnique();

                    b.ToTable("Charites");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Encan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDebutSoireeCloture")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFinSoireeCloture")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroEncan")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Encans");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.EncanLot", b =>
                {
                    b.Property<int>("NumeroEncan")
                        .HasColumnType("int");

                    b.Property<int>("IdLot")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("NumeroEncan", "IdLot");

                    b.HasIndex("IdLot");

                    b.ToTable("EncanLots");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Facture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAchat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EstLivree")
                        .HasColumnType("bit");

                    b.Property<int>("IdAdresse")
                        .HasColumnType("int");

                    b.Property<string>("IdClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdLot")
                        .HasColumnType("int");

                    b.Property<int>("NumeroFacture")
                        .HasColumnType("int");

                    b.Property<double>("PrixTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdAdresse");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdLot");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Artiste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDepot")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFinVente")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dimensions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstVendu")
                        .HasColumnType("bit");

                    b.Property<int>("IdCategorie")
                        .HasColumnType("int");

                    b.Property<string>("IdClientMise")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdMedium")
                        .HasColumnType("int");

                    b.Property<int>("IdVendeur")
                        .HasColumnType("int");

                    b.Property<double?>("Mise")
                        .HasColumnType("float");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValeurEstimeMax")
                        .HasColumnType("float");

                    b.Property<double>("ValeurEstimeMin")
                        .HasColumnType("float");

                    b.Property<bool>("estLivrable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdCategorie");

                    b.HasIndex("IdClientMise");

                    b.HasIndex("IdMedium");

                    b.HasIndex("IdVendeur");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Medium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mediums");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdLot")
                        .HasColumnType("int");

                    b.Property<string>("Lien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdLot");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Vendeur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdresseId")
                        .HasColumnType("int");

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdresseId")
                        .IsUnique();

                    b.ToTable("Vendeurs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "7da4163f-edb4-47b5-86ea-999999999999",
                            ConcurrencyStamp = "7da4163f-edb4-47b5-86ea-999999999999",
                            Name = "Administrateur",
                            NormalizedName = "ADMINISTRATEUR"
                        },
                        new
                        {
                            Id = "7da4163f-edb4-47b5-86ea-888888888888",
                            ConcurrencyStamp = "7da4163f-edb4-47b5-86ea-888888888888",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            RoleId = "7da4163f-edb4-47b5-86ea-999999999999"
                        },
                        new
                        {
                            UserId = "1d8ac862-e54d-4f10-b6f8-638808c02967",
                            RoleId = "7da4163f-edb4-47b5-86ea-888888888888"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Adresse", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Adresses")
                        .HasForeignKey("IdApplicationUser")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.CarteCredit", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", "Client")
                        .WithMany("CarteCredits")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Charite", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", "Client")
                        .WithOne()
                        .HasForeignKey("Gamma2024.Server.Models.Charite", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.Facture", "Facture")
                        .WithOne("Charite")
                        .HasForeignKey("Gamma2024.Server.Models.Charite", "IdFacture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Facture");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.EncanLot", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.Lot", "Lot")
                        .WithMany("EncanLots")
                        .HasForeignKey("IdLot")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.Encan", "Encan")
                        .WithMany("EncanLots")
                        .HasForeignKey("NumeroEncan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Encan");

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Facture", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.Adresse", "Adresse")
                        .WithMany("Factures")
                        .HasForeignKey("IdAdresse")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", "Client")
                        .WithMany()
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.Lot", "Lot")
                        .WithMany()
                        .HasForeignKey("IdLot")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Adresse");

                    b.Navigation("Client");

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Lot", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.Categorie", "Categorie")
                        .WithMany("Lots")
                        .HasForeignKey("IdCategorie")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", "ClientMise")
                        .WithMany()
                        .HasForeignKey("IdClientMise")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Gamma2024.Server.Models.Medium", "Medium")
                        .WithMany("Lots")
                        .HasForeignKey("IdMedium")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.Vendeur", "Vendeur")
                        .WithMany("Lots")
                        .HasForeignKey("IdVendeur")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("ClientMise");

                    b.Navigation("Medium");

                    b.Navigation("Vendeur");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Photo", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.Lot", "Lot")
                        .WithMany("Photos")
                        .HasForeignKey("IdLot")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Vendeur", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.Adresse", "Adresse")
                        .WithOne("Vendeur")
                        .HasForeignKey("Gamma2024.Server.Models.Vendeur", "AdresseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Adresse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Gamma2024.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Adresse", b =>
                {
                    b.Navigation("Factures");

                    b.Navigation("Vendeur");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.ApplicationUser", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("CarteCredits");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Categorie", b =>
                {
                    b.Navigation("Lots");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Encan", b =>
                {
                    b.Navigation("EncanLots");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Facture", b =>
                {
                    b.Navigation("Charite");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Lot", b =>
                {
                    b.Navigation("EncanLots");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Medium", b =>
                {
                    b.Navigation("Lots");
                });

            modelBuilder.Entity("Gamma2024.Server.Models.Vendeur", b =>
                {
                    b.Navigation("Lots");
                });
#pragma warning restore 612, 618
        }
    }
}
