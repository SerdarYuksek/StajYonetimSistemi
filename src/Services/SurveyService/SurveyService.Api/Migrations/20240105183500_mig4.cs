using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "surveyAnswers",
                newName: "AnswerOption");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_QuestionNumber",
                table: "surveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_surveyAnswers_QuestionNumber",
                table: "surveyAnswers");

            migrationBuilder.RenameColumn(
                name: "AnswerOption",
                table: "surveyAnswers",
                newName: "Answer");
        }
    }
}
