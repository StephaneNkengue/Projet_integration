using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnleverVendeurIdDeAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "VendeurId",
                table: "Adresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd5b67c9-5ae6-4ad7-a485-e1901b99e93f", "AQAAAAIAAYagAAAAELVAHytQGOZPO18ydtn9gw1UXjfbbQ9WnnI8hn6TPqVmEEOwGZ8dRuDNILAWhp/Org==", "0905a04e-94d9-4090-8fc5-79f17b51a568" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "107a9ac8-e425-425a-9235-f9152f485d03", "AQAAAAIAAYagAAAAEMbqUaQkCH39guOkEufEMl9ADHYeT19r4lAkBt0pHiCe4ETEtmstuYgZma1DFBkkbA==", "2e9b61a8-b289-4f13-9311-1fca4921d03a" });

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

            migrationBuilder.AddColumn<int>(
                name: "AdresseId1",
                table: "Vendeurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendeurId",
                table: "Adresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "VendeurId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "VendeurId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "676aa399-3ba1-4c68-9e93-bb9537313f5a", "AQAAAAIAAYagAAAAEEz7FHzUQf5wf7R7RA0Jb3oXiLKh787rFgV6gaTxCIJWxTyexq2yJxHZR0HWJp+yfQ==", "d0789ac2-3d85-4d37-9f07-3c96a2a2d303" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83ec5aa2-f21e-43f6-a8a7-56ba32852a32", "AQAAAAIAAYagAAAAEDA4mW389h/kigEqjfqMfgzmSDUKkP/dT4EJIhvjRqAI67PxFDqhMIgLYAhBQ5djog==", "6423639c-ed68-4f59-9609-0c5e80dc0581" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
