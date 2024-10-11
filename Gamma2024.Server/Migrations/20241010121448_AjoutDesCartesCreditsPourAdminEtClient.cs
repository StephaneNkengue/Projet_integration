using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutDesCartesCreditsPourAdminEtClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b38d6faa-b1d7-4329-aedd-c310fb67e920", "AQAAAAIAAYagAAAAEEmBFfgmKl/ZEDowmtAE2xwyHqmFLZ6gWFv+UL12vKaNiaKYSrK2He6SSXq0HMKvnA==", "e96dc1e7-dec0-4e64-a0dc-3fbf1f0e5a24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07600795-01a5-4cdc-ba37-5a604508bda1", "AQAAAAIAAYagAAAAECc6hDUS/roQ5sl58MfB+8Kt0PyOVecrTZGO2B/nsMR5yTvmPAOoLh3qdr+1cJzsqw==", "41d17dac-5622-453b-a1b2-09af345584c3" });

            migrationBuilder.InsertData(
                table: "CartesCredits",
                columns: new[] { "Id", "AnneeExpiration", "IdClient", "MoisExpiration", "Nom", "Numero" },
                values: new object[,]
                {
                    { 1, 2025, "8e445865-a24d-4543-a6c6-9443d048cdb9", 12, "Admin Admin", "5555555555554444" },
                    { 2, 2026, "1d8ac862-e54d-4f10-b6f8-638808c02967", 12, "Jean Dupont", "4242424242424242" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CartesCredits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartesCredits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "751f2ac1-b30f-47c9-b881-440d40dc0285", "AQAAAAIAAYagAAAAENZzZr07EhHFJW18W8LMld3BKjENnrZFT6hZ/duHeVwtcI7I8gsbTZkZk9ZhungAkQ==", "54bef98f-d890-4316-9cd8-3a94d8785675" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57582645-8341-48b1-8563-9be6ff17dca2", "AQAAAAIAAYagAAAAEA5MB0ybXVEemuZ9iEhPzxiN++ImhoZ6nbk/vSVsyeKB2sRxfZ1yvv+IGxyohX7AnA==", "2554061c-d698-40fd-8aa3-8160a2b5077f" });
        }
    }
}
