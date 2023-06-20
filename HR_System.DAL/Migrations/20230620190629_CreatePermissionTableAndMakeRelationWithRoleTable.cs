using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissionTableAndMakeRelationWithRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "PermissionsDB",
                columns: table => new
                {
                    ControllerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionNumber = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsDB", x => new { x.RoleId, x.ControllerName });
                    table.ForeignKey(
                        name: "FK_PermissionsDB_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionsDB");

            migrationBuilder.AddColumn<byte>(
                name: "Permissions",
                table: "Roles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
