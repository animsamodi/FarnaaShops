using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_site_setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowTopImageBannerMobile",
                table: "SiteSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowTopImageBannerWeb",
                table: "SiteSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerMobile",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerMobileTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerMobileUrl",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerWeb",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerWebTitle",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopImageBannerWebUrl",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowTopImageBannerMobile",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "ShowTopImageBannerWeb",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerMobile",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerMobileTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerMobileUrl",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerWeb",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerWebTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TopImageBannerWebUrl",
                table: "SiteSettings");
        }
    }
}
