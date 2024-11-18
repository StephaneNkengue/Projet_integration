using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutAttributPasLotPasMise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pasMise",
                table: "Encans",
                newName: "PasMise");

            migrationBuilder.RenameColumn(
                name: "pasLot",
                table: "Encans",
                newName: "PasLot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasMise",
                table: "Encans",
                newName: "pasMise");

            migrationBuilder.RenameColumn(
                name: "PasLot",
                table: "Encans",
                newName: "pasLot");
        }
    }
}
