using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationTableAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Vendeurs_VendeurId",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_VendeurId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "IdVendeur",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "VendeurId",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "AdresseId",
                table: "Vendeurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e06504a2-77aa-498d-8bb5-db36da8e4674", "AQAAAAIAAYagAAAAEBcIpeyhJMrPDbdWSL2d+xncN+4vmhHwRvmtcC53NtX73wMmT+2rWhboaiPg5QnudA==", "fc4d46b1-37b5-4f74-b7f4-12e7d6aa016c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24b7801e-ab42-4128-8e6f-40e5c31ab0c8", "AQAAAAIAAYagAAAAEMrPZD8a/uBhjJp7GDeWn1HPcq/ue6FeRq6pJlLraRPYE7zmNK2tJ0JDW7RUJc25QQ==", "2f124f39-31c9-4134-ab06-2ea151c0a117" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId",
                table: "Vendeurs",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs");

            migrationBuilder.DropIndex(
                name: "IX_Vendeurs_AdresseId",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "AdresseId",
                table: "Vendeurs");

            migrationBuilder.AddColumn<int>(
                name: "IdVendeur",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendeurId",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdVendeur", "VendeurId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdVendeur", "VendeurId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31458349-1aae-4952-b80b-1349df7e3464", "AQAAAAIAAYagAAAAEKGFfBxenw/RV7qPAkgD+LGSufYOJK4o4a7g6m/BZSGkpSCtC7d+4RKCrn/N1/n6WQ==", "ddb519eb-460f-40c3-b18c-33dccae56676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e1f49b9-492a-43fe-8d49-b933c82cec75", "AQAAAAIAAYagAAAAEMAc6sudrjXReWW3cMoh2dGsr7j3Ln7Sylcuss4uS9Q29MXbd/FSt2cTaY2pMPvMnQ==", "81e32f56-3d6f-48be-aeef-665122453be4" });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_VendeurId",
                table: "Adresses",
                column: "VendeurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Vendeurs_VendeurId",
                table: "Adresses",
                column: "VendeurId",
                principalTable: "Vendeurs",
                principalColumn: "Id");
        }
    }
}
