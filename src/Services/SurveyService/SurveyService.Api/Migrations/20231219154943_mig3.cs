using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_SurveyQuestionID",
                table: "surveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_surveyAnswers_SurveyQuestionID",
                table: "surveyAnswers");

            migrationBuilder.DropColumn(
                name: "SurveyQuestionID",
                table: "surveyAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyQuestionID",
                table: "surveyAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_surveyAnswers_SurveyQuestionID",
                table: "surveyAnswers",
                column: "SurveyQuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_SurveyQuestionID",
                table: "surveyAnswers",
                column: "SurveyQuestionID",
                principalTable: "surveyQuestions",
                principalColumn: "ID");
        }
    }
}
