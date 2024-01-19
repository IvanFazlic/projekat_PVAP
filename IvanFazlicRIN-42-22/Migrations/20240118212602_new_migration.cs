using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IvanFazlicRIN_42_22.Migrations
{
    /// <inheritdoc />
    public partial class new_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BojaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikli_Boje_BojaId",
                        column: x => x.BojaId,
                        principalTable: "Boje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtikalKategorije",
                columns: table => new
                {
                    ArtikalId = table.Column<int>(type: "int", nullable: false),
                    KategorijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalKategorije", x => new { x.ArtikalId, x.KategorijaId });
                    table.ForeignKey(
                        name: "FK_ArtikalKategorije_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtikalKategorije_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalId = table.Column<int>(type: "int", nullable: false),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentari_Artikli_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalKategorije_KategorijaId",
                table: "ArtikalKategorije",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_BojaId",
                table: "Artikli",
                column: "BojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_ArtikalId",
                table: "Komentari",
                column: "ArtikalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikalKategorije");

            migrationBuilder.DropTable(
                name: "Komentari");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Boje");
        }
    }
}
