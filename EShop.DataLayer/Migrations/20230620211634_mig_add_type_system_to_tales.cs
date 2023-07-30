using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_type_system_to_tales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "UserSearches",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "UserProductViews",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "UserAddresses",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "StaticPages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "SiteSettings",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "SiteFotterMenus",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "SiteFotterLinks",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Redirects",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "PageSeos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "OrderLimits",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "IndexLayouts",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "CategotyMains",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "CategoryBrandPages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "TypeSystem",
                table: "BannerImages",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "UserSearches");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "UserProductViews");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "SiteFotterMenus");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "SiteFotterLinks");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Redirects");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "PageSeos");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "OrderLimits");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "CategotyMains");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "CategoryBrandPages");

            migrationBuilder.DropColumn(
                name: "TypeSystem",
                table: "BannerImages");
        }
    }
}
