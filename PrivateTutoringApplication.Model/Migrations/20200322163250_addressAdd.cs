using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class addressAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Lesson",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Kullanici",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biyografi",
                table: "Kullanici",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "Biyografi",
                table: "Kullanici");
        }
    }
}
