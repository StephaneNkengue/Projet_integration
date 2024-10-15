using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangementDimensionsLotEnDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Largeur",
                table: "Lots",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Hauteur",
                table: "Lots",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e88793c0-3467-4a3f-ac8b-de1ef881aee9", "AQAAAAIAAYagAAAAEB5fAm/WkpZ9zClnYFwxWoU74CrUFxu5dVR1R3ThWfyAFf13uilkFwiRdLVz2fyabQ==", "6d598259-a6f4-402f-898c-f821d3255dcd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f3ec4c3-234f-424a-9f58-d3b84fe4de59", "AQAAAAIAAYagAAAAEPMhHl/FVXtrKajVzU+URxub+qAdFcWftVqOs3yHu2iW8nqR/NiBbZljO82zotvAOg==", "62581ab4-ffa6-4f17-b5c1-64a803c9f420" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Largeur",
                table: "Lots",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Hauteur",
                table: "Lots",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0440083f-0c26-4ab1-89c5-498d33aa1aec", "AQAAAAIAAYagAAAAEMK9xKwPX0o1RdpoHAEBBqxCDP+InIHbc0pf+UDcOS/3GYc1iIA74ey3QKcUVH/qMw==", "0641aa98-f7bf-4f80-b1b8-7f8637bba887" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bc7f1b7-45fc-41b4-a9af-62326f8a6fee", "AQAAAAIAAYagAAAAEM24xFCHW5AQp2xTJkwRi5ApVHqEqVZJv9LKXwv7k81YMJYItqLmOPHIHrS6Vf/JGA==", "d32df31b-8912-4f64-8d4f-22e2f833c2ca" });
        }
    }
}
