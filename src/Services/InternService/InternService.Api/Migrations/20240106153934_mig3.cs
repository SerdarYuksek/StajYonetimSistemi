using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternConfirm",
                table: "internStatuses",
                newName: "ContributStatus");

            migrationBuilder.RenameColumn(
                name: "InternAccept",
                table: "internStatuses",
                newName: "ConfirmStatus");

            migrationBuilder.RenameColumn(
                name: "ContributConfirm",
                table: "internStatuses",
                newName: "AcceptStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContributStatus",
                table: "internStatuses",
                newName: "InternConfirm");

            migrationBuilder.RenameColumn(
                name: "ConfirmStatus",
                table: "internStatuses",
                newName: "InternAccept");

            migrationBuilder.RenameColumn(
                name: "AcceptStatus",
                table: "internStatuses",
                newName: "ContributConfirm");
        }
    }
}
