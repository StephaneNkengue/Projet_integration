using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModificationNomsColonnesDansCarteCredits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartesCredits_AspNetUsers_IdClient",
                table: "CartesCredits");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "CartesCredits",
                newName: "IdApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_CartesCredits_IdClient",
                table: "CartesCredits",
                newName: "IX_CartesCredits_IdApplicationUser");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78f0799f-8cfb-47b5-ad05-3c620bf3c9c5", "AQAAAAIAAYagAAAAEPdoMBMcfieBD1fIHLGcpInphqAycJQKDSfV33Yerh0Vl4sktIMHDA8/6vvvkoZHxQ==", "0dc9975b-e39e-4569-a981-ec4d00ecb2ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "726d5cf3-6857-45f3-a33e-083778c9e3fc", "AQAAAAIAAYagAAAAEHqJTN8ZmvBoz/dSty5IS6dgK4xLE/SCNm2mUiV4Z6ZO/1+LLPtkxwZTKbqyfQmSDQ==", "0f80f515-a761-4e91-ab1c-1b81d5fa6f10" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartesCredits_AspNetUsers_IdApplicationUser",
                table: "CartesCredits",
                column: "IdApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartesCredits_AspNetUsers_IdApplicationUser",
                table: "CartesCredits");

            migrationBuilder.RenameColumn(
                name: "IdApplicationUser",
                table: "CartesCredits",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_CartesCredits_IdApplicationUser",
                table: "CartesCredits",
                newName: "IX_CartesCredits_IdClient");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c76b9f0-e8f3-4847-80a3-94b631baa9d7", "AQAAAAIAAYagAAAAEBXsNqX7jfeUakoqb1rmUSVKkyn5yIhAWueFB144zzzZdhxhsSKvMRBOd6OwLb59Pg==", "63a46889-68aa-4cbc-b45f-0db8e4022dfd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e579cf0-ada5-47b3-8660-9f1ffc9ebbd7", "AQAAAAIAAYagAAAAEGk1DD15g6wPFNIlPGZW8T7RxoaKRqUTnvOXtpXrST69+5jml8oQY6kbripGO5/inQ==", "e93226f2-6de6-4c19-b99e-ed62f4b0b5ba" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartesCredits_AspNetUsers_IdClient",
                table: "CartesCredits",
                column: "IdClient",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
