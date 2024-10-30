using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class RetraitProprieteVendeurDansClasseAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "AdresseId1",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c235de07-e292-4012-9139-78d70014842d", "AQAAAAIAAYagAAAAEE4DbAVgaFHR1lvBR6erCHgrdGTOLLKkazSyEeww7XKaitGlppz0UWxD1KkmJlgFZw==", "8fe5acd2-90cc-491b-b637-183e87143bf1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e37fecb-9721-4c9e-b711-5aa3dfc56f16", "AQAAAAIAAYagAAAAEMAiD8yt/MCwHVMPrFVRMx61/TX0bQIlcedDH48Jo7z8FSsbg7vh/jwRSQc9aj8gLQ==", "7797d4a5-9191-450e-83a0-ea1b7e09a897" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdresseId1",
                table: "Vendeurs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "306d5a9e-2ac7-4426-a9a3-5e128a93fd4d", "AQAAAAIAAYagAAAAEOUTMd5AWup+radwdzVIWze3PYXKRBacwAp1FeEABGlQeIwjlh1An7yJTKyWny0ggw==", "99c16b7d-7926-4c3b-8937-6e005c789ec7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f169f751-c329-4ebe-afbc-2c82cac5fcb3", "AQAAAAIAAYagAAAAEGqIDYQs9N//7QOOPRK/lA6cKreVmweQRtp87hLTgv5SM+/MhP14LK7Rv1JmUd4Vpg==", "5f168da9-01fd-4134-88b9-88df3618f3e4" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1",
                unique: true,
                filter: "[AdresseId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1",
                principalTable: "Adresses",
                principalColumn: "Id");
        }
    }
}
