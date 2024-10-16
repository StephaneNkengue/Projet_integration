using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18999dd0-c1ee-4380-9cd0-c559cc031f81", "AQAAAAIAAYagAAAAEFss2pWTfS7oIdww59XIVdt9gNPijIjaDnh97MF3t75EqL86AmoysILJMiA1tKRTqQ==", "3400ad4f-b787-41e6-91eb-fb11e581de15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f51adc97-086e-4dc9-b9fb-708773f8af06", "AQAAAAIAAYagAAAAEHwnfctFRZwvdnZSGH/R0bxoUB9ypTjbrGIre5oqaQOc65dt/LLoGOxX/o9n3n3jhA==", "2c342a40-8273-4d42-9f2a-4b3b0915fe5b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
