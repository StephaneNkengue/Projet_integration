using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTelephoneDansClientDefaultEtAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "45d9a58b-babe-4c51-8b4e-e2f5bd5f85cd", "AQAAAAIAAYagAAAAEERcJ+N3RELa5DB9rkppPGmM7lYhTZwOAp1m+3/hjief13ibisWjDozPKSrGcaNbPg==", "455-555-5555", "2dec435a-49a5-4fdc-a80f-cba3b7980a02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "8e75e3ac-79f6-47a9-9e4f-9828d3dcb57c", "AQAAAAIAAYagAAAAEOBhU2N7JbajpRKb7E6sRtRJoSWvHZCvMIK4L4AyzrMKKspIWYBFD8/eNAwUn5AokA==", "466-666-6666", "10f9f217-5ebe-4861-af82-ab5a5b5e2ef6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "8e8a5bea-df53-4266-b7c5-7befcf4e6150", "AQAAAAIAAYagAAAAEMtNtBawSqEhUMegDasLJGyHvVkkvu89dD9EhWpfWABKqTYlsD3os2RufgI5h31TjQ==", null, "cc886c33-e1f9-4aca-8bb6-cb39e7d32926" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "40f30b0f-4629-487d-93d3-fb178c7c51d2", "AQAAAAIAAYagAAAAEEE7TNb5KmgcgB05sjGNzRd1Tn6+OMOVwFg4uheE36l8WAidr/lYLq7bw1pIgDe1gA==", null, "2bee4470-ffd3-49c0-be55-7cb5967e1949" });
        }
    }
}
