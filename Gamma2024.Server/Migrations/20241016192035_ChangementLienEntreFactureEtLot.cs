using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangementLienEntreFactureEtLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Factures_FactureId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_FactureId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "FactureId",
                table: "Lots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3b03af7-ecc0-427a-9157-593521860c81", "AQAAAAIAAYagAAAAEPZjOaad6N1FDWUSchl1yHC3znhxlpsBo46xammZeh1KnzA3yBVuOsJzhoc0snLNgw==", "79099654-2e9a-40f3-a077-709d9321faba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcc3e958-8ba8-4bc8-a9bc-f18e9cbddca7", "AQAAAAIAAYagAAAAELl6DM3mqPgpfiEp2BoeGiP1PwpmEQsgv3p7xeqsrdxYHYU9fUw5ciVXPa5UwdhbCg==", "b5557a7e-06bd-4c45-a156-a9a4298d5c46" });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdFacture",
                table: "Lots",
                column: "IdFacture");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Factures_IdFacture",
                table: "Lots",
                column: "IdFacture",
                principalTable: "Factures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Factures_IdFacture",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_IdFacture",
                table: "Lots");

            migrationBuilder.AddColumn<int>(
                name: "FactureId",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e88793c0-3467-4a3f-ac8b-de1ef881aee9", "AQAAAAIAAYagAAAAEB5fAm/WkpZ9zClnYFwxWoU74CrUFxu5dVR1R3ThWfyAFf13uilkFwiRdLVz2fyabQ==", "6d598259-a6f4-402f-898c-f821d3255dcd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f3ec4c3-234f-424a-9f58-d3b84fe4de59", "AQAAAAIAAYagAAAAEPMhHl/FVXtrKajVzU+URxub+qAdFcWftVqOs3yHu2iW8nqR/NiBbZljO82zotvAOg==", "62581ab4-ffa6-4f17-b5c1-64a803c9f420" });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_FactureId",
                table: "Lots",
                column: "FactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Factures_FactureId",
                table: "Lots",
                column: "FactureId",
                principalTable: "Factures",
                principalColumn: "Id");
        }
    }
}
