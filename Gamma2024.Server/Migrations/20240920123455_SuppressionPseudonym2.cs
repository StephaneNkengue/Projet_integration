using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionPseudonym2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bef677cb-4b1c-4648-9bb6-2f5701a0fd03", "AQAAAAIAAYagAAAAEOAYtcleb0RWMY7vl/qT8c9hGF/Ij5U7blZ8CwfTvImnX8nZ+cMu+0QOx7FuS4MaIQ==", "b45de648-7fea-4b7b-a5e8-8d8b1d773558" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dec4e82-f512-4057-a23f-730ccf1c0e3e", "AQAAAAIAAYagAAAAENJX44fIAcX4e5eVM9MRwG+vor0ONoK5wW7YtR6Xdib8nr/joH2SSmH0h7Br7RUPzw==", "d62beb19-85d0-4a31-9bd5-0f29fc945f44" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
