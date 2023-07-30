using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_site_menu_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteMenus_SiteMenus_ParentId",
                table: "SiteMenus");

            migrationBuilder.DropIndex(
                name: "IX_SiteMenus_ParentId",
                table: "SiteMenus");

            migrationBuilder.AddColumn<long>(
                name: "Code",
                table: "SiteMenus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SiteMenus_Code",
                table: "SiteMenus",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteMenus_SiteMenus_Code",
                table: "SiteMenus",
                column: "Code",
                principalTable: "SiteMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteMenus_SiteMenus_Code",
                table: "SiteMenus");

            migrationBuilder.DropIndex(
                name: "IX_SiteMenus_Code",
                table: "SiteMenus");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SiteMenus");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMenus_ParentId",
                table: "SiteMenus",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteMenus_SiteMenus_ParentId",
                table: "SiteMenus",
                column: "ParentId",
                principalTable: "SiteMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
