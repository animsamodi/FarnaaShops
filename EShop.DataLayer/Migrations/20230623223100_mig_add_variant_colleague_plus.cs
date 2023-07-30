using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_variant_colleague_plus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountColleaguePlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GetColleaguePricePlusFromOrginal",
                table: "Variants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxOrderCountColleaguePlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceColleaguePlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountColleaguePlus",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "GetColleaguePricePlusFromOrginal",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "MaxOrderCountColleaguePlus",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "PriceColleaguePlus",
                table: "Variants");
        }
    }
}
