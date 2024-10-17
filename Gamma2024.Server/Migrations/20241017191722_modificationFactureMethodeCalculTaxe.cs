using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class modificationFactureMethodeCalculTaxe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdresseId1",
                table: "Vendeurs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "145c32e1-aa5b-4dff-9149-410fd76a3c8b", "AQAAAAIAAYagAAAAEDQFxQUmLBpxWuBuPOIJnIi5tDH1poH7dbtvGYrfVPv5AAvz13p8Pl4bxJFc+7KyUQ==", "a64b0432-6271-447a-a14d-68712ac85836" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b7aaf61-c3ed-415f-a923-1e016981ec82", "AQAAAAIAAYagAAAAEBoEhhOVlFq3G88bIgAOC5Tegj+a/BsTaa5HgQaEq9gAVqVR1ljA0otadS8VdgwZpA==", "67226b4b-0f51-4094-8d2b-0adb27e19ec2" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1",
                unique: true,
                filter: "[AdresseId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs",
                column: "AdresseId1",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropIndex(
                name: "IX_Vendeurs_AdresseId1",
                table: "Vendeurs");

            migrationBuilder.DropColumn(
                name: "AdresseId1",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18999dd0-c1ee-4380-9cd0-c559cc031f81", "AQAAAAIAAYagAAAAEFss2pWTfS7oIdww59XIVdt9gNPijIjaDnh97MF3t75EqL86AmoysILJMiA1tKRTqQ==", "3400ad4f-b787-41e6-91eb-fb11e581de15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f51adc97-086e-4dc9-b9fb-708773f8af06", "AQAAAAIAAYagAAAAEHwnfctFRZwvdnZSGH/R0bxoUB9ypTjbrGIre5oqaQOc65dt/LLoGOxX/o9n3n3jhA==", "2c342a40-8273-4d42-9f2a-4b3b0915fe5b" });
        }
    }
}
