using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mng_shifts.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1542 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employee_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequests_Shifts_ShiftId",
                table: "SwapRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SwapRequests",
                table: "SwapRequests");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequests_ShiftId",
                table: "SwapRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts");

            migrationBuilder.RenameTable(
                name: "SwapRequests",
                newName: "SwapRequest");

            migrationBuilder.RenameTable(
                name: "Shifts",
                newName: "Shift");

            migrationBuilder.RenameIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shift",
                newName: "IX_Shift_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "SwapRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SwapRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Shift",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Shift",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SwapRequest",
                table: "SwapRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shift",
                table: "Shift",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SwapProposal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwapRequestId = table.Column<int>(type: "int", nullable: false),
                    ProposedShiftId = table.Column<int>(type: "int", nullable: false),
                    ProposedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwapProposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwapProposal_Shift_ProposedShiftId",
                        column: x => x.ProposedShiftId,
                        principalTable: "Shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SwapProposal_SwapRequest_SwapRequestId",
                        column: x => x.SwapRequestId,
                        principalTable: "SwapRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequest_ShiftId",
                table: "SwapRequest",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_SwapProposal_ProposedShiftId",
                table: "SwapProposal",
                column: "ProposedShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_SwapProposal_SwapRequestId",
                table: "SwapProposal",
                column: "SwapRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequest_Shift_ShiftId",
                table: "SwapRequest",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Employee_EmployeeId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_SwapRequest_Shift_ShiftId",
                table: "SwapRequest");

            migrationBuilder.DropTable(
                name: "SwapProposal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SwapRequest",
                table: "SwapRequest");

            migrationBuilder.DropIndex(
                name: "IX_SwapRequest_ShiftId",
                table: "SwapRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shift",
                table: "Shift");

            migrationBuilder.RenameTable(
                name: "SwapRequest",
                newName: "SwapRequests");

            migrationBuilder.RenameTable(
                name: "Shift",
                newName: "Shifts");

            migrationBuilder.RenameIndex(
                name: "IX_Shift_EmployeeId",
                table: "Shifts",
                newName: "IX_Shifts_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SwapRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SwapRequests",
                table: "SwapRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shifts",
                table: "Shifts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SwapRequests_ShiftId",
                table: "SwapRequests",
                column: "ShiftId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employee_EmployeeId",
                table: "Shifts",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SwapRequests_Shifts_ShiftId",
                table: "SwapRequests",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
