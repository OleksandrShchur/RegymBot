using Microsoft.EntityFrameworkCore.Migrations;

namespace RegymBot.Migrations
{
    public partial class AddPriceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceType",
                table: "Prices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceType",
                table: "Prices");
        }
    }
}
