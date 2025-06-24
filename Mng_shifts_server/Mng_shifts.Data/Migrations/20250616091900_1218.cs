using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mng_shifts.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1218 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SwapProposal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SwapProposal_EmployeeId",
                table: "SwapProposal",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapProposal_Employee_EmployeeId",
                table: "SwapProposal",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapProposal_Employee_EmployeeId",
                table: "SwapProposal");

            migrationBuilder.DropIndex(
                name: "IX_SwapProposal_EmployeeId",
                table: "SwapProposal");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SwapProposal");
        }
    }
}
