using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifiAdressesDeAdminClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "EstDomicile",
                value: true);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "EstDomicile",
                value: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64eaea67-67c3-4433-972f-2f4db68c251d", "AQAAAAIAAYagAAAAEF6E6hrfWWWfJenHs/b2SqQTSE+evniPh/BSrIXOC59hozDhByWsND9JSS/TP6VTMA==", "94d8ea37-7d0b-46bf-a5bc-d3fbce91d9cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b3b9467-072f-4fcd-8eb7-6b7944a7ecbb", "AQAAAAIAAYagAAAAEHWVGXBi/wrqvWJpIZDASQiWlKclYGJmAxGG9Di+2RQ3gsS7Wlok3s5QPdswG3NiWw==", "7c986f7a-516b-4492-842d-df9605ee52ec" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1,
                column: "EstDomicile",
                value: false);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 2,
                column: "EstDomicile",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7767f9df-4589-4df0-8853-31567e53b0c1", "AQAAAAIAAYagAAAAENqkHeM2FV99gywmAXiceeNYbZUlChLMXtebaW+wyPyh71/eboFuJafIsqsEcdp1cA==", "134dc482-7729-4896-9c99-70a309b4a0c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6008cc6c-eded-4243-be6c-1f9eea2484a7", "AQAAAAIAAYagAAAAEJDDBRgpoHI/lGugNO2Sk90YstR25hyimexSTEO38UMlZVpCgS15HpYLR+Cka/rbWw==", "0b55f761-9e50-4632-a6f7-bfb9a0f200b3" });
        }
    }
}
