using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pseudonym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAdresse = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEncan = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDebutSoireeCloture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinSoireeCloture = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mediums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAdresse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendeurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartesCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoisExpiration = table.Column<int>(type: "int", nullable: false),
                    AnneeExpiration = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartesCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartesCredits_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonne = table.Column<int>(type: "int", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Appartement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodePostal = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    EstDomicile = table.Column<bool>(type: "bit", nullable: false),
                    IdApplicationUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdVendeur = table.Column<int>(type: "int", nullable: true),
                    VendeurId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_AspNetUsers_IdApplicationUser",
                        column: x => x.IdApplicationUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Adresses_Vendeurs_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Vendeurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValeurEstimeMin = table.Column<double>(type: "float", nullable: false),
                    ValeurEstimeMax = table.Column<double>(type: "float", nullable: false),
                    DateDepot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Artiste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCategorie = table.Column<int>(type: "int", nullable: false),
                    IdClientMise = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Mise = table.Column<double>(type: "float", nullable: true),
                    EstVendu = table.Column<bool>(type: "bit", nullable: false),
                    DateFinVente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdVendeur = table.Column<int>(type: "int", nullable: false),
                    IdMedium = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_AspNetUsers_IdClientMise",
                        column: x => x.IdClientMise,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lots_Categories_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lots_Mediums_IdMedium",
                        column: x => x.IdMedium,
                        principalTable: "Mediums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lots_Vendeurs_IdVendeur",
                        column: x => x.IdVendeur,
                        principalTable: "Vendeurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EncanLots",
                columns: table => new
                {
                    NumeroEncan = table.Column<int>(type: "int", nullable: false),
                    IdLot = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncanLots", x => new { x.NumeroEncan, x.IdLot });
                    table.ForeignKey(
                        name: "FK_EncanLots_Encans_NumeroEncan",
                        column: x => x.NumeroEncan,
                        principalTable: "Encans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncanLots_Lots_IdLot",
                        column: x => x.IdLot,
                        principalTable: "Lots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroFacture = table.Column<int>(type: "int", nullable: false),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixTotal = table.Column<double>(type: "float", nullable: false),
                    EstLivree = table.Column<bool>(type: "bit", nullable: false),
                    IdLot = table.Column<int>(type: "int", nullable: false),
                    IdAdresse = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Adresses_IdAdresse",
                        column: x => x.IdAdresse,
                        principalTable: "Adresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factures_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factures_Lots_IdLot",
                        column: x => x.IdLot,
                        principalTable: "Lots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Lots_IdLot",
                        column: x => x.IdLot,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontantDon = table.Column<double>(type: "float", nullable: false),
                    NomOrganisme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdClient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdFacture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charites_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charites_Factures_IdFacture",
                        column: x => x.IdFacture,
                        principalTable: "Factures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7da4163f-edb4-47b5-86ea-888888888888", "7da4163f-edb4-47b5-86ea-888888888888", "Client", "CLIENT" },
                    { "7da4163f-edb4-47b5-86ea-999999999999", "7da4163f-edb4-47b5-86ea-999999999999", "Administrateur", "ADMINISTRATEUR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IdAdresse", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Pseudonym", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1d8ac862-e54d-4f10-b6f8-638808c02967", 0, "Avatars/client.jpg", "941ccaf0-4ec2-4b35-a61b-69a79f1255e7", "client@example.com", true, "Jean", 2, false, null, "Dupont", "CLIENT@EXAMPLE.COM", "CLIENT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEOAH94bBUHxufSbVtmjLV0rhHXRDX/INGsFqJS/DszIGXMiHfgeeTo8iFKw3NlOLg==", null, false, "JeanDu", "573ee18d-f15f-4bb4-913f-cf176dbe5b09", false, "client@example.com" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "Avatars/admin.jpg", "4ef971b7-fbcc-4830-9180-53f7e9209a9e", "admin@example.com", true, "Super", 1, false, null, "Admin", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEFx3xwTrqUPVBmmU0EGDhHrmtmKj7GlKDiaEQr7oVXXL4L1RnEu93gyiC72/29D9nA==", null, false, "SuperAdmin", "45de26ea-9bfb-4acb-95b7-979c9e5f7d70", false, "admin@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "Id", "Appartement", "CodePostal", "EstDomicile", "IdApplicationUser", "IdPersonne", "IdVendeur", "Numero", "Pays", "Rue", "VendeurId", "Ville" },
                values: new object[,]
                {
                    { 1, null, "12345", false, "8e445865-a24d-4543-a6c6-9443d048cdb9", null, null, 123, "Pays Admin", "Rue Admin", null, "Ville Admin" },
                    { 2, null, "67890", false, "1d8ac862-e54d-4f10-b6f8-638808c02967", null, null, 456, "Pays Client", "Rue Client", null, "Ville Client" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7da4163f-edb4-47b5-86ea-888888888888", "1d8ac862-e54d-4f10-b6f8-638808c02967" },
                    { "7da4163f-edb4-47b5-86ea-999999999999", "8e445865-a24d-4543-a6c6-9443d048cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_IdApplicationUser",
                table: "Adresses",
                column: "IdApplicationUser");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_VendeurId",
                table: "Adresses",
                column: "VendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartesCredits_IdClient",
                table: "CartesCredits",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Charites_IdClient",
                table: "Charites",
                column: "IdClient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charites_IdFacture",
                table: "Charites",
                column: "IdFacture",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncanLots_IdLot",
                table: "EncanLots",
                column: "IdLot");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdAdresse",
                table: "Factures",
                column: "IdAdresse");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdClient",
                table: "Factures",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdLot",
                table: "Factures",
                column: "IdLot");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdCategorie",
                table: "Lots",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdClientMise",
                table: "Lots",
                column: "IdClientMise");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdMedium",
                table: "Lots",
                column: "IdMedium");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdVendeur",
                table: "Lots",
                column: "IdVendeur");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_IdLot",
                table: "Photos",
                column: "IdLot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartesCredits");

            migrationBuilder.DropTable(
                name: "Charites");

            migrationBuilder.DropTable(
                name: "EncanLots");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Encans");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Mediums");

            migrationBuilder.DropTable(
                name: "Vendeurs");
        }
    }
}
