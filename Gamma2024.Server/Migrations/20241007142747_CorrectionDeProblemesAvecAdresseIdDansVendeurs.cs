using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionDeProblemesAvecAdresseIdDansVendeurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ÏdAdresse",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "821778bd-ef4e-4d8d-a76c-7bc5592c8869", "AQAAAAIAAYagAAAAEDQXrwZDB0iRLYfX40HAo20AT6wC6cB5toZ+eqkeMbhnalHo+HXNDM4UcWE3i1ocNQ==", "2d0edd4f-dd5c-471c-aa61-84f8a33b4154" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0365a08a-2eab-44fc-bca9-ac00faef0bee", "AQAAAAIAAYagAAAAEAeOAPrVEzXLa57aJEk9RxI61Agg26eExGGhRoT15iDRNGObqdW7clGXFi4T6gY1fQ==", "cabeadec-91df-40a7-9677-bee5e26d4da0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ÏdAdresse",
                table: "Vendeurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "252f5c9e-42c2-4d9f-9623-c7c9c91409e5", "AQAAAAIAAYagAAAAEP5pT2c5xbMJ3QEvYyY4MJ8Wb7DvEpwE8RST9FC2j7xYRkv0FdarCL0J1aLqDj7uRg==", "6b9c6c8b-ab6c-4301-947d-54448ff7244b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "001d6fa2-c564-45d9-b0b4-4bce34d27c6c", "AQAAAAIAAYagAAAAEAOs+fjrARsmIjlXbueNnmLaeZt4bURT+P0ceo52xvC2P/tl3Eh7xKjungC3kBazTw==", "97588e6a-eb15-4b1b-a51e-8f7d0adf0915" });
        }
    }
}
