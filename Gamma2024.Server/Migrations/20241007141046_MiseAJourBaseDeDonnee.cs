using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class MiseAJourBaseDeDonnee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Vendeurs_VendeurId",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_VendeurId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "IdPersonne",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "IdVendeur",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "VendeurId",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "IdAdresse",
                table: "Vendeurs",
                newName: "AdresseId");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Lots",
                newName: "Numero");

            migrationBuilder.AddColumn<double>(
                name: "PrixMinPourVente",
                table: "Lots",
                type: "float",
                nullable: true);

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
                values: new object[] { "62dfb88b-c578-455f-bf44-313cf4ba7027", "AQAAAAIAAYagAAAAEF8RTIX8R+GfZYk7W9qFEzCO6ZzAuMHnpZjGTsLFnCivLTgXUH2NxzejdWSkBro9vw==", "89d92939-6405-4892-bb82-3fce83ca27a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d76ce02-cb35-4d5b-9036-f18c99f01548", "AQAAAAIAAYagAAAAEA5zpsj80BStkgnnukRmifzg9taOZLogfU+ETn7zyWtRVFjoSTChM4im0zPbO5oKbw==", "307d86fb-f5d0-4f9e-a465-a1ae320417a8" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId",
                table: "Vendeurs",
                column: "AdresseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs");

            migrationBuilder.DropIndex(
                name: "IX_Vendeurs_AdresseId",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "PrixMinPourVente",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "PrixOuverture",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "AdresseId",
                table: "Vendeurs",
                newName: "IdAdresse");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Lots",
                newName: "Nom");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdPersonne",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdVendeur",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendeurId",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdPersonne", "IdVendeur", "VendeurId" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdPersonne", "IdVendeur", "VendeurId" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c6d9adf-c4c3-483f-89e7-86695893fd8a", "AQAAAAIAAYagAAAAEH67rKnjcepKwkDDj5IoNvTArwgBmj8+ufhcNjRY+pjDbMV9QM+50bqnPBxcxIsbLA==", "1f0f6b4c-dbda-4b79-834b-992e694252d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "614a067a-c7dd-469b-b952-d159e6cef6d9", "AQAAAAIAAYagAAAAEPcbQPeJp/xtL413i0nfeKc4iAdIo5f632TuhnRwIbqUa2k/hP/fDLDMUGjmvXWY0A==", "6cf99b93-3973-4783-870b-bb1481ffb201" });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_VendeurId",
                table: "Adresses",
                column: "VendeurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Vendeurs_VendeurId",
                table: "Adresses",
                column: "VendeurId",
                principalTable: "Vendeurs",
                principalColumn: "Id");
        }
    }
}
