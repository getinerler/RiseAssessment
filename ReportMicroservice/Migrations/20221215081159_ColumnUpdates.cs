using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportMicroservice.Migrations
{
    public partial class ColumnUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Reports");

            migrationBuilder.AddColumn<bool>(
                name: "ExcelFileReady",
                table: "Reports",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcelFileReady",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Reports",
                type: "text",
                nullable: true);
        }
    }
}
