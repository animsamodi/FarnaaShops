using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_user_request_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeNaghsh",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileKartMeli",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileParvaneKasb",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileSanad",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeMalekiyat",
                table: "CooperationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeSherkat",
                table: "CooperationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoeTamalok",
                table: "CooperationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneSabet",
                table: "CooperationRequests",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeNaghsh",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileKartMeli",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileParvaneKasb",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileSanad",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NoeMalekiyat",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "NoeSherkat",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "NoeTamalok",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "TelephoneSabet",
                table: "CooperationRequests");
        }
    }
}
