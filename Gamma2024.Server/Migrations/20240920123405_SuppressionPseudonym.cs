using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionPseudonym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Pseudonym",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pseudonym",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c1cec1a-b62e-4a42-b5fc-d86b69aa1e6f", "AQAAAAIAAYagAAAAEGn72PZkfNmlBEWPK8cp0gLPQHMuFIBhZe+BUkq3AhO49pAzTAV255/qGDbe7NDTyQ==", "51ac9011-f5c9-4a8d-848a-b639b59a47b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "585e4cf9-b2e9-4d84-bcfb-faf81774c513", "AQAAAAIAAYagAAAAEMWs2l8Wkym18wP9NBUlsZyttKh9xne0nciCoCkz23DAIwaHVVEUWefjV89jxphJsQ==", "b050bee3-e4fb-4a5f-9ecd-34a794b2f6ac" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pseudonym",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Pseudonym", "SecurityStamp" },
                values: new object[] { "5f25ab3f-142a-4ffe-88ce-a4f95cdf7519", "AQAAAAIAAYagAAAAEBA1XoYBRQSTx1EWbKRWNj2wr9DsnyUOqVRsj67cDu8ONgWgAlR6wY4Cp4i/UAi6cg==", "JeanDu", "c3a2d98f-c7d8-4ac7-83b2-a3015a48a2df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Pseudonym", "SecurityStamp" },
                values: new object[] { "be084a16-de80-460d-81da-8de70dfe2727", "AQAAAAIAAYagAAAAEPS9DoDHnFK3ulIs95BZk6SxRlGiH6Q6GokbOqqfqCZRVMXMFvg+QSUehqmIvo+UMQ==", "SuperAdmin", "f3adb294-061b-43c4-b160-e4f63451ffb1" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Pseudonym",
                table: "AspNetUsers",
                column: "Pseudonym",
                unique: true);
        }
    }
}
