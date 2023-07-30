using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_org_img_colomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "MainMenus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage1",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage2",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalImage",
                table: "BannerImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "MainMenus");

            migrationBuilder.DropColumn(
                name: "OrginalImage1",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "OrginalImage2",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "OrginalImage",
                table: "BannerImages");
        }
    }
}
