using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1_Car_Garage.Migrations
{
    /// <inheritdoc />
    public partial class MakeBudgetNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racers_Budgets_BudgetId",
                table: "Racers");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "Racers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Racers_Budgets_BudgetId",
                table: "Racers",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racers_Budgets_BudgetId",
                table: "Racers");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetId",
                table: "Racers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Racers_Budgets_BudgetId",
                table: "Racers",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
