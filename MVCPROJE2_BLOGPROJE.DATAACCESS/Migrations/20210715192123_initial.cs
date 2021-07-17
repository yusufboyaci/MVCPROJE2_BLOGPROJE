using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCPROJE2_BLOGPROJE.DATAACCESS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Konular",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KonuBasliklari = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konular", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Uyeler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MailAdresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    KullaniciResim = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyeler", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Makaleler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OkunmaSayisi = table.Column<int>(type: "int", nullable: true),
                    MakaleIcerigi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MakaleBasligi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MakaleResim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UyeID = table.Column<int>(type: "int", nullable: false),
                    KonuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makaleler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Makaleler_Uyeler_UyeID",
                        column: x => x.UyeID,
                        principalTable: "Uyeler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UyeKonu",
                columns: table => new
                {
                    UyeID = table.Column<int>(type: "int", nullable: false),
                    KonuID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UyeKonu", x => new { x.UyeID, x.KonuID });
                    table.ForeignKey(
                        name: "FK_UyeKonu_Konular_KonuID",
                        column: x => x.KonuID,
                        principalTable: "Konular",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UyeKonu_Uyeler_UyeID",
                        column: x => x.UyeID,
                        principalTable: "Uyeler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonuMakale",
                columns: table => new
                {
                    KonuID = table.Column<int>(type: "int", nullable: false),
                    MakaleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonuMakale", x => new { x.KonuID, x.MakaleID });
                    table.ForeignKey(
                        name: "FK_KonuMakale_Konular_KonuID",
                        column: x => x.KonuID,
                        principalTable: "Konular",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KonuMakale_Makaleler_MakaleID",
                        column: x => x.MakaleID,
                        principalTable: "Makaleler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KonuMakale_MakaleID",
                table: "KonuMakale",
                column: "MakaleID");

            migrationBuilder.CreateIndex(
                name: "IX_Makaleler_UyeID",
                table: "Makaleler",
                column: "UyeID");

            migrationBuilder.CreateIndex(
                name: "IX_UyeKonu_KonuID",
                table: "UyeKonu",
                column: "KonuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KonuMakale");

            migrationBuilder.DropTable(
                name: "UyeKonu");

            migrationBuilder.DropTable(
                name: "Makaleler");

            migrationBuilder.DropTable(
                name: "Konular");

            migrationBuilder.DropTable(
                name: "Uyeler");
        }
    }
}
