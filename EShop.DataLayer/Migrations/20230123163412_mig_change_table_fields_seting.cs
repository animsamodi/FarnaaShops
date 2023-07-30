using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_table_fields_seting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tilte",
                table: "FilterPrices",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "SearchBg",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchBg",
                table: "SiteSettings");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "FilterPrices",
                newName: "Tilte");
        }
    }
}
