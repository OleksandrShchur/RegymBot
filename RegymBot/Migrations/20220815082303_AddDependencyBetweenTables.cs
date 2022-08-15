using Microsoft.EntityFrameworkCore.Migrations;

namespace RegymBot.Migrations
{
    public partial class AddDependencyBetweenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticMessages_Pages_PageId",
                table: "StaticMessages");

            migrationBuilder.DropIndex(
                name: "IX_StaticMessages_PageId",
                table: "StaticMessages");
        }
    }
}
