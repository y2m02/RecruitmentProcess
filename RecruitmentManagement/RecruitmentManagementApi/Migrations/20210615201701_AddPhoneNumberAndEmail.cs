using Microsoft.EntityFrameworkCore.Migrations;

namespace RecruitmentManagementApi.Migrations
{
    public partial class AddPhoneNumberAndEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Candidates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Candidates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitments_Candidates_CandidateId",
                table: "Recruitments");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentUpdateHistories_Recruitments_RecruitmentId",
                table: "RecruitmentUpdateHistories");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Candidates");

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
    }
}
