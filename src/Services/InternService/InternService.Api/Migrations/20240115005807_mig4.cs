using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_internInfos_internStatuses_InternStatusId",
                table: "internInfos");

            migrationBuilder.DropIndex(
                name: "IX_internInfos_InternStatusId",
                table: "internInfos");

            migrationBuilder.AddColumn<int>(
                name: "InternStatusId",
                table: "internStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_internStatuses_InternStatusId",
                table: "internStatuses",
                column: "InternStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_internStatuses_internInfos_InternStatusId",
                table: "internStatuses",
                column: "InternStatusId",
                principalTable: "internInfos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_internStatuses_internInfos_InternStatusId",
                table: "internStatuses");

            migrationBuilder.DropIndex(
                name: "IX_internStatuses_InternStatusId",
                table: "internStatuses");

            migrationBuilder.DropColumn(
                name: "InternStatusId",
                table: "internStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_internInfos_InternStatusId",
                table: "internInfos",
                column: "InternStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_internInfos_internStatuses_InternStatusId",
                table: "internInfos",
                column: "InternStatusId",
                principalTable: "internStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
