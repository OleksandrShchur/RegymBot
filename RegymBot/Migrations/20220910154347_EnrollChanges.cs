using Microsoft.EntityFrameworkCore.Migrations;

namespace RegymBot.Migrations
{
    public partial class EnrollChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SelectedClub",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AdminsInfo",
                columns: table => new
                {
                    AdminsInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminApolloLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminVavylonLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPSHKNLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminApolloTelegramId = table.Column<long>(type: "bigint", nullable: false),
                    AdminVavylonTelegramId = table.Column<long>(type: "bigint", nullable: false),
                    AdminPSHKNTelegramId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminsInfo", x => x.AdminsInfoId);
                });

            migrationBuilder.CreateTable(
                name: "AdminsRegistrationLinks",
                columns: table => new
                {
                    AdminsRegistrationLinksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apollo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valylon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pshkn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminsRegistrationLinks", x => x.AdminsRegistrationLinksId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminsInfo");

            migrationBuilder.DropTable(
                name: "AdminsRegistrationLinks");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedClub",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
