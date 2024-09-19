using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class UniqueUserPseudonym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pseudonym",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f25ab3f-142a-4ffe-88ce-a4f95cdf7519", "AQAAAAIAAYagAAAAEBA1XoYBRQSTx1EWbKRWNj2wr9DsnyUOqVRsj67cDu8ONgWgAlR6wY4Cp4i/UAi6cg==", "c3a2d98f-c7d8-4ac7-83b2-a3015a48a2df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be084a16-de80-460d-81da-8de70dfe2727", "AQAAAAIAAYagAAAAEPS9DoDHnFK3ulIs95BZk6SxRlGiH6Q6GokbOqqfqCZRVMXMFvg+QSUehqmIvo+UMQ==", "f3adb294-061b-43c4-b160-e4f63451ffb1" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Pseudonym",
                table: "AspNetUsers",
                column: "Pseudonym",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Pseudonym",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Pseudonym",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "941ccaf0-4ec2-4b35-a61b-69a79f1255e7", "AQAAAAIAAYagAAAAEEOAH94bBUHxufSbVtmjLV0rhHXRDX/INGsFqJS/DszIGXMiHfgeeTo8iFKw3NlOLg==", "573ee18d-f15f-4bb4-913f-cf176dbe5b09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ef971b7-fbcc-4830-9180-53f7e9209a9e", "AQAAAAIAAYagAAAAEFx3xwTrqUPVBmmU0EGDhHrmtmKj7GlKDiaEQr7oVXXL4L1RnEu93gyiC72/29D9nA==", "45de26ea-9bfb-4acb-95b7-979c9e5f7d70" });
        }
    }
}
