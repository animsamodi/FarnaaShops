using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_ofogh_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldUrl",
                table: "Redirects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewUrl",
                table: "Redirects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OfoghHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonNationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRoleIDstr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghUserRoleExtraFieldPostalCode = table.Column<int>(type: "int", nullable: false),
                    OfoghUserRoleExtraFieldLicenseNumber = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    OfoghUserRoleExtraFieldDocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfoghUserRoleExtraFieldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileBuyerType = table.Column<int>(type: "int", nullable: false),
                    OfoghBuyerDatileBuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileBuyerNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileBuyerMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileBuyerManagerNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileCompanyNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatilePostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghBuyerDatileDocNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuffGroupId = table.Column<int>(type: "int", nullable: false),
                    OfoghTrackingCodeTrackingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghTrackingCodePrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghServiceServicePrice = table.Column<int>(type: "int", nullable: false),
                    statusAppointment = table.Column<int>(type: "int", nullable: false),
                    OfoghObjListRolesList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfoghObjListFactorID = table.Column<int>(type: "int", nullable: false),
                    OfoghOutputResultCode = table.Column<int>(type: "int", nullable: false),
                    OfoghOutputResultMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfoghHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfoghHistories");

            migrationBuilder.AlterColumn<string>(
                name: "OldUrl",
                table: "Redirects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NewUrl",
                table: "Redirects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
