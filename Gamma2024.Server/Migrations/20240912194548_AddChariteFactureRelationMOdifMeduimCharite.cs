using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddChariteFactureRelationMOdifMeduimCharite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Mediums",
                newName: "Type");

            migrationBuilder.AlterColumn<double>(
                name: "MontantDon",
                table: "Charites",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "IdFacture",
                table: "Charites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbf05c21-3f86-42b0-883c-a0d9705c2866", "AQAAAAIAAYagAAAAECKPa0FHlp/FcVRvNtalSx7oOAQ+0h2x9J1XcHmBK8D34g7GnKtZyVDbBTXwrwD1nA==", "f45d9d87-7b90-4e40-8389-b6a85eb2cba4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcbd4561-f017-481c-984d-fe6988de1bbd", "AQAAAAIAAYagAAAAEM4tV/AkUW85lEN+MiZg32qlJzE7bo+Q6mfdWsLhuA4mOWdMVFpliHULjkHBF3LDyg==", "56512c5f-3662-4c99-b3bb-adc692338644" });

            migrationBuilder.CreateIndex(
                name: "IX_Charites_IdFacture",
                table: "Charites",
                column: "IdFacture",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Charites_Factures_IdFacture",
                table: "Charites",
                column: "IdFacture",
                principalTable: "Factures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charites_Factures_IdFacture",
                table: "Charites");

            migrationBuilder.DropIndex(
                name: "IX_Charites_IdFacture",
                table: "Charites");

            migrationBuilder.DropColumn(
                name: "IdFacture",
                table: "Charites");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Mediums",
                newName: "Nom");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontantDon",
                table: "Charites",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcee211f-855b-416d-8abe-9076d53aa17a", "AQAAAAIAAYagAAAAEKnUhnSDoLR5tm1VKzjHPQ5N/0gHqhqtI+pVtEeI7HxQTQd/+KRddwjsCcf0ojACKg==", "22e59540-6545-48c7-9ba4-370debb79db3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "199e4380-554d-46e6-aa3e-1f15577c1d59", "AQAAAAIAAYagAAAAEIJDT++AYsfC81TtZ9gNrLSu8s6luMhnDutFw7C9VC8hzn3ZbK22F4kuB8GstkMYvg==", "93064c28-40c6-4f5c-966a-69d456c69b79" });
        }
    }
}
