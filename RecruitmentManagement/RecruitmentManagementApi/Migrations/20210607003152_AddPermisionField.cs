using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitmentManagementApi.Migrations
{
    public partial class AddPermisionField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "AuthorizationKeys",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "AuthorizationKeys");
        }
    }
}
