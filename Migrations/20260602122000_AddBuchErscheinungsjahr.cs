using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Bibliothek.Migrations
{
    [DbContext(typeof(BibliothekContext))]
    [Migration("20260602122000_AddBuchErscheinungsjahr")]
    public partial class AddBuchErscheinungsjahr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Erscheinungsjahr",
                table: "Buecher",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Erscheinungsjahr",
                table: "Buecher");
        }
    }
}
