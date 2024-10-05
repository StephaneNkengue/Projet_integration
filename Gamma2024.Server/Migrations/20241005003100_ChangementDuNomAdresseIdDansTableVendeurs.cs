using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangementDuNomAdresseIdDansTableVendeurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAdresse",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "525f0c10-a921-49e3-b87e-095bd09a4fc4", "AQAAAAIAAYagAAAAEG2OqDRGIUylXSic1+8leTFXA89Ts63Ji82gsDlpNWET9LTrDevT6axA1nDgSmw/Fg==", "c4c938a9-9d42-45cb-aa58-4874b9ee2800" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84d9879a-1c4a-4bf9-9b6f-a3f94b9ce976", "AQAAAAIAAYagAAAAEC6BIf+VBYI+bsEso4hpGTEzD6gR+1VFkM5LlgIbb1AaBVvUlxSjhtfMC8vdNIc8Fg==", "9814bf79-0808-4b1d-b682-b24b21a2efa3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAdresse",
                table: "Vendeurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
