using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmerApp.Migrations
{
    /// <inheritdoc />
    public partial class TargetExpenseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensePurpose",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TargetId",
                table: "Expenses",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Targets_TargetId",
                table: "Expenses",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Targets_TargetId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_TargetId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "ExpensePurpose",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
