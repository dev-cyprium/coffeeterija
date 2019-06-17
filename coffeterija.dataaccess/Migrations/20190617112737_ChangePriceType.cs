using Microsoft.EntityFrameworkCore.Migrations;

namespace coffeterija.dataaccess.Migrations
{
    public partial class ChangePriceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CoffePrices",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "CoffePrices",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
