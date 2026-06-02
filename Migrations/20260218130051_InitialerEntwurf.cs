using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliothek.Migrations
{
    /// <inheritdoc />
    public partial class InitialerEntwurf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verlage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmensitzId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verlage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verlage_Orte_FirmensitzId",
                        column: x => x.FirmensitzId,
                        principalTable: "Orte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buecher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnzahlSeiten = table.Column<int>(type: "int", nullable: false),
                    VerlagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buecher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buecher_Verlage_VerlagId",
                        column: x => x.VerlagId,
                        principalTable: "Verlage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Autoren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jahrgang = table.Column<int>(type: "int", nullable: false),
                    BuchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autoren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autoren_Buecher_BuchId",
                        column: x => x.BuchId,
                        principalTable: "Buecher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autoren_BuchId",
                table: "Autoren",
                column: "BuchId");

            migrationBuilder.CreateIndex(
                name: "IX_Buecher_VerlagId",
                table: "Buecher",
                column: "VerlagId");

            migrationBuilder.CreateIndex(
                name: "IX_Verlage_FirmensitzId",
                table: "Verlage",
                column: "FirmensitzId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autoren");

            migrationBuilder.DropTable(
                name: "Buecher");

            migrationBuilder.DropTable(
                name: "Verlage");

            migrationBuilder.DropTable(
                name: "Orte");
        }
    }
}
