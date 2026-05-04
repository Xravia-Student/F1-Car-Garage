using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1_Car_Garage.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToRacerAndManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Racers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: 3,
                column: "UserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Racers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Manufacturers");
        }
    }
}
