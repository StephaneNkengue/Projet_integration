using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTableFactureLivraison : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Adresses_IdAdresse",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Charites_ChariteId",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Factures_ChariteId",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Factures_IdAdresse",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "ChariteId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "Don",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "FraisLivraison",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "IdAdresse",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "IdCharite",
                table: "Factures",
                newName: "IdFactureLivraison");

            migrationBuilder.AddColumn<bool>(
                name: "Livrable",
                table: "Factures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FactureLivraisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixFinal = table.Column<double>(type: "float", nullable: false),
                    SousTotal = table.Column<double>(type: "float", nullable: false),
                    TPS = table.Column<double>(type: "float", nullable: false),
                    TVQ = table.Column<double>(type: "float", nullable: false),
                    Don = table.Column<double>(type: "float", nullable: true),
                    IdAdresse = table.Column<int>(type: "int", nullable: false),
                    IdCharite = table.Column<int>(type: "int", nullable: true),
                    IdFacture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureLivraisons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactureLivraisons_Adresses_IdAdresse",
                        column: x => x.IdAdresse,
                        principalTable: "Adresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FactureLivraisons_Charites_IdCharite",
                        column: x => x.IdCharite,
                        principalTable: "Charites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdFactureLivraison",
                table: "Factures",
                column: "IdFactureLivraison",
                unique: true,
                filter: "[IdFactureLivraison] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FactureLivraisons_IdAdresse",
                table: "FactureLivraisons",
                column: "IdAdresse");

            migrationBuilder.CreateIndex(
                name: "IX_FactureLivraisons_IdCharite",
                table: "FactureLivraisons",
                column: "IdCharite");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_FactureLivraisons_IdFactureLivraison",
                table: "Factures",
                column: "IdFactureLivraison",
                principalTable: "FactureLivraisons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_FactureLivraisons_IdFactureLivraison",
                table: "Factures");

            migrationBuilder.DropTable(
                name: "FactureLivraisons");

            migrationBuilder.DropIndex(
                name: "IX_Factures_IdFactureLivraison",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "Livrable",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "IdFactureLivraison",
                table: "Factures",
                newName: "IdCharite");

            migrationBuilder.AddColumn<int>(
                name: "ChariteId",
                table: "Factures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Don",
                table: "Factures",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FraisLivraison",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IdAdresse",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ChariteId",
                table: "Factures",
                column: "ChariteId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdAdresse",
                table: "Factures",
                column: "IdAdresse");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Adresses_IdAdresse",
                table: "Factures",
                column: "IdAdresse",
                principalTable: "Adresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Charites_ChariteId",
                table: "Factures",
                column: "ChariteId",
                principalTable: "Charites",
                principalColumn: "Id");
        }
    }
}
