using Microsoft.EntityFrameworkCore.Migrations;

namespace Homeworks.DataAccess.Migrations
{
    public partial class AddsAReferenceIDToExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reference",
                table: "Exercises",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reference",
                table: "Exercises");
        }
    }
}
