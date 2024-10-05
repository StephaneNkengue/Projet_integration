using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionIdPersonneDansAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPersonne",
                table: "Adresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9639d4b3-af65-400d-abb8-1ef50f587e6f", "AQAAAAIAAYagAAAAEPoxZPDgaqf5BcVaqYekjm2U7VOj67JulHuK2fvBVdDzvN3UWvn9ImRoMXUt2W65QA==", "a8c67dd5-e6f3-435b-8e6a-603c41417298" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0a91c2f-a496-42ad-a8cf-da70b78cd993", "AQAAAAIAAYagAAAAEFJAcmGhRrbGuj6fLFeNOoHmWUOdS8x8/O1rs1nI7jPuLbs7OmOg5+qTQiiprUc5Dw==", "fbeeef30-97b2-4866-b4ef-db93c0a61bbb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPersonne",
                table: "Adresses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "IdPersonne",
                value: null);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdPersonne",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "525f0c10-a921-49e3-b87e-095bd09a4fc4", "AQAAAAIAAYagAAAAEG2OqDRGIUylXSic1+8leTFXA89Ts63Ji82gsDlpNWET9LTrDevT6axA1nDgSmw/Fg==", "c4c938a9-9d42-45cb-aa58-4874b9ee2800" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84d9879a-1c4a-4bf9-9b6f-a3f94b9ce976", "AQAAAAIAAYagAAAAEC6BIf+VBYI+bsEso4hpGTEzD6gR+1VFkM5LlgIbb1AaBVvUlxSjhtfMC8vdNIc8Fg==", "9814bf79-0808-4b1d-b682-b24b21a2efa3" });
        }
    }
}
