using Microsoft.EntityFrameworkCore.Migrations;

namespace coffeterija.dataaccess.Migrations
{
    public partial class NotNullAdditionsAndContinentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OriginCountries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Continents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OriginCountries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Continents",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
