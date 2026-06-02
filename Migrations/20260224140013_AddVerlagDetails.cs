using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliothek.Migrations
{
    /// <inheritdoc />
    public partial class AddVerlagDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Bibliotheken_BibliothekId",
                table: "Buecher");

            migrationBuilder.DropTable(
                name: "AutorBuch");

            migrationBuilder.DropTable(
                name: "Bibliotheken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buecher",
                table: "Buecher");

            migrationBuilder.DropIndex(
                name: "IX_Buecher_BibliothekId",
                table: "Buecher");

            migrationBuilder.DropColumn(
                name: "BibliothekId",
                table: "Buecher");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Buecher",
                newName: "AutorId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Verlage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefonnummer",
                table: "Verlage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Buecher",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Buecher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buecher",
                table: "Buecher",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Buecher_AutorId",
                table: "Buecher",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher",
                column: "AutorId",
                principalTable: "Autoren",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buecher",
                table: "Buecher");

            migrationBuilder.DropIndex(
                name: "IX_Buecher_AutorId",
                table: "Buecher");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Verlage");

            migrationBuilder.DropColumn(
                name: "Telefonnummer",
                table: "Verlage");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Buecher",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Buecher",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Buecher",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BibliothekId",
                table: "Buecher",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buecher",
                table: "Buecher",
                column: "Id");

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
    }
}
