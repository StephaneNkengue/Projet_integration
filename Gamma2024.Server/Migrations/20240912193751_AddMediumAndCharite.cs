using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMediumAndCharite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Lots");

            migrationBuilder.AddColumn<int>(
                name: "IdMedium",
                table: "Lots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Charites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontantDon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NomOrganisme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdClient = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charites_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mediums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediums", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Lots_IdMedium",
                table: "Lots",
                column: "IdMedium");

            migrationBuilder.CreateIndex(
                name: "IX_Charites_IdClient",
                table: "Charites",
                column: "IdClient",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Mediums_IdMedium",
                table: "Lots",
                column: "IdMedium",
                principalTable: "Mediums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Mediums_IdMedium",
                table: "Lots");

            migrationBuilder.DropTable(
                name: "Charites");

            migrationBuilder.DropTable(
                name: "Mediums");

            migrationBuilder.DropIndex(
                name: "IX_Lots_IdMedium",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "IdMedium",
                table: "Lots");

            migrationBuilder.AddColumn<string>(
                name: "Medium",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee9c9a98-1f25-425c-9e3b-f0be72f149f6", "AQAAAAIAAYagAAAAEJvViN31+aynskp6sqiuN0tnVeiOIPoMqJJaaj4nWE7nb5DzvHhVjRowwVzdzbhU6w==", "810e7a21-3dd8-4107-804e-d8a8d6b79fc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3b5f1a7-3f40-4003-b633-2990f69a132e", "AQAAAAIAAYagAAAAEBcsxf0ZEBuyeOp0BOAW5Gzj80tGQ51djByPT9e9brNbsjduu9cjap8aYQY4l8XRAg==", "6699cb7b-cdc4-46ed-9e45-0fea6f2eb566" });
        }
    }
}
