using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentAndAttendenceTablevol2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendence_Employees_EmoloyeeId",
                table: "Attendence");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Attendence_AttendenceId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendence",
                table: "Attendence");

            migrationBuilder.RenameTable(
                name: "Attendence",
                newName: "Attendences");

            migrationBuilder.RenameIndex(
                name: "IX_Attendence_EmoloyeeId",
                table: "Attendences",
                newName: "IX_Attendences_EmoloyeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendences",
                table: "Attendences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Employees_EmoloyeeId",
                table: "Attendences",
                column: "EmoloyeeId",
                principalTable: "Employees",
                principalColumn: "NationalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Attendences_AttendenceId",
                table: "Employees",
                column: "AttendenceId",
                principalTable: "Attendences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Employees_EmoloyeeId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Attendences_AttendenceId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendences",
                table: "Attendences");

            migrationBuilder.RenameTable(
                name: "Attendences",
                newName: "Attendence");

            migrationBuilder.RenameIndex(
                name: "IX_Attendences_EmoloyeeId",
                table: "Attendence",
                newName: "IX_Attendence_EmoloyeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendence",
                table: "Attendence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendence_Employees_EmoloyeeId",
                table: "Attendence",
                column: "EmoloyeeId",
                principalTable: "Employees",
                principalColumn: "NationalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Attendence_AttendenceId",
                table: "Employees",
                column: "AttendenceId",
                principalTable: "Attendence",
                principalColumn: "Id");
        }
    }
}
