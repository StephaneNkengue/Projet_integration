using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class MiseAutomatiqueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiseAutomatique_AspNetUsers_UserId",
                table: "MiseAutomatique");

            migrationBuilder.DropForeignKey(
                name: "FK_MiseAutomatique_Lots_LotId",
                table: "MiseAutomatique");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiseAutomatique",
                table: "MiseAutomatique");

            migrationBuilder.RenameTable(
                name: "MiseAutomatique",
                newName: "MiseAutomatiques");

            migrationBuilder.RenameColumn(
                name: "EstActive",
                table: "MiseAutomatiques",
                newName: "EstMiseAutomatique");

            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "MiseAutomatiques",
                newName: "DateMise");

            migrationBuilder.RenameIndex(
                name: "IX_MiseAutomatique_UserId",
                table: "MiseAutomatiques",
                newName: "IX_MiseAutomatiques_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MiseAutomatique_LotId",
                table: "MiseAutomatiques",
                newName: "IX_MiseAutomatiques_LotId");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontantMaximal",
                table: "MiseAutomatiques",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Montant",
                table: "MiseAutomatiques",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiseAutomatiques",
                table: "MiseAutomatiques",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MiseAutomatiques_AspNetUsers_UserId",
                table: "MiseAutomatiques",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiseAutomatiques_Lots_LotId",
                table: "MiseAutomatiques",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiseAutomatiques_AspNetUsers_UserId",
                table: "MiseAutomatiques");

            migrationBuilder.DropForeignKey(
                name: "FK_MiseAutomatiques_Lots_LotId",
                table: "MiseAutomatiques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiseAutomatiques",
                table: "MiseAutomatiques");

            migrationBuilder.DropColumn(
                name: "Montant",
                table: "MiseAutomatiques");

            migrationBuilder.RenameTable(
                name: "MiseAutomatiques",
                newName: "MiseAutomatique");

            migrationBuilder.RenameColumn(
                name: "EstMiseAutomatique",
                table: "MiseAutomatique",
                newName: "EstActive");

            migrationBuilder.RenameColumn(
                name: "DateMise",
                table: "MiseAutomatique",
                newName: "DateCreation");

            migrationBuilder.RenameIndex(
                name: "IX_MiseAutomatiques_UserId",
                table: "MiseAutomatique",
                newName: "IX_MiseAutomatique_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MiseAutomatiques_LotId",
                table: "MiseAutomatique",
                newName: "IX_MiseAutomatique_LotId");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontantMaximal",
                table: "MiseAutomatique",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiseAutomatique",
                table: "MiseAutomatique",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MiseAutomatique_AspNetUsers_UserId",
                table: "MiseAutomatique",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiseAutomatique_Lots_LotId",
                table: "MiseAutomatique",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
