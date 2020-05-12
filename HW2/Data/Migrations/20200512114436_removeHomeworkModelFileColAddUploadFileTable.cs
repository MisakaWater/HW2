using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HW2.Data.Migrations
{
    public partial class removeHomeworkModelFileColAddUploadFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_File_AnswerFileId",
                table: "Homeworks");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_AnswerFileId",
                table: "Homeworks");

            migrationBuilder.RenameColumn(
                name: "AnswerFileId",
                table: "Homeworks",
                newName: "AnswerFileID");

            migrationBuilder.AlterColumn<long>(
                name: "AnswerFileID",
                table: "Homeworks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UploadFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadFiles");

            migrationBuilder.RenameColumn(
                name: "AnswerFileID",
                table: "Homeworks",
                newName: "AnswerFileId");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerFileId",
                table: "Homeworks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
