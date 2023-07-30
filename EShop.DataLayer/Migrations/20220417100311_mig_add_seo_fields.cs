using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_seo_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseSchema",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseSchema",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogBaseSchema",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogCanonical",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogHeaderTag",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogMetaDescription",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogMetaKeywords",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogMetaTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogSchema",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseSchema",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseSchema",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSchema",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "BaseSchema",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogBaseSchema",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogCanonical",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogHeaderTag",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogMetaDescription",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogMetaKeywords",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogMetaTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BlogSchema",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "BaseSchema",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BaseSchema",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "Blogs");
        }
    }
}
