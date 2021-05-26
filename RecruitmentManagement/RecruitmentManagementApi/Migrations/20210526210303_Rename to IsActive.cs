using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitmentManagementApi.Migrations
{
    public partial class RenametoIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "AuthorizationKeys",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AuthorizationKeys",
                newName: "IsValid");
        }
    }
}
