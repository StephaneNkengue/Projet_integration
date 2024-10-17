using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifiPathAvatarAdminClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "default.png", "7767f9df-4589-4df0-8853-31567e53b0c1", "AQAAAAIAAYagAAAAENqkHeM2FV99gywmAXiceeNYbZUlChLMXtebaW+wyPyh71/eboFuJafIsqsEcdp1cA==", "134dc482-7729-4896-9c99-70a309b4a0c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "default.png", "6008cc6c-eded-4243-be6c-1f9eea2484a7", "AQAAAAIAAYagAAAAEJDDBRgpoHI/lGugNO2Sk90YstR25hyimexSTEO38UMlZVpCgS15HpYLR+Cka/rbWw==", "0b55f761-9e50-4632-a6f7-bfb9a0f200b3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/Avatars/default.png", "b38d6faa-b1d7-4329-aedd-c310fb67e920", "AQAAAAIAAYagAAAAEEmBFfgmKl/ZEDowmtAE2xwyHqmFLZ6gWFv+UL12vKaNiaKYSrK2He6SSXq0HMKvnA==", "e96dc1e7-dec0-4e64-a0dc-3fbf1f0e5a24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "/Gamma2024.Server/Avatars/default.png", "07600795-01a5-4cdc-ba37-5a604508bda1", "AQAAAAIAAYagAAAAECc6hDUS/roQ5sl58MfB+8Kt0PyOVecrTZGO2B/nsMR5yTvmPAOoLh3qdr+1cJzsqw==", "41d17dac-5622-453b-a1b2-09af345584c3" });
        }
    }
}
