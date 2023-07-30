using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class addproductField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseSummary",
                table: "PropertyNames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CommodityId",
                table: "Products",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseSummary",
                table: "PropertyNames");

            migrationBuilder.DropColumn(
                name: "CommodityId",
                table: "Products");
        }
    }
}
