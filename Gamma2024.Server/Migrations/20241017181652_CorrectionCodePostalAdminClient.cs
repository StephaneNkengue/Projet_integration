using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionCodePostalAdminClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodePostal",
                value: "A1A1A1");

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodePostal",
                value: "A1A1A1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodePostal",
                value: "12345");

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodePostal",
                value: "67890");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45d9a58b-babe-4c51-8b4e-e2f5bd5f85cd", "AQAAAAIAAYagAAAAEERcJ+N3RELa5DB9rkppPGmM7lYhTZwOAp1m+3/hjief13ibisWjDozPKSrGcaNbPg==", "2dec435a-49a5-4fdc-a80f-cba3b7980a02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e75e3ac-79f6-47a9-9e4f-9828d3dcb57c", "AQAAAAIAAYagAAAAEOBhU2N7JbajpRKb7E6sRtRJoSWvHZCvMIK4L4AyzrMKKspIWYBFD8/eNAwUn5AokA==", "10f9f217-5ebe-4861-af82-ab5a5b5e2ef6" });
        }
    }
}
