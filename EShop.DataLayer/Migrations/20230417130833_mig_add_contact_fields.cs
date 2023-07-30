using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_contact_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseSchema",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Canonical",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePosti",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderTag",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSchema",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Canonical",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CodePosti",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "HeaderTag",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Schema",
                table: "Contacts");
        }
    }
}
