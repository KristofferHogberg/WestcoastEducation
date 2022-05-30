using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Westcoast_Education_Api.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollmentDate",
                table: "CourseStudents",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 30, 19, 30, 12, 605, DateTimeKind.Utc).AddTicks(2800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 26, 57, 775, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
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
                oldDefaultValue: new DateTime(2022, 5, 30, 19, 30, 12, 605, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
