using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_fields_request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmMobile",
                table: "CooperationRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PhoneActiveCode",
                table: "CooperationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneActiveCodeExpDate",
                table: "CooperationRequests",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmMobile",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "PhoneActiveCode",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "PhoneActiveCodeExpDate",
                table: "CooperationRequests");
        }
    }
}
