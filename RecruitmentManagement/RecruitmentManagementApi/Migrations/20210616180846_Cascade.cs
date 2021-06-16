using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitmentManagementApi.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories",
                column: "RecruitmentId",
                principalTable: "Recruitments",
                principalColumn: "RecruitmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories",
                column: "RecruitmentId",
                principalTable: "Recruitments",
                principalColumn: "RecruitmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
