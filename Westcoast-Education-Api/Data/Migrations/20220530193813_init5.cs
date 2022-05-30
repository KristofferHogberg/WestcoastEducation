using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.Data.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 38, 13, 301, DateTimeKind.Utc).AddTicks(180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 31, 52, 656, DateTimeKind.Utc).AddTicks(670));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 31, 52, 656, DateTimeKind.Utc).AddTicks(670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 38, 13, 301, DateTimeKind.Utc).AddTicks(180));
        }
    }
}
