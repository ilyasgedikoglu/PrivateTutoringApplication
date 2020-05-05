using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanici_Kullanici_EkleyenId",
                table: "Kullanici");

            migrationBuilder.DropIndex(
                name: "IX_Kullanici_EkleyenId",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "CinsiyetId",
                table: "Kullanici");

            migrationBuilder.DropColumn(
                name: "EkleyenId",
                table: "Kullanici");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinsiyetId",
                table: "Kullanici",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EkleyenId",
                table: "Kullanici",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_EkleyenId",
                table: "Kullanici",
                column: "EkleyenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanici_Kullanici_EkleyenId",
                table: "Kullanici",
                column: "EkleyenId",
                principalTable: "Kullanici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
