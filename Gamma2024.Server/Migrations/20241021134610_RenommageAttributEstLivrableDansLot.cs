using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenommageAttributEstLivrableDansLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estLivrable",
                table: "Lots",
                newName: "EstLivrable");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstLivrable",
                table: "Lots",
                newName: "estLivrable");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dc0fb7c-c865-4987-aedb-28de372c4169", "AQAAAAIAAYagAAAAEFJYKukHbJ436BdwrWE7fdUakwWlNFCTxZJTxXHAPAWlfBdWBQfmdy48XaCf+ITjrQ==", "00e74995-62e6-4754-bc4f-6844f22ab312" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2549fa8c-da55-4c9b-a986-6537de65a0c2", "AQAAAAIAAYagAAAAENGnUB3iPF1BYJ3fmPJ7Ur8K3vj64+QXYHGykVzh3ZuoE5X6Q+rwyG0+47hT0+vpjQ==", "a521e807-337b-4730-b07a-43ad0e6d1e2b" });
        }
    }
}
