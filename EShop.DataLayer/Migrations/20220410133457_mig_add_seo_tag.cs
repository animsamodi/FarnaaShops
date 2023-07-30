using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_seo_tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaKeyWords",
                table: "Products",
                newName: "MetaKeywords");

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "StaticPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "MetaKeywords",
                table: "Products",
                newName: "MetaKeyWords");
        }
    }
}
