using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BayiHesapDetay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EPosta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayiHesapDetay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BayiKategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklamasi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayiKategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BayiRoller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayiRoller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BayiUrunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklamasi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BirimFiyati = table.Column<double>(type: "float", nullable: false),
                    StokMiktari = table.Column<int>(type: "int", nullable: false),
                    SonKullanmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayiUrunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BayiUrunler_BayiKategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "BayiKategoriler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BayiHesaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    HesapDetayiId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BayiHesaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BayiHesaplar_BayiHesapDetay_HesapDetayiId",
                        column: x => x.HesapDetayiId,
                        principalTable: "BayiHesapDetay",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BayiHesaplar_BayiRoller_RolId",
                        column: x => x.RolId,
                        principalTable: "BayiRoller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BayiHesaplar_HesapDetayiId",
                table: "BayiHesaplar",
                column: "HesapDetayiId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BayiHesaplar_RolId",
                table: "BayiHesaplar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_BayiUrunler_KategoriId",
                table: "BayiUrunler",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BayiHesaplar");

            migrationBuilder.DropTable(
                name: "BayiUrunler");

            migrationBuilder.DropTable(
                name: "BayiHesapDetay");

            migrationBuilder.DropTable(
                name: "BayiRoller");

            migrationBuilder.DropTable(
                name: "BayiKategoriler");
        }
    }
}
