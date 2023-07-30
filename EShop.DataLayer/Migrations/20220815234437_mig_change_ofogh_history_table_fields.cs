using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_ofogh_history_table_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "OfoghBuyerDatileBuyerType",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "OfoghObjListFactorID",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "OfoghOutputResultCode",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "OfoghServiceServicePrice",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "OfoghUserRoleExtraFieldLicenseNumber",
                table: "OfoghHistories");

            migrationBuilder.RenameColumn(
                name: "statusAppointment",
                table: "OfoghHistories",
                newName: "TypeOfogh");

            migrationBuilder.RenameColumn(
                name: "UserRoleIDstr",
                table: "OfoghHistories",
                newName: "VaziyatHaml");

            migrationBuilder.RenameColumn(
                name: "StuffGroupId",
                table: "OfoghHistories",
                newName: "StatusOfogh");

            migrationBuilder.RenameColumn(
                name: "PersonNationalID",
                table: "OfoghHistories",
                newName: "Tedad");

            migrationBuilder.RenameColumn(
                name: "OfoghUserRoleExtraFieldPostalCode",
                table: "OfoghHistories",
                newName: "CountTry");

            migrationBuilder.RenameColumn(
                name: "OfoghUserRoleExtraFieldDocumentDate",
                table: "OfoghHistories",
                newName: "LastTryDate");

            migrationBuilder.RenameColumn(
                name: "OfoghUserRoleExtraFieldDescription",
                table: "OfoghHistories",
                newName: "TarikhSanad");

            migrationBuilder.RenameColumn(
                name: "OfoghTrackingCodeTrackingCode",
                table: "OfoghHistories",
                newName: "TarikhBarname");

            migrationBuilder.RenameColumn(
                name: "OfoghTrackingCodePrice",
                table: "OfoghHistories",
                newName: "Takhfif");

            migrationBuilder.RenameColumn(
                name: "OfoghServiceDescription",
                table: "OfoghHistories",
                newName: "ShomareSuratHesab");

            migrationBuilder.RenameColumn(
                name: "OfoghOutputResultMessage",
                table: "OfoghHistories",
                newName: "ShomareGharardadBurs");

            migrationBuilder.RenameColumn(
                name: "OfoghObjListRolesList",
                table: "OfoghHistories",
                newName: "ShomareBarname");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatilePostalCode",
                table: "OfoghHistories",
                newName: "ShenaseRahgiri");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileDocNumber",
                table: "OfoghHistories",
                newName: "ShenaseMeli");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileCompanyNationalCode",
                table: "OfoghHistories",
                newName: "ShenaseKala");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileCompanyName",
                table: "OfoghHistories",
                newName: "Sharh");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileBuyerNationalCode",
                table: "OfoghHistories",
                newName: "SerialBarname");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileBuyerName",
                table: "OfoghHistories",
                newName: "ResultMessage");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileBuyerMobile",
                table: "OfoghHistories",
                newName: "NameKharidar");

            migrationBuilder.RenameColumn(
                name: "OfoghBuyerDatileBuyerManagerNationalCode",
                table: "OfoghHistories",
                newName: "Mobile");

            migrationBuilder.AddColumn<string>(
                name: "CodeMessage",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeNaghs",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePostiMabda",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePostiMaghsad",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastTryTime",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mablagh",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maliyat",
                table: "OfoghHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeMessage",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "CodeNaghs",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "CodePostiMabda",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "CodePostiMaghsad",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "LastTryTime",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "Mablagh",
                table: "OfoghHistories");

            migrationBuilder.DropColumn(
                name: "Maliyat",
                table: "OfoghHistories");

            migrationBuilder.RenameColumn(
                name: "VaziyatHaml",
                table: "OfoghHistories",
                newName: "UserRoleIDstr");

            migrationBuilder.RenameColumn(
                name: "TypeOfogh",
                table: "OfoghHistories",
                newName: "statusAppointment");

            migrationBuilder.RenameColumn(
                name: "Tedad",
                table: "OfoghHistories",
                newName: "PersonNationalID");

            migrationBuilder.RenameColumn(
                name: "TarikhSanad",
                table: "OfoghHistories",
                newName: "OfoghUserRoleExtraFieldDescription");

            migrationBuilder.RenameColumn(
                name: "TarikhBarname",
                table: "OfoghHistories",
                newName: "OfoghTrackingCodeTrackingCode");

            migrationBuilder.RenameColumn(
                name: "Takhfif",
                table: "OfoghHistories",
                newName: "OfoghTrackingCodePrice");

            migrationBuilder.RenameColumn(
                name: "StatusOfogh",
                table: "OfoghHistories",
                newName: "StuffGroupId");

            migrationBuilder.RenameColumn(
                name: "ShomareSuratHesab",
                table: "OfoghHistories",
                newName: "OfoghServiceDescription");

            migrationBuilder.RenameColumn(
                name: "ShomareGharardadBurs",
                table: "OfoghHistories",
                newName: "OfoghOutputResultMessage");

            migrationBuilder.RenameColumn(
                name: "ShomareBarname",
                table: "OfoghHistories",
                newName: "OfoghObjListRolesList");

            migrationBuilder.RenameColumn(
                name: "ShenaseRahgiri",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatilePostalCode");

            migrationBuilder.RenameColumn(
                name: "ShenaseMeli",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileDocNumber");

            migrationBuilder.RenameColumn(
                name: "ShenaseKala",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileCompanyNationalCode");

            migrationBuilder.RenameColumn(
                name: "Sharh",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileCompanyName");

            migrationBuilder.RenameColumn(
                name: "SerialBarname",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileBuyerNationalCode");

            migrationBuilder.RenameColumn(
                name: "ResultMessage",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileBuyerName");

            migrationBuilder.RenameColumn(
                name: "NameKharidar",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileBuyerMobile");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "OfoghHistories",
                newName: "OfoghBuyerDatileBuyerManagerNationalCode");

            migrationBuilder.RenameColumn(
                name: "LastTryDate",
                table: "OfoghHistories",
                newName: "OfoghUserRoleExtraFieldDocumentDate");

            migrationBuilder.RenameColumn(
                name: "CountTry",
                table: "OfoghHistories",
                newName: "OfoghUserRoleExtraFieldPostalCode");

            migrationBuilder.AddColumn<int>(
                name: "ActivityType",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfoghBuyerDatileBuyerType",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfoghObjListFactorID",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfoghOutputResultCode",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfoghServiceServicePrice",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfoghUserRoleExtraFieldLicenseNumber",
                table: "OfoghHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
