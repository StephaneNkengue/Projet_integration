using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class MiseAJourDesInfosDansLaBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "692382e1-7f70-4231-8130-21bbee834df8", "AQAAAAIAAYagAAAAENcT565n6ofSgztwnCmOtD2ByBLOR3tiW6skAAyXbPJHSFJVmZxjz2HaC0LLw7zuEw==", "0a9bf163-8a4a-41c6-bed5-655af255c123" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "465fb8d0-bb9e-48e3-9264-032766a3d899", "AQAAAAIAAYagAAAAEBOleOdEG5ZFjgg7iVpOiRP4AWWYRwy2ZwHkXfM4zD1Lo1+P/V8vyVUOYX5FAFx8rg==", "479d734d-7b7f-4740-b06a-2cf3b656fa81" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e06504a2-77aa-498d-8bb5-db36da8e4674", "AQAAAAIAAYagAAAAEBcIpeyhJMrPDbdWSL2d+xncN+4vmhHwRvmtcC53NtX73wMmT+2rWhboaiPg5QnudA==", "fc4d46b1-37b5-4f74-b7f4-12e7d6aa016c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24b7801e-ab42-4128-8e6f-40e5c31ab0c8", "AQAAAAIAAYagAAAAEMrPZD8a/uBhjJp7GDeWn1HPcq/ue6FeRq6pJlLraRPYE7zmNK2tJ0JDW7RUJc25QQ==", "2f124f39-31c9-4134-ab06-2ea151c0a117" });
        }
    }
}
