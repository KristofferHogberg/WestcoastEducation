using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 26, 57, 775, DateTimeKind.Utc).AddTicks(2190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 20, 40, 397, DateTimeKind.Utc).AddTicks(7710));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 20, 40, 397, DateTimeKind.Utc).AddTicks(7710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 26, 57, 775, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
