using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_request_hamkar_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoeForush",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShenaseSenfi",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeForush",
                table: "CooperationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShenaseSenfi",
                table: "CooperationRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoeForush",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShenaseSenfi",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NoeForush",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "ShenaseSenfi",
                table: "CooperationRequests");
        }
    }
}
