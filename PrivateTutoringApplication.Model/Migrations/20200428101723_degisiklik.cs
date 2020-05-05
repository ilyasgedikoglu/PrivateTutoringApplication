using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class degisiklik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_TutorSchedule_TutorScheduleId",
                table: "Schedule");

            migrationBuilder.AlterColumn<int>(
                name: "TutorScheduleId",
                table: "Schedule",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_TutorSchedule_TutorScheduleId",
                table: "Schedule",
                column: "TutorScheduleId",
                principalTable: "TutorSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_TutorSchedule_TutorScheduleId",
                table: "Schedule");

            migrationBuilder.AlterColumn<int>(
                name: "TutorScheduleId",
                table: "Schedule",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_TutorSchedule_TutorScheduleId",
                table: "Schedule",
                column: "TutorScheduleId",
                principalTable: "TutorSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
