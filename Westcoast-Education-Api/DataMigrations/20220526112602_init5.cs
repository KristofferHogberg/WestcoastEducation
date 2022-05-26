using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.DataMigrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Teachers");

            migrationBuilder.AddColumn<bool>(
                name: "Isteacher",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isteacher",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Teachers",
                type: "int",
                nullable: true);
        }
    }
}
