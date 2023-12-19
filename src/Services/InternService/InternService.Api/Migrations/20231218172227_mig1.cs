using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternConfırm",
                table: "internStatuses",
                newName: "InternConfirm");

            migrationBuilder.AddColumn<string>(
                name: "StudentNo",
                table: "internInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentNo",
                table: "internInfos");

            migrationBuilder.RenameColumn(
                name: "InternConfirm",
                table: "internStatuses",
                newName: "InternConfırm");
        }
    }
}
