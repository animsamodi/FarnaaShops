using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class bannerimage_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerImages_Banners_BannerId",
                table: "BannerImages");

            migrationBuilder.DropIndex(
                name: "IX_BannerImages_BannerId",
                table: "BannerImages");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "BannerImages");

            migrationBuilder.AddColumn<int>(
                name: "BannerType",
                table: "BannerImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerType",
                table: "BannerImages");

            migrationBuilder.AddColumn<long>(
                name: "BannerId",
                table: "BannerImages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BannerImages_BannerId",
                table: "BannerImages",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerImages_Banners_BannerId",
                table: "BannerImages",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
