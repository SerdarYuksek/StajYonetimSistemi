using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserChoice",
                table: "surveyInfos");

            migrationBuilder.CreateTable(
                name: "surveyAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    StudentNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyQuestionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveyAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_surveyAnswers_surveyInfos_SurveyQuestionID",
                        column: x => x.SurveyQuestionID,
                        principalTable: "surveyInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_surveyAnswers_SurveyQuestionID",
                table: "surveyAnswers",
                column: "SurveyQuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "surveyAnswers");

            migrationBuilder.AddColumn<string>(
                name: "UserChoice",
                table: "surveyInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
