using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.Data.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 30, 12, 605, DateTimeKind.Utc).AddTicks(2800));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 30, 12, 605, DateTimeKind.Utc).AddTicks(2800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 31, 52, 656, DateTimeKind.Utc).AddTicks(670));
        }
    }
}
