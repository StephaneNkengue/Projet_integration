using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class encanId_nullable_encan_lots : Migration
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
                values: new object[] { "fd5b67c9-5ae6-4ad7-a485-e1901b99e93f", "AQAAAAIAAYagAAAAELVAHytQGOZPO18ydtn9gw1UXjfbbQ9WnnI8hn6TPqVmEEOwGZ8dRuDNILAWhp/Org==", "0905a04e-94d9-4090-8fc5-79f17b51a568" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "107a9ac8-e425-425a-9235-f9152f485d03", "AQAAAAIAAYagAAAAEMbqUaQkCH39guOkEufEMl9ADHYeT19r4lAkBt0pHiCe4ETEtmstuYgZma1DFBkkbA==", "2e9b61a8-b289-4f13-9311-1fca4921d03a" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots",
                column: "IdEncan",
                principalTable: "Encans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
