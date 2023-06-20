using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsDB_Roles_RoleId",
                table: "PermissionsDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionsDB",
                table: "PermissionsDB");

            migrationBuilder.RenameTable(
                name: "PermissionsDB",
                newName: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                columns: new[] { "RoleId", "ControllerName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Roles_RoleId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "PermissionsDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionsDB",
                table: "PermissionsDB",
                columns: new[] { "RoleId", "ControllerName" });

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsDB_Roles_RoleId",
                table: "PermissionsDB",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
