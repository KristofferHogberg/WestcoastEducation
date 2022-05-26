using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.DataMigrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Teachers",
                newName: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Teachers",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Students",
                type: "int",
                nullable: true);
        }
    }
}
