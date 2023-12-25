using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                table: "personals");

            migrationBuilder.AddColumn<bool>(
                name: "RegistrationCheck",
                table: "students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "RegistrationCheck",
                table: "personals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "personals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationCheck",
                table: "students");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "students");

            migrationBuilder.DropColumn(
                name: "RegistrationCheck",
                table: "personals");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "personals");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmCode",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmCode",
                table: "personals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
