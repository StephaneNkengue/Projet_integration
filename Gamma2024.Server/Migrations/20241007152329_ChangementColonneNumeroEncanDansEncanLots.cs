using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangementColonneNumeroEncanDansEncanLots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncanLots_Encans_NumeroEncan",
                table: "EncanLots");

            migrationBuilder.RenameColumn(
                name: "NumeroEncan",
                table: "EncanLots",
                newName: "IdEncan");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d3d317e-f262-4244-8e2e-711600ed8a52", "AQAAAAIAAYagAAAAEDjPcgtNAD8u+kwEvEhtX9FIdBQTx4V528jGLEBU153UWOBTfKWxZOMhuzQ+xonApA==", "805ef1d3-6514-4959-9d55-320cfc453e5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64daf2b5-e6a8-47cf-9102-8998ea7e0f44", "AQAAAAIAAYagAAAAELJgtlJoHGAjng74uxhYpEE2sG4HidDt79nk8R2X5yDUVmQF4yWWkp/wnzZbW2spMg==", "9001121d-940b-4d40-a65f-82dff5b94b43" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots",
                column: "IdEncan",
                principalTable: "Encans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncanLots_Encans_IdEncan",
                table: "EncanLots");

            migrationBuilder.RenameColumn(
                name: "IdEncan",
                table: "EncanLots",
                newName: "NumeroEncan");

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

            migrationBuilder.AddForeignKey(
                name: "FK_EncanLots_Encans_NumeroEncan",
                table: "EncanLots",
                column: "NumeroEncan",
                principalTable: "Encans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
