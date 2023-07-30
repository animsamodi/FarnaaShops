using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_cooperation_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shahr",
                table: "CooperationRequests",
                newName: "PrDateCheck");

            migrationBuilder.RenameColumn(
                name: "Ostan",
                table: "CooperationRequests",
                newName: "Description");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "CooperationRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "CooperationRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CooperationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CooperationRequests_CityId",
                table: "CooperationRequests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CooperationRequests_ProvinceId",
                table: "CooperationRequests",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CooperationRequests_Cities_CityId",
                table: "CooperationRequests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CooperationRequests_Provinces_ProvinceId",
                table: "CooperationRequests",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CooperationRequests_Cities_CityId",
                table: "CooperationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CooperationRequests_Provinces_ProvinceId",
                table: "CooperationRequests");

            migrationBuilder.DropIndex(
                name: "IX_CooperationRequests_CityId",
                table: "CooperationRequests");

            migrationBuilder.DropIndex(
                name: "IX_CooperationRequests_ProvinceId",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CooperationRequests");

            migrationBuilder.RenameColumn(
                name: "PrDateCheck",
                table: "CooperationRequests",
                newName: "Shahr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CooperationRequests",
                newName: "Ostan");
        }
    }
}
