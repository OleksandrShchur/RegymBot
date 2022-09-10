using Microsoft.EntityFrameworkCore.Migrations;

namespace RegymBot.Migrations
{
    public partial class VavylonRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valylon",
                table: "AdminsRegistrationLinks",
                newName: "Vavylon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vavylon",
                table: "AdminsRegistrationLinks",
                newName: "Valylon");
        }
    }
}
