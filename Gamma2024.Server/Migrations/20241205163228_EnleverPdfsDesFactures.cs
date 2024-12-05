using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnleverPdfsDesFactures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacturePDFPath",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "FacturePDFPath",
                table: "FactureLivraisons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacturePDFPath",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacturePDFPath",
                table: "FactureLivraisons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
