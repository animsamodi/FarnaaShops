using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_img_to_index_layout_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl3",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl4",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideImage",
                table: "IndexLayouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image3",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "ImageUrl3",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "ImageUrl4",
                table: "IndexLayouts");

            migrationBuilder.DropColumn(
                name: "SideImage",
                table: "IndexLayouts");
        }
    }
}
