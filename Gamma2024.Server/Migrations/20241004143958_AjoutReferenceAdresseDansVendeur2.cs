using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutReferenceAdresseDansVendeur2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f85bd7e0-8daf-4cc6-846b-ee64aea3c87e", "AQAAAAIAAYagAAAAEGiHWrmmpgGFT9ku7gH6mWEDiFw68kMEBLrWfFzK2us1v8CzlAUD7xHf9aw+nuQuLA==", "dd0ce23c-1099-4346-8083-9fd5714edd6e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3da7f70-20f5-4716-b3e6-44a5c4d9fee8", "AQAAAAIAAYagAAAAEOx7eFiBtTb7/mmSieAvPXRBSTFVXnE2HAfHFPB8AKNA070nOHGKSbLBABlfgFciHA==", "76bdafd6-f61e-4e5d-9409-c8b7d2a0fa8c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0c34442-825f-40f2-8a5f-41b18950323e", "AQAAAAIAAYagAAAAECcc1UFACbpgxtHqpayXPdodQ2ymx0HefCqyiRPZw+o1lyOICuWAsnaaWYlRAbCGLA==", "e35f6100-aa1b-4474-8504-e8a70bad91a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d238fccf-4cb3-4ad7-9581-d86e5a802fa6", "AQAAAAIAAYagAAAAEAr9ti5goZ0pyuLwm9TcWl2gnNbN2iLmFqOMtubLDsZkLYy9aHD8m1ZmhRtZBK2ONg==", "8aa48818-bcc1-4b01-84bc-495ecc31650c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vendeurs_Adresses_AdresseId",
                table: "Vendeurs",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
