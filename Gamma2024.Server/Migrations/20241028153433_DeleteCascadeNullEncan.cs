using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeNullEncan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff176598-423c-4557-bfc4-48e928f579e9", "AQAAAAIAAYagAAAAEBCLhDAVClAVnNnHmZ3ahe6KYsdJa/tTtcmHC64QlZsy07wt7VRMIl+nfrP0UJ8oKw==", "860b65a9-156b-473d-b11b-be6f787cf1e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f3de20-69b9-491d-bc82-b484b44cd47f", "AQAAAAIAAYagAAAAEImrQqIdpN3WKyTx0Ys/9QQXVKT5jTAyfxsPYj6ljA7MwE8U/IWotqFi5RT5o5V7VQ==", "87162d4b-fc3b-4d65-8341-2f4a480982d1" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots",
                column: "IdEncan",
                principalTable: "Encans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "046b33c2-c608-46c2-adf9-89071108fbc2", "AQAAAAIAAYagAAAAEJNs6urvrNfMPgHJcIsuzWAsc78rvihuICnZ179hPwgN6GuvD8pyvvtCovqoLnhWAg==", "0b428bdd-abc9-49de-a5da-7e0191a4b9cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cb39a81-0231-4653-ae41-0700c99c0d81", "AQAAAAIAAYagAAAAEEYzVwFmb9/VLRDypK+8GTTh8Eh6UKEvGV9DFX7S6w8lTz3hXhAQmGJsmbrk6Y3gQg==", "b5772704-5b5b-498c-8127-cb7baa51a979" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots",
                column: "IdEncan",
                principalTable: "Encans",
                principalColumn: "Id");
        }
    }
}
