using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "surveyInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionOptionsA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionOptionsB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionOptionsC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionOptionsD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionOptionsE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserChoice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveyInfos", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "surveyInfos");
        }
    }
}
