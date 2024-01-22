using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "ExamAssignments");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ExamAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "ExamAnswers");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ExamAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
