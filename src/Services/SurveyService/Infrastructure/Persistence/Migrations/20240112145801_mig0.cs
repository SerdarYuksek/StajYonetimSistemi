using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "surveyAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionNumber = table.Column<int>(type: "integer", nullable: false),
                    StudentNo = table.Column<string>(type: "text", nullable: false),
                    AnswerOption = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveyAnswers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "surveyQuestions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionNumber = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    QuestionOptionsA = table.Column<string>(type: "text", nullable: false),
                    QuestionOptionsB = table.Column<string>(type: "text", nullable: false),
                    QuestionOptionsC = table.Column<string>(type: "text", nullable: false),
                    QuestionOptionsD = table.Column<string>(type: "text", nullable: false),
                    QuestionOptionsE = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveyQuestions", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "surveyAnswers");

            migrationBuilder.DropTable(
                name: "surveyQuestions");
        }
    }
}
