using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class init_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Giris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(maxLength: 511, nullable: true),
                    Durum = table.Column<bool>(nullable: false),
                    IPAddress = table.Column<List<IPAddress>>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Giris_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 63, nullable: true),
                    Category = table.Column<string>(maxLength: 63, nullable: true),
                    Level = table.Column<string>(maxLength: 127, nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Discount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciLesson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciLesson_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciLesson_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonComment_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonComment_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    TutorId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    LessonStartDate = table.Column<DateTime>(nullable: false),
                    LessonDuration = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorSchedule_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutorSchedule_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false),
                    TutorScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_TutorSchedule_TutorScheduleId",
                        column: x => x.TutorScheduleId,
                        principalTable: "TutorSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Giris_KullaniciId",
                table: "Giris",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciLesson_KullaniciId",
                table: "KullaniciLesson",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciLesson_LessonId",
                table: "KullaniciLesson",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonComment_KullaniciId",
                table: "LessonComment",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonComment_LessonId",
                table: "LessonComment",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_KullaniciId",
                table: "Schedule",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TutorScheduleId",
                table: "Schedule",
                column: "TutorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSchedule_KullaniciId",
                table: "TutorSchedule",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSchedule_LessonId",
                table: "TutorSchedule",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Giris");

            migrationBuilder.DropTable(
                name: "KullaniciLesson");

            migrationBuilder.DropTable(
                name: "LessonComment");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "TutorSchedule");

            migrationBuilder.DropTable(
                name: "Lesson");
        }
    }
}
