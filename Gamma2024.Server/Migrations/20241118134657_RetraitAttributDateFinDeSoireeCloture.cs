using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class RetraitAttributDateFinDeSoireeCloture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFinSoireeCloture",
                table: "Encans");

            migrationBuilder.AddColumn<int>(
                name: "pasLot",
                table: "Encans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pasMise",
                table: "Encans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pasLot",
                table: "Encans");

            migrationBuilder.DropColumn(
                name: "pasMise",
                table: "Encans");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinSoireeCloture",
                table: "Encans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
