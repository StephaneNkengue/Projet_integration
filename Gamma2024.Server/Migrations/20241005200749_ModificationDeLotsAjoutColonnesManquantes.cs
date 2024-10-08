using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDeLotsAjoutColonnesManquantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Lots");

            migrationBuilder.AddColumn<double>(
                name: "PrixMinPourVente",
                table: "Lots",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrixOuverture",
                table: "Lots",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrixMinPourVente",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "PrixOuverture",
                table: "Lots");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0a6d0be-ae5a-48bc-a3fc-cecf0467b92a", "AQAAAAIAAYagAAAAEAOPHHR+3QUUQ4RkNKoz7mqh3BWJ/Ki2g+pzANU6RK4JFBsqHxoGfXmcQ5s1NogEfA==", "7eda711c-2c6f-4a4f-a8a3-d52984107bc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26859cf7-afee-43f7-adde-9cffcc0a3b3d", "AQAAAAIAAYagAAAAEI4j2xdToFUsHufU6uEq6PoRmOh9ab5/YSbEfdvVXVpCtXt/VOA8xTFUvNLpU6Z29Q==", "952b4447-a291-451b-b596-949ca6b8a6a4" });
        }
    }
}
