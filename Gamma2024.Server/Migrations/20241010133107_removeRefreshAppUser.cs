using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class removeRefreshAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "41b73f17-0496-4709-a397-0ef1d747180f", "AQAAAAIAAYagAAAAEFF5oSpMXDUgY5ie97pgX5ly8mBnQ8JAVB6RB7dwQjn9YuJWdB+lvC/N04gd+uAf4w==", "faec5c4c-d75a-4c6d-b0e7-81780fe65cac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af7a8ff8-c487-4a1c-bbb4-d5dbf411750e", "AQAAAAIAAYagAAAAEG+Y3EdOMGw/D8IfAeF4sCsdj0vDXM+hx09tEu+3E1Ha1uaq6HTfnPmjyAF0xcf36g==", "f54003d4-1fe9-47cb-8432-cecedfb57c52" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
