using Microsoft.EntityFrameworkCore.Migrations;

namespace coffeterija.dataaccess.Migrations
{
    public partial class AddAreaToCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "OriginCountries",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "OriginCountries");
        }
    }
}
