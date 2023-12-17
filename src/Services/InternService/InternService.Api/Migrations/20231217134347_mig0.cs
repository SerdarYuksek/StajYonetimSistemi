using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternService.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "internInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    İnternType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    İnternNumber = table.Column<int>(type: "int", nullable: false),
                    Holliday = table.Column<bool>(type: "bit", nullable: false),
                    SaturdayInc = table.Column<bool>(type: "bit", nullable: false),
                    Education = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_internInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "internStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptDay = table.Column<int>(type: "int", nullable: false),
                    InternConfırm = table.Column<bool>(type: "bit", nullable: false),
                    InternAccept = table.Column<bool>(type: "bit", nullable: false),
                    ContributConfirm = table.Column<bool>(type: "bit", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_internStatuses", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "internInfos");

            migrationBuilder.DropTable(
                name: "internStatuses");
        }
    }
}
