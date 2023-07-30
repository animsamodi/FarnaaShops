using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class statuscomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "StatusComment",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusComment",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Comments",
                type: "bit",
                maxLength: 100,
                nullable: false,
                defaultValue: false);
        }
    }
}
