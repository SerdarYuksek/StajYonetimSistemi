using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_QuestionNumber",
                table: "surveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_surveyAnswers_QuestionNumber",
                table: "surveyAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_surveyAnswers_QuestionNumber",
                table: "surveyAnswers",
                column: "QuestionNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_QuestionNumber",
                table: "surveyAnswers",
                column: "QuestionNumber",
                principalTable: "surveyQuestions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
