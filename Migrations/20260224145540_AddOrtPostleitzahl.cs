using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliothek.Migrations
{
    /// <inheritdoc />
    public partial class AddOrtPostleitzahl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Postleitzahl",
                table: "Orte",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postleitzahl",
                table: "Orte");
        }
    }
}
