using Microsoft.EntityFrameworkCore.Migrations;

namespace HW2.Data.Migrations
{
    public partial class AddGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerFileID",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "UploadFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "Homeworks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GUID",
                table: "UploadFiles");

            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "AnswerFileID",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
