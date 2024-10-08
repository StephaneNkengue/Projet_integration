using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class RendrePrixMinPourVenteNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrixMinPourVente",
                table: "Lots",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c022510a-fbd1-42e8-b8c2-783478fb5589", "AQAAAAIAAYagAAAAEN85BggpGmuwtg03FBSv05AtFdJh8//HLqhodkT8JISlEnUTVWD9madTF/UFQ5/oCQ==", "099ccd4a-bc9f-4b8c-b93d-18645ee732d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68f20caf-b5ab-4e7f-8408-bb4d2fad3dee", "AQAAAAIAAYagAAAAEA3G9Hm0z17lKkxc598VC0sKIYdC5plFPpht2c/7TAFQRGk4BwUQtiLXildD9aMgbA==", "b1f3ce05-2edc-471e-a012-f329ddd2519a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrixMinPourVente",
                table: "Lots",
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
                values: new object[] { "d51a8ed1-851f-47de-84f6-399c9d4eda57", "AQAAAAIAAYagAAAAEJQTuhPOATh0XW7Vl9pspkns/tIxAqAZD4+5h+AOD3bbp9FKT/sdb0AKxBOCkAdh7Q==", "1554c936-6ac6-48de-bcf3-21e26f1d9948" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a49f9f69-8e6f-4f72-8bcb-a194c5518eb8", "AQAAAAIAAYagAAAAEILMkDGK+ylbPMDLbkwnewZNsHYqncf2/XaDsISSP2pSGkuAVcr5Jnj882nqJzFk3g==", "a5a5a8e4-9828-4b5f-b7f2-71aac53f05f2" });
        }
    }
}
