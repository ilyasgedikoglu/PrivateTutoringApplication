using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PrivateTutoringApplication.Model.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yetki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Adi = table.Column<string>(maxLength: 63, nullable: true),
                    Aciklama = table.Column<string>(maxLength: 127, nullable: true),
                    Goster = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EklenmeZamani = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    Silindi = table.Column<bool>(nullable: false),
                    Ad = table.Column<string>(maxLength: 63, nullable: true),
                    Soyad = table.Column<string>(maxLength: 63, nullable: true),
                    KullaniciAdi = table.Column<string>(maxLength: 127, nullable: true),
                    Sifre = table.Column<string>(maxLength: 63, nullable: true),
                    TuzlamaDegeri = table.Column<string>(maxLength: 63, nullable: true),
                    SifreGuid = table.Column<Guid>(nullable: true),
                    KimlikNo = table.Column<string>(maxLength: 31, nullable: true),
                    Resim = table.Column<string>(maxLength: 255, nullable: true),
                    CinsiyetId = table.Column<int>(nullable: true),
                    DogumYeri = table.Column<string>(maxLength: 63, nullable: true),
                    SifreGuidGecerlilik = table.Column<DateTime>(nullable: true),
                    DogumTarihi = table.Column<DateTime>(nullable: true),
                    EkleyenId = table.Column<int>(nullable: true),
                    YetkiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanici_Kullanici_EkleyenId",
                        column: x => x.EkleyenId,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kullanici_Yetki_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "Yetki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_EkleyenId",
                table: "Kullanici",
                column: "EkleyenId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanici_YetkiId",
                table: "Kullanici",
                column: "YetkiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Yetki");
        }
    }
}
