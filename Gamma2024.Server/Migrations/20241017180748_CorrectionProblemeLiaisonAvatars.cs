using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionProblemeLiaisonAvatars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatars/default.png", "8e8a5bea-df53-4266-b7c5-7befcf4e6150", "AQAAAAIAAYagAAAAEMtNtBawSqEhUMegDasLJGyHvVkkvu89dD9EhWpfWABKqTYlsD3os2RufgI5h31TjQ==", "cc886c33-e1f9-4aca-8bb6-cb39e7d32926" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatars/default.png", "40f30b0f-4629-487d-93d3-fb178c7c51d2", "AQAAAAIAAYagAAAAEEE7TNb5KmgcgB05sjGNzRd1Tn6+OMOVwFg4uheE36l8WAidr/lYLq7bw1pIgDe1gA==", "2bee4470-ffd3-49c0-be55-7cb5967e1949" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "default.png", "3fb8704b-b359-4179-a835-07c77271dafd", "AQAAAAIAAYagAAAAEJuIZIMsxnRAlmw3hbRXGQFv1Bwf0l+yrZe7N0YY/34Oi5tb6zc3AckYgFh2X8bXXg==", "82a9df8e-26d0-41dc-9b77-5652614da447" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "default.png", "07b638e7-0f4c-4d31-a35a-4d6368342f0a", "AQAAAAIAAYagAAAAEN310TMdyw5nRcIT0KqQQfWEuNQlhWx1ldOWVz8RVofMyTQDXB+i35ZLBwOVAMEs/Q==", "2f990e49-4025-41e6-9ee7-da2d3de1a2e0" });
        }
    }
}
