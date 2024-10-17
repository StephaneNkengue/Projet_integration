using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionDeProblemes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ÏdAdresse",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62dfb88b-c578-455f-bf44-313cf4ba7027", "AQAAAAIAAYagAAAAEF8RTIX8R+GfZYk7W9qFEzCO6ZzAuMHnpZjGTsLFnCivLTgXUH2NxzejdWSkBro9vw==", "89d92939-6405-4892-bb82-3fce83ca27a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d76ce02-cb35-4d5b-9036-f18c99f01548", "AQAAAAIAAYagAAAAEA5zpsj80BStkgnnukRmifzg9taOZLogfU+ETn7zyWtRVFjoSTChM4im0zPbO5oKbw==", "307d86fb-f5d0-4f9e-a465-a1ae320417a8" });
        }
    }
}
