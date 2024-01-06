using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InternStatusId",
                table: "internInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_internInfos_internStatuses_InternStatusId",
                table: "internInfos");

            migrationBuilder.DropIndex(
                name: "IX_internInfos_InternStatusId",
                table: "internInfos");

            migrationBuilder.DropColumn(
                name: "InternStatusId",
                table: "internInfos");
        }
    }
}
