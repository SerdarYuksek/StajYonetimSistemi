using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveyAnswers_surveyInfos_SurveyQuestionID",
                table: "surveyAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_surveyInfos",
                table: "surveyInfos");

            migrationBuilder.RenameTable(
                name: "surveyInfos",
                newName: "surveyQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_surveyQuestions",
                table: "surveyQuestions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_SurveyQuestionID",
                table: "surveyAnswers",
                column: "SurveyQuestionID",
                principalTable: "surveyQuestions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveyAnswers_surveyQuestions_SurveyQuestionID",
                table: "surveyAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_surveyQuestions",
                table: "surveyQuestions");

            migrationBuilder.RenameTable(
                name: "surveyQuestions",
                newName: "surveyInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_surveyInfos",
                table: "surveyInfos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_surveyAnswers_surveyInfos_SurveyQuestionID",
                table: "surveyAnswers",
                column: "SurveyQuestionID",
                principalTable: "surveyInfos",
                principalColumn: "ID");
        }
    }
}
