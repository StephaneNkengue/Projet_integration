using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationAvatarDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Avatars/default.png", "f5c9c497-3d05-48d7-aa80-740f4ec892a6", "AQAAAAIAAYagAAAAEHfZvOiMhUBNpjizkouTbQ+n3zoTqHxExJ1BEp0JKzOUsaTKHRGTjWdKefNGzc0djw==", "da162965-f5de-48f1-9df9-46759022f203" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Avatars/default.png", "9ce6198a-f07a-4a30-9d7d-0a096c4f4b43", "AQAAAAIAAYagAAAAEI86ApKtk43k+3hL+O7zel6mWO8H6SNG7XCU5nA0k2Qdlgwa5UC1Dc/EtPBtAv7okw==", "7e20a6ad-a59c-4c7b-a068-f6bab2db5a39" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
