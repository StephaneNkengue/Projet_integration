using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutDes2TypesDeTaxesDansFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Taxes",
                table: "Factures",
                newName: "TVQ");

            migrationBuilder.AddColumn<double>(
                name: "TPS",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2de158a6-79a0-43a3-9507-c4c745307bff", "AQAAAAIAAYagAAAAEEu/31Wl60LWVg5ssoxhiz5oBZphj4mEFfcZ7Yd+zcnbrH+axURVGL4DuZplQ9jMgA==", "d4685b4f-aa7e-4a51-b8a5-3e98afbd9eb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e3f88e2-6532-40dd-bac4-63256cdbce7b", "AQAAAAIAAYagAAAAECCmi1hIT67a7c5p8JjExJPdauOvFoFmmV1fnGOgtVI+I/VUzngWX4fnn7rdf08i2A==", "d5baab1e-94e3-4079-a0d8-1fca10ead801" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TPS",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "TVQ",
                table: "Factures",
                newName: "Taxes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3b03af7-ecc0-427a-9157-593521860c81", "AQAAAAIAAYagAAAAEPZjOaad6N1FDWUSchl1yHC3znhxlpsBo46xammZeh1KnzA3yBVuOsJzhoc0snLNgw==", "79099654-2e9a-40f3-a077-709d9321faba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcc3e958-8ba8-4bc8-a9bc-f18e9cbddca7", "AQAAAAIAAYagAAAAELl6DM3mqPgpfiEp2BoeGiP1PwpmEQsgv3p7xeqsrdxYHYU9fUw5ciVXPa5UwdhbCg==", "b5557a7e-06bd-4c45-a156-a9a4298d5c46" });
        }
    }
}
