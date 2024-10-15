using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "aaa60d92-16ef-4854-a3a1-d8330fd7118a", "AQAAAAIAAYagAAAAEDRqPBKRWB43YsBvQMltI4RD+uHRlFSk8OSe4i24U2U41XVktRazzctT4ghf1lkzzw==", null, null, "c7baef91-ff9e-46d6-84c7-b06b2d30d511" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "a3343119-f5d9-4659-a154-b7594dc3dd47", "AQAAAAIAAYagAAAAEAb4KIskcXNp7U42ZV9aabT6YeSJb4CzjDZZz/PBY8TwcKB6oXvTPr8kA+FZEsMXAQ==", null, null, "e16549cb-524e-408d-bfe5-69df677b212c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5c9c497-3d05-48d7-aa80-740f4ec892a6", "AQAAAAIAAYagAAAAEHfZvOiMhUBNpjizkouTbQ+n3zoTqHxExJ1BEp0JKzOUsaTKHRGTjWdKefNGzc0djw==", "da162965-f5de-48f1-9df9-46759022f203" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ce6198a-f07a-4a30-9d7d-0a096c4f4b43", "AQAAAAIAAYagAAAAEI86ApKtk43k+3hL+O7zel6mWO8H6SNG7XCU5nA0k2Qdlgwa5UC1Dc/EtPBtAv7okw==", "7e20a6ad-a59c-4c7b-a068-f6bab2db5a39" });
        }
    }
}
