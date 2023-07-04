using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentAndAttendenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendenceId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attendence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendenceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmoloyeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendence_Employees_EmoloyeeId",
                        column: x => x.EmoloyeeId,
                        principalTable: "Employees",
                        principalColumn: "NationalID");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AttendenceId",
                table: "Employees",
                column: "AttendenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendence_EmoloyeeId",
                table: "Attendence",
                column: "EmoloyeeId",
                unique: true,
                filter: "[EmoloyeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Attendence_AttendenceId",
                table: "Employees",
                column: "AttendenceId",
                principalTable: "Attendence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Attendence_AttendenceId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Attendence");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AttendenceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AttendenceId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");
        }
    }
}
