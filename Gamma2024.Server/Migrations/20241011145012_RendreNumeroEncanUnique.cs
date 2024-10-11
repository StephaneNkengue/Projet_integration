using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class RendreNumeroEncanUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96d69aaa-baa6-42a4-8bf0-6217c9eae05e", "AQAAAAIAAYagAAAAEJTCCeDk5xbSM/1Xq9iemGJKhxG+IdWr7CwB16LgU1y5nk0hW8kuuEHH8PHuVOkwGg==", "a9c73a70-4e73-45c5-b998-d7394f1b6a44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d281d57b-9e76-41db-bbce-a67b078bbfd4", "AQAAAAIAAYagAAAAEOScfEZop203AdSlzbbdqIgtiPryfGTyaQjsMBaNxoD+XmVt3ZiC4A6qJmtzRppTbw==", "9c3fe615-895d-4416-af4e-60c10da0a3b8" });

            migrationBuilder.CreateIndex(
                name: "IX_Encans_NumeroEncan",
                table: "Encans",
                column: "NumeroEncan",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Encans_NumeroEncan",
                table: "Encans");

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
        }
    }
}
