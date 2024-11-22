using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionDeLaTableCartesCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartesCredits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartesCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApplicationUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnneeExpiration = table.Column<int>(type: "int", nullable: false),
                    MoisExpiration = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartesCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartesCredits_AspNetUsers_IdApplicationUser",
                        column: x => x.IdApplicationUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CartesCredits",
                columns: new[] { "Id", "AnneeExpiration", "IdApplicationUser", "MoisExpiration", "Nom", "Numero" },
                values: new object[,]
                {
                    { 1, 2025, "8e445865-a24d-4543-a6c6-9443d048cdb9", 12, "Admin Admin", "5555555555554444" },
                    { 2, 2026, "1d8ac862-e54d-4f10-b6f8-638808c02967", 12, "Jean Dupont", "4242424242424242" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartesCredits_IdApplicationUser",
                table: "CartesCredits",
                column: "IdApplicationUser");
        }
    }
}
