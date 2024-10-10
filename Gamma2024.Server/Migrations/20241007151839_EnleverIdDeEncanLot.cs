using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnleverIdDeEncanLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EncanLots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39feae7f-3500-4bad-b986-71510e6e4227", "AQAAAAIAAYagAAAAEOFn4+/omfOp9VObBIw2GGqqcufnDj4XQNusnMMCfC9HA+u/+rcLaseOjvZyqHKd1Q==", "80a9c3d6-601a-43db-9095-b3715d041cea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba243535-9fbf-4227-b68e-f0a381d1ea5e", "AQAAAAIAAYagAAAAEMuAIySXpcF1lZ/bp4y7yvc2hOglyBWhu0XlpTRSrAsu8KZnnPePVzTXl3XY/OREpQ==", "31081852-8bdc-474f-b075-e6fcd90d4b7d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EncanLots",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
