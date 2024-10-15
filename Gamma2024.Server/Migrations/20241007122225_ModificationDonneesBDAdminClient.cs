using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDonneesBDAdminClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/wwwroot/Avatars/default.png", "eca274b0-f3e4-494c-acfa-7d444d846ee0", "AQAAAAIAAYagAAAAEIySmwOqJYYMVvh5NqcB19jFlruBtQHCmhQD/lYA0cMA3qK3bAEOLEB6uAxVpy72GA==", "4e45c160-5dda-4fce-8789-d43f2afa3537" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/wwwroot/Avatars/default.png", "e53c54b1-275b-4c1f-b8e6-550127949ce7", "AQAAAAIAAYagAAAAENcoocTsNbfjZuV0eUm4MhOCaEN9N7gBGwMynK2IHl4js2XZY4Bu9mGzuRfeYsQS8w==", "19f8a62a-c30f-4cf8-b2ac-de062b0ff208" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/Avatars/default.png", "f85bd7e0-8daf-4cc6-846b-ee64aea3c87e", "AQAAAAIAAYagAAAAEGiHWrmmpgGFT9ku7gH6mWEDiFw68kMEBLrWfFzK2us1v8CzlAUD7xHf9aw+nuQuLA==", "dd0ce23c-1099-4346-8083-9fd5714edd6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/Avatars/default.png", "c3da7f70-20f5-4716-b3e6-44a5c4d9fee8", "AQAAAAIAAYagAAAAEOx7eFiBtTb7/mmSieAvPXRBSTFVXnE2HAfHFPB8AKNA070nOHGKSbLBABlfgFciHA==", "76bdafd6-f61e-4e5d-9409-c8b7d2a0fa8c" });
        }
    }
}
