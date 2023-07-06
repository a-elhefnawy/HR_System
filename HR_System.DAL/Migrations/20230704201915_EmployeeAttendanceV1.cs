using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeAttendanceV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Attendences_AttendenceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AttendenceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Attendences_EmoloyeeId",
                table: "Attendences");

            migrationBuilder.DropColumn(
                name: "AttendenceId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_EmoloyeeId",
                table: "Attendences",
                column: "EmoloyeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendences_EmoloyeeId",
                table: "Attendences");

            migrationBuilder.AddColumn<int>(
                name: "AttendenceId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AttendenceId",
                table: "Employees",
                column: "AttendenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_EmoloyeeId",
                table: "Attendences",
                column: "EmoloyeeId",
                unique: true,
                filter: "[EmoloyeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Attendences_AttendenceId",
                table: "Employees",
                column: "AttendenceId",
                principalTable: "Attendences",
                principalColumn: "Id");
        }
    }
}
