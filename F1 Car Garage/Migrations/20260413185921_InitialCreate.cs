using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace F1_Car_Garage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Spent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sponsorship = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseSpeed = table.Column<int>(type: "int", nullable: false),
                    BaseHandling = table.Column<int>(type: "int", nullable: false),
                    BaseAcceleration = table.Column<int>(type: "int", nullable: false),
                    BaseManeuverability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "Racers",
                columns: table => new
                {
                    RacerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racers", x => x.RacerId);
                    table.ForeignKey(
                        name: "FK_Racers_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpeedBoost = table.Column<int>(type: "int", nullable: false),
                    HandlingBoost = table.Column<int>(type: "int", nullable: false),
                    AccelBoost = table.Column<int>(type: "int", nullable: false),
                    ManeuverabilityBoost = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarParts",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    InstalledOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParts", x => new { x.CarId, x.PartId });
                    table.ForeignKey(
                        name: "FK_CarParts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "ManufacturerId", "Country", "Name", "Tier" },
                values: new object[,]
                {
                    { 1, "Italy", "Ferrari", "Premium" },
                    { 2, "Italy", "Pirelli", "Premium" },
                    { 3, "Italy", "Brembo", "Mid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_PartId",
                table: "CarParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ManufacturerId",
                table: "Parts",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_BudgetId",
                table: "Racers",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Racers_CarId",
                table: "Racers",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarParts");

            migrationBuilder.DropTable(
                name: "Racers");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
