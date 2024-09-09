using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreationClasseBDIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Client_IdCompte",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Client_IdPersonne",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EstBloque",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompte",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPersonne",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonneId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartesCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoisExpiration = table.Column<int>(type: "int", nullable: false),
                    AnneeExpiration = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartesCredit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartesCredit_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomUtilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pseudonyme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotPasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
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
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonne = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Appartement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodePostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstDomicile = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnes_Adresses_AdresseId1",
                        column: x => x.AdresseId1,
                        principalTable: "Adresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vendeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonne = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendeurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendeurs_Personnes_IdPersonne",
                        column: x => x.IdPersonne,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroEncan = table.Column<int>(type: "int", nullable: false),
                    IdCategorie = table.Column<int>(type: "int", nullable: false),
                    IdClientMise = table.Column<int>(type: "int", nullable: true),
                    Mise = table.Column<double>(type: "float", nullable: true),
                    EstVendu = table.Column<bool>(type: "bit", nullable: false),
                    DateFinVente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdVendeur = table.Column<int>(type: "int", nullable: false),
                    ClientMiseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_AspNetUsers_ClientMiseId",
                        column: x => x.ClientMiseId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lots_Categories_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lots_Vendeurs_IdVendeur",
                        column: x => x.IdVendeur,
                        principalTable: "Vendeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncanLots",
                columns: table => new
                {
                    NumeroEncan = table.Column<int>(type: "int", nullable: false),
                    IdLot = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    EncanId = table.Column<int>(type: "int", nullable: true),
                    LotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncanLots", x => new { x.NumeroEncan, x.IdLot });
                    table.ForeignKey(
                        name: "FK_EncanLots_Encans_EncanId",
                        column: x => x.EncanId,
                        principalTable: "Encans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EncanLots_Encans_NumeroEncan",
                        column: x => x.NumeroEncan,
                        principalTable: "Encans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EncanLots_Lots_IdLot",
                        column: x => x.IdLot,
                        principalTable: "Lots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EncanLots_Lots_LotId",
                        column: x => x.LotId,
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
                    IdAdresseFacturation = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    AdresseFacturationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Adresses_AdresseFacturationId",
                        column: x => x.AdresseFacturationId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factures_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Administrateur", "ADMINISTRATEUR" },
                    { "2", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1d8ac862-e54d-4f10-b6f8-638808c02967", 0, "4c886713-03cb-418e-9e77-070506f15bb2", "ApplicationUser", "client@example.com", true, false, null, "CLIENT@EXAMPLE.COM", "CLIENT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJdOZJyc9W2LOY34FHmBQZTAXKmJ7nMaOLh3nZgysq+ya9Yk6GsoQkEv07yzaseBtQ==", null, false, "", false, "client@example.com" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "ed0c8308-5012-4008-9adc-dbef0bf1c92c", "ApplicationUser", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOh89MuBaTCojb969WfHoMEnOKXATSB9ajw9wm1ZvOV3WtLc+MRUSxZrlMv5DOFu5g==", null, false, "", false, "admin@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "1d8ac862-e54d-4f10-b6f8-638808c02967" },
                    { "1", "8e445865-a24d-4543-a6c6-9443d048cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Client_IdCompte",
                table: "AspNetUsers",
                column: "Client_IdCompte",
                unique: true,
                filter: "[IdCompte] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Client_IdPersonne",
                table: "AspNetUsers",
                column: "Client_IdPersonne",
                unique: true,
                filter: "[IdPersonne] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdCompte",
                table: "AspNetUsers",
                column: "IdCompte",
                unique: true,
                filter: "[IdCompte] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdPersonne",
                table: "AspNetUsers",
                column: "IdPersonne",
                unique: true,
                filter: "[IdPersonne] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonneId",
                table: "AspNetUsers",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_IdPersonne",
                table: "Adresses",
                column: "IdPersonne",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartesCredit_ClientId",
                table: "CartesCredit",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EncanLots_EncanId",
                table: "EncanLots",
                column: "EncanId");

            migrationBuilder.CreateIndex(
                name: "IX_EncanLots_IdLot",
                table: "EncanLots",
                column: "IdLot");

            migrationBuilder.CreateIndex(
                name: "IX_EncanLots_LotId",
                table: "EncanLots",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_AdresseFacturationId",
                table: "Factures",
                column: "AdresseFacturationId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_LotId",
                table: "Factures",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ClientMiseId",
                table: "Lots",
                column: "ClientMiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdCategorie",
                table: "Lots",
                column: "IdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdVendeur",
                table: "Lots",
                column: "IdVendeur");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_AdresseId1",
                table: "Personnes",
                column: "AdresseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_IdLot",
                table: "Photos",
                column: "IdLot");

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_IdPersonne",
                table: "Vendeurs",
                column: "IdPersonne",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comptes_Client_IdCompte",
                table: "AspNetUsers",
                column: "Client_IdCompte",
                principalTable: "Comptes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Comptes_IdCompte",
                table: "AspNetUsers",
                column: "IdCompte",
                principalTable: "Comptes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnes_Client_IdPersonne",
                table: "AspNetUsers",
                column: "Client_IdPersonne",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnes_IdPersonne",
                table: "AspNetUsers",
                column: "IdPersonne",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnes_PersonneId",
                table: "AspNetUsers",
                column: "PersonneId",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Personnes_IdPersonne",
                table: "Adresses",
                column: "IdPersonne",
                principalTable: "Personnes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comptes_Client_IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Comptes_IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnes_Client_IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnes_IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnes_PersonneId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Personnes_IdPersonne",
                table: "Adresses");

            migrationBuilder.DropTable(
                name: "CartesCredit");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "EncanLots");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Encans");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Vendeurs");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Client_IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Client_IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonneId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "1d8ac862-e54d-4f10-b6f8-638808c02967" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DropColumn(
                name: "Client_IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Client_IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstBloque",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdCompte",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdPersonne",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonneId",
                table: "AspNetUsers");
        }
    }
}
