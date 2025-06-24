using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mng_shifts.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "SwapProposal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SwapProposal_ShiftId",
                table: "SwapProposal",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_SwapProposal_Shift_ShiftId",
                table: "SwapProposal",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwapProposal_Shift_ShiftId",
                table: "SwapProposal");

            migrationBuilder.DropIndex(
                name: "IX_SwapProposal_ShiftId",
                table: "SwapProposal");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "SwapProposal");
        }
    }
}
