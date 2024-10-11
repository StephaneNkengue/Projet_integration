using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationRelationsAdressesAvecUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAdresse",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "751f2ac1-b30f-47c9-b881-440d40dc0285", "AQAAAAIAAYagAAAAENZzZr07EhHFJW18W8LMld3BKjENnrZFT6hZ/duHeVwtcI7I8gsbTZkZk9ZhungAkQ==", "54bef98f-d890-4316-9cd8-3a94d8785675" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57582645-8341-48b1-8563-9be6ff17dca2", "AQAAAAIAAYagAAAAEA5MB0ybXVEemuZ9iEhPzxiN++ImhoZ6nbk/vSVsyeKB2sRxfZ1yvv+IGxyohX7AnA==", "2554061c-d698-40fd-8aa3-8160a2b5077f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAdresse",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "IdAdresse", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d3d317e-f262-4244-8e2e-711600ed8a52", 2, "AQAAAAIAAYagAAAAEDjPcgtNAD8u+kwEvEhtX9FIdBQTx4V528jGLEBU153UWOBTfKWxZOMhuzQ+xonApA==", "805ef1d3-6514-4959-9d55-320cfc453e5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "IdAdresse", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64daf2b5-e6a8-47cf-9102-8998ea7e0f44", 1, "AQAAAAIAAYagAAAAELJgtlJoHGAjng74uxhYpEE2sG4HidDt79nk8R2X5yDUVmQF4yWWkp/wnzZbW2spMg==", "9001121d-940b-4d40-a65f-82dff5b94b43" });
        }
    }
}
