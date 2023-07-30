using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_site_menu_relation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteMenus_SiteMenus_Code",
                table: "SiteMenus");

            migrationBuilder.DropIndex(
                name: "IX_SiteMenus_Code",
                table: "SiteMenus");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "SiteMenus",
                newName: "ParentCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentCode",
                table: "SiteMenus",
                newName: "ParentId");

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
    }
}
