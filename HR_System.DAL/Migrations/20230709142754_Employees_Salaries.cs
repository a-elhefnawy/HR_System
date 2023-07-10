using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Employees_Salaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeesSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AttendanceDays = table.Column<int>(type: "int", nullable: false),
                    AbsenceDays = table.Column<int>(type: "int", nullable: false),
                    OvertimePerHours = table.Column<int>(type: "int", nullable: false),
                    DeductionPerHours = table.Column<int>(type: "int", nullable: false),
                    SalaryOverTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryOfMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeesSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesSalaries_EmployeeId",
                table: "EmployeesSalaries",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesSalaries");
        }
    }
}
