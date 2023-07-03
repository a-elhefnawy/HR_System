using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateGeneralSittingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "week_end",
                table: "GeneralSittings",
                newName: "week_end_Day2");

            migrationBuilder.AddColumn<string>(
                name: "week_end_Day1",
                table: "GeneralSittings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "week_end_Day1",
                table: "GeneralSittings");

            migrationBuilder.RenameColumn(
                name: "week_end_Day2",
                table: "GeneralSittings",
                newName: "week_end");
        }
    }
}
