using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutSousTotalDansFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SousTotal",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c21e0f62-cc9c-44a1-b13c-5fec6dd64d74", "AQAAAAIAAYagAAAAEHbujtOJv1SE0AaAehmkA4+fOAbKDg5xa13wmYqepqA07UBBQbsCLVRew/AZZKMNuQ==", "ef76294c-91b7-4d22-b43d-d29a894afaeb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3dbc905-8af3-413f-95db-85f816561c34", "AQAAAAIAAYagAAAAEKcT/QaSTmE/2ffd+JYYaCcdKIieL1DM9NDfCMV0+g2V+qSI0edZtyT4dLOWeWnrBw==", "18b4a82e-c508-4f7c-96ac-3ac38b5c217e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SousTotal",
                table: "Factures");

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
    }
}
