using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class FraisDeLivraisonNonNullableA0DansFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FraisLivraison",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2880ea31-868f-466d-b814-8d01706afa30", "AQAAAAIAAYagAAAAEGCrlFapM8Dm7BxRon0DAe14PjYTVIG5MjtWrSkDffXRSFM6NdElVp+tfC428qPQ+A==", "c7e55aa8-fb4b-48b5-828d-d141af0be1c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad740e9-2d5a-47bd-92fb-cc6986a399e4", "AQAAAAIAAYagAAAAEFggYHsejz0ZORzNqFEqcEqzDY5T/x6CN3LVuk41cpPUhaa609NZmZaC5DJxmflBUQ==", "0afe8125-a353-4a30-8df1-adfdbebafd4e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FraisLivraison",
                table: "Factures",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

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
    }
}
