using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutAuthorisationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31458349-1aae-4952-b80b-1349df7e3464", "AQAAAAIAAYagAAAAEKGFfBxenw/RV7qPAkgD+LGSufYOJK4o4a7g6m/BZSGkpSCtC7d+4RKCrn/N1/n6WQ==", "ddb519eb-460f-40c3-b18c-33dccae56676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e1f49b9-492a-43fe-8d49-b933c82cec75", "AQAAAAIAAYagAAAAEMAc6sudrjXReWW3cMoh2dGsr7j3Ln7Sylcuss4uS9Q29MXbd/FSt2cTaY2pMPvMnQ==", "81e32f56-3d6f-48be-aeef-665122453be4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
