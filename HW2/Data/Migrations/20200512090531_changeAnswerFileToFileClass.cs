using Microsoft.EntityFrameworkCore.Migrations;

namespace HW2.Data.Migrations
{
    public partial class changeAnswerFileToFileClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerFile",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "AnswerFileId",
                table: "Homeworks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_AnswerFileId",
                table: "Homeworks",
                column: "AnswerFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_File_AnswerFileId",
                table: "Homeworks",
                column: "AnswerFileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_File_AnswerFileId",
                table: "Homeworks");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_AnswerFileId",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "AnswerFileId",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "AnswerFile",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
