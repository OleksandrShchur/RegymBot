using Microsoft.EntityFrameworkCore.Migrations;

namespace RegymBot.Migrations
{
    public partial class RemovePageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticMessages_Pages_PageId",
                table: "StaticMessages");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_StaticMessages_PageId",
                table: "StaticMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaticMessages_PageId",
                table: "StaticMessages",
                column: "PageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticMessages_Pages_PageId",
                table: "StaticMessages",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
