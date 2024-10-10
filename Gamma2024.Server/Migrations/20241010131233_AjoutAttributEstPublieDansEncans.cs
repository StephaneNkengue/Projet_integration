using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjoutAttributEstPublieDansEncans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstPublie",
                table: "Encans",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstPublie",
                table: "Encans");

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
    }
}
