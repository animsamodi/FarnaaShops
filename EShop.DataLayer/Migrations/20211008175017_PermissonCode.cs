using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class PermissonCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Code",
                table: "Permissons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Permissons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Permissons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Permissons");
        }
    }
}
