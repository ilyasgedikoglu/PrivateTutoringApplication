using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class degisiklikk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorSchedule_Kullanici_KullaniciId",
                table: "TutorSchedule");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "TutorSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "TutorSchedule",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorSchedule_Kullanici_KullaniciId",
                table: "TutorSchedule",
                column: "KullaniciId",
                principalTable: "Kullanici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorSchedule_Kullanici_KullaniciId",
                table: "TutorSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "TutorSchedule",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TutorId",
                table: "TutorSchedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorSchedule_Kullanici_KullaniciId",
                table: "TutorSchedule",
                column: "KullaniciId",
                principalTable: "Kullanici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
