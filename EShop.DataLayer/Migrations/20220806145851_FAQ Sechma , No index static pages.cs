using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class FAQSechmaNoindexstaticpages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNoIndex",
                table: "StaticPages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FAQSchema",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FAQSchema",
                table: "CategotyMains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FAQSchema",
                table: "CategoryBrandPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FAQSchema",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FAQSchema",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNoIndex",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "FAQSchema",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FAQSchema",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "FAQSchema",
                table: "CategoryBrandPages");

            migrationBuilder.DropColumn(
                name: "FAQSchema",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "FAQSchema",
                table: "Brands");
        }
    }
}
