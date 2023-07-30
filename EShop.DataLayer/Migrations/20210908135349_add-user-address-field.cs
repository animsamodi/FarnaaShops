using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class adduseraddressfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClientAddress",
                table: "UserAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClientAddress",
                table: "UserAddresses");
        }
    }
}
