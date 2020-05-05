using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class init_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SifreGuid",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "SifreGuidGecerlilik",
                table: "Kullanici");

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVPath",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Kullanici",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GraduationStatus",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Kullanici",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpperDepartment",
                table: "Kullanici",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpperSchool",
                table: "Kullanici",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "CVPath",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "GraduationStatus",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "UpperDepartment",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "UpperSchool",
                table: "Kullanici");

            migrationBuilder.AddColumn<Guid>(
                name: "SifreGuid",
                table: "Kullanici",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SifreGuidGecerlilik",
                table: "Kullanici",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
