using Microsoft.EntityFrameworkCore.Migrations;

namespace HW2.Data.Migrations
{
    public partial class AddAnswerFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HomeworkId",
                table: "UploadFiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UploadFiles_HomeworkId",
                table: "UploadFiles",
                column: "HomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadFiles_Homeworks_HomeworkId",
                table: "UploadFiles",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadFiles_Homeworks_HomeworkId",
                table: "UploadFiles");

            migrationBuilder.DropIndex(
                name: "IX_UploadFiles_HomeworkId",
                table: "UploadFiles");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "UploadFiles");
        }
    }
}
