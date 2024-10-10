using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutReferenceAdresseDansVendeur : Migration
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
                name: "VendeurId",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "IdAdresse",
                table: "Vendeurs",
                newName: "AdresseId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0c34442-825f-40f2-8a5f-41b18950323e", "AQAAAAIAAYagAAAAECcc1UFACbpgxtHqpayXPdodQ2ymx0HefCqyiRPZw+o1lyOICuWAsnaaWYlRAbCGLA==", "e35f6100-aa1b-4474-8504-e8a70bad91a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d238fccf-4cb3-4ad7-9581-d86e5a802fa6", "AQAAAAIAAYagAAAAEAr9ti5goZ0pyuLwm9TcWl2gnNbN2iLmFqOMtubLDsZkLYy9aHD8m1ZmhRtZBK2ONg==", "8aa48818-bcc1-4b01-84bc-495ecc31650c" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId",
                table: "Vendeurs",
                column: "AdresseId",
                unique: true);

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

            migrationBuilder.RenameColumn(
                name: "AdresseId",
                table: "Vendeurs",
                newName: "IdAdresse");

            migrationBuilder.AddColumn<int>(
                name: "VendeurId",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "VendeurId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "VendeurId",
                value: null);

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
