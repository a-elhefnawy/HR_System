using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PageNameId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                columns: new[] { "RoleId", "PageNameId" });

            migrationBuilder.CreateTable(
                name: "pagesNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagesNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PageNameId",
                table: "Permissions",
                column: "PageNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_pagesNames_PageNameId",
                table: "Permissions",
                column: "PageNameId",
                principalTable: "pagesNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_pagesNames_PageNameId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "pagesNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PageNameId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PageNameId",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                columns: new[] { "RoleId", "ControllerName" });
        }
    }
}
