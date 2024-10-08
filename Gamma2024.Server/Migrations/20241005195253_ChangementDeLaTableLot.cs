using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamma2024.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangementDeLaTableLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Lots",
                newName: "Numero");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8ac862-e54d-4f10-b6f8-638808c02967",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0a6d0be-ae5a-48bc-a3fc-cecf0467b92a", "AQAAAAIAAYagAAAAEAOPHHR+3QUUQ4RkNKoz7mqh3BWJ/Ki2g+pzANU6RK4JFBsqHxoGfXmcQ5s1NogEfA==", "7eda711c-2c6f-4a4f-a8a3-d52984107bc7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26859cf7-afee-43f7-adde-9cffcc0a3b3d", "AQAAAAIAAYagAAAAEI4j2xdToFUsHufU6uEq6PoRmOh9ab5/YSbEfdvVXVpCtXt/VOA8xTFUvNLpU6Z29Q==", "952b4447-a291-451b-b596-949ca6b8a6a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Lots",
                newName: "Code");

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
    }
}
