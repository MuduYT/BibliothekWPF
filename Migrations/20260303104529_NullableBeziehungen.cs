using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bibliothek.Migrations
{
    /// <inheritdoc />
    public partial class NullableBeziehungen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher");

            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Verlage_VerlagId",
                table: "Buecher");

            migrationBuilder.DropForeignKey(
                name: "FK_Verlage_Orte_FirmensitzId",
                table: "Verlage");

            migrationBuilder.AlterColumn<int>(
                name: "FirmensitzId",
                table: "Verlage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VerlagId",
                table: "Buecher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Buecher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher",
                column: "AutorId",
                principalTable: "Autoren",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Verlage_VerlagId",
                table: "Buecher",
                column: "VerlagId",
                principalTable: "Verlage",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Verlage_Orte_FirmensitzId",
                table: "Verlage",
                column: "FirmensitzId",
                principalTable: "Orte",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher");

            migrationBuilder.DropForeignKey(
                name: "FK_Buecher_Verlage_VerlagId",
                table: "Buecher");

            migrationBuilder.DropForeignKey(
                name: "FK_Verlage_Orte_FirmensitzId",
                table: "Verlage");

            migrationBuilder.AlterColumn<int>(
                name: "FirmensitzId",
                table: "Verlage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VerlagId",
                table: "Buecher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AutorId",
                table: "Buecher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Autoren_AutorId",
                table: "Buecher",
                column: "AutorId",
                principalTable: "Autoren",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buecher_Verlage_VerlagId",
                table: "Buecher",
                column: "VerlagId",
                principalTable: "Verlage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verlage_Orte_FirmensitzId",
                table: "Verlage",
                column: "FirmensitzId",
                principalTable: "Orte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
