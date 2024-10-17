using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class MiseAJourBdPourChangementsLieesAuFactures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charites_AspNetUsers_IdClient",
                table: "Charites");

            migrationBuilder.DropForeignKey(
                name: "FK_Charites_Factures_IdFacture",
                table: "Charites");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Lots_IdLot",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Factures_IdLot",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Charites_IdClient",
                table: "Charites");

            migrationBuilder.DropIndex(
                name: "IX_Charites_IdFacture",
                table: "Charites");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "EstLivree",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "IdLot",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "NumeroFacture",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "Charites");

            migrationBuilder.DropColumn(
                name: "IdFacture",
                table: "Charites");

            migrationBuilder.DropColumn(
                name: "MontantDon",
                table: "Charites");

            migrationBuilder.RenameColumn(
                name: "PrixTotal",
                table: "Factures",
                newName: "Taxes");

            migrationBuilder.AddColumn<int>(
                name: "FactureId",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hauteur",
                table: "Lots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdFacture",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Largeur",
                table: "Lots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SeraLivree",
                table: "Lots",
                type: "bit",
                nullable: true);

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
                name: "FraisEncanteur",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FraisLivraison",
                table: "Factures",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCharite",
                table: "Factures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PrixFinal",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrixLots",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0440083f-0c26-4ab1-89c5-498d33aa1aec", "AQAAAAIAAYagAAAAEMK9xKwPX0o1RdpoHAEBBqxCDP+InIHbc0pf+UDcOS/3GYc1iIA74ey3QKcUVH/qMw==", "0641aa98-f7bf-4f80-b1b8-7f8637bba887" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bc7f1b7-45fc-41b4-a9af-62326f8a6fee", "AQAAAAIAAYagAAAAEM24xFCHW5AQp2xTJkwRi5ApVHqEqVZJv9LKXwv7k81YMJYItqLmOPHIHrS6Vf/JGA==", "d32df31b-8912-4f64-8d4f-22e2f833c2ca" });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_FactureId",
                table: "Lots",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ChariteId",
                table: "Factures",
                column: "ChariteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Charites_ChariteId",
                table: "Factures",
                column: "ChariteId",
                principalTable: "Charites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Factures_FactureId",
                table: "Lots",
                column: "FactureId",
                principalTable: "Factures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Charites_ChariteId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Factures_FactureId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_FactureId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Factures_ChariteId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "FactureId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "Hauteur",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "IdFacture",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "Largeur",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "SeraLivree",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "ChariteId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "Don",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "FraisEncanteur",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "FraisLivraison",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "IdCharite",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "PrixFinal",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "PrixLots",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "Taxes",
                table: "Factures",
                newName: "PrixTotal");

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstLivree",
                table: "Factures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdLot",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroFacture",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdClient",
                table: "Charites",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdFacture",
                table: "Charites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MontantDon",
                table: "Charites",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96d69aaa-baa6-42a4-8bf0-6217c9eae05e", "AQAAAAIAAYagAAAAEJTCCeDk5xbSM/1Xq9iemGJKhxG+IdWr7CwB16LgU1y5nk0hW8kuuEHH8PHuVOkwGg==", "a9c73a70-4e73-45c5-b998-d7394f1b6a44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d281d57b-9e76-41db-bbce-a67b078bbfd4", "AQAAAAIAAYagAAAAEOScfEZop203AdSlzbbdqIgtiPryfGTyaQjsMBaNxoD+XmVt3ZiC4A6qJmtzRppTbw==", "9c3fe615-895d-4416-af4e-60c10da0a3b8" });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IdLot",
                table: "Factures",
                column: "IdLot");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Charites_AspNetUsers_IdClient",
                table: "Charites",
                column: "IdClient",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Charites_Factures_IdFacture",
                table: "Charites",
                column: "IdFacture",
                principalTable: "Factures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Lots_IdLot",
                table: "Factures",
                column: "IdLot",
                principalTable: "Lots",
                principalColumn: "Id");
        }
    }
}
