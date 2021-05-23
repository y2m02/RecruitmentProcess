using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitmentManagementApi.Migrations
{
    public partial class RemoveCandidateAndChangeReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentUpdateHistories_Candidates_CandidateId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentUpdateHistories_CandidateId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.DropColumn(
                name: "ChangeReason",
                table: "RecruitmentUpdateHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "RecruitmentUpdateHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChangeReason",
                table: "RecruitmentUpdateHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentUpdateHistories_CandidateId",
                table: "RecruitmentUpdateHistories",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentUpdateHistories_Candidates_CandidateId",
                table: "RecruitmentUpdateHistories",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
