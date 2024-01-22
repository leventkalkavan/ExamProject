using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAssignments_AspNetUsers_UserId",
                table: "ExamAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ExamAssignments_UserId",
                table: "ExamAssignments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ExamAssignments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ExamAssignments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "ExamAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssignments_UserId1",
                table: "ExamAssignments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAssignments_AspNetUsers_UserId1",
                table: "ExamAssignments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAssignments_AspNetUsers_UserId1",
                table: "ExamAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ExamAssignments_UserId1",
                table: "ExamAssignments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ExamAssignments");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "ExamAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ExamAssignments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssignments_UserId",
                table: "ExamAssignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAssignments_AspNetUsers_UserId",
                table: "ExamAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
