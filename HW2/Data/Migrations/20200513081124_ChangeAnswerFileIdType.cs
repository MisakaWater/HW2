using Microsoft.EntityFrameworkCore.Migrations;

namespace HW2.Data.Migrations
{
    public partial class ChangeAnswerFileIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnswerFileID",
                table: "Homeworks",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AnswerFileID",
                table: "Homeworks",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
