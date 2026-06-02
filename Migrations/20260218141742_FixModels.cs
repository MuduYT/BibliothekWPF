using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliothek.Migrations
{
    /// <inheritdoc />
    public partial class FixModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autoren_Buecher_BuchId",
                table: "Autoren");

            migrationBuilder.DropIndex(
                name: "IX_Autoren_BuchId",
                table: "Autoren");

            migrationBuilder.DropColumn(
                name: "BuchId",
                table: "Autoren");

            migrationBuilder.AddColumn<int>(
                name: "BibliothekId",
                table: "Buecher",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutorBuch",
                columns: table => new
                {
                    AutorenId = table.Column<int>(type: "int", nullable: false),
                    BuecherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorBuch", x => new { x.AutorenId, x.BuecherId });
                    table.ForeignKey(
                        name: "FK_AutorBuch_Autoren_AutorenId",
                        column: x => x.AutorenId,
                        principalTable: "Autoren",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorBuch_Buecher_BuecherId",
                        column: x => x.BuecherId,
                        principalTable: "Buecher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bibliotheken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliotheken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buecher_BibliothekId",
                table: "Buecher",
                column: "BibliothekId");

            migrationBuilder.CreateIndex(
                name: "IX_AutorBuch_BuecherId",
                table: "AutorBuch",
                column: "BuecherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Bibliotheken_BibliothekId",
                table: "Buecher",
                column: "BibliothekId",
                principalTable: "Bibliotheken",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Bibliotheken_BibliothekId",
                table: "Buecher");

            migrationBuilder.DropTable(
                name: "AutorBuch");

            migrationBuilder.DropTable(
                name: "Bibliotheken");

            migrationBuilder.DropIndex(
                name: "IX_Buecher_BibliothekId",
                table: "Buecher");

            migrationBuilder.DropColumn(
                name: "BibliothekId",
                table: "Buecher");

            migrationBuilder.AddColumn<int>(
                name: "BuchId",
                table: "Autoren",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autoren_BuchId",
                table: "Autoren",
                column: "BuchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autoren_Buecher_BuchId",
                table: "Autoren",
                column: "BuchId",
                principalTable: "Buecher",
                principalColumn: "Id");
        }
    }
}
