using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class change_Blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "PrDate",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeBlog",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TypeBlog",
                table: "Blogs");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
