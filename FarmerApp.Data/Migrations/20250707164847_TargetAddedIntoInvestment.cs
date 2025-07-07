using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmerApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class TargetAddedIntoInvestment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "Investments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investments_TargetId",
                table: "Investments",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_Targets_TargetId",
                table: "Investments",
                column: "TargetId",
                principalTable: "Targets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_Targets_TargetId",
                table: "Investments");

            migrationBuilder.DropIndex(
                name: "IX_Investments_TargetId",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Investments");
        }
    }
}
