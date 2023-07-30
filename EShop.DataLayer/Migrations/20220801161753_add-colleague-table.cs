using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class addcolleaguetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountColleague",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxOrderCountColleague",
                table: "Variants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceColleague",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "Family",
            //    table: "Users",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "Users",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Family",
            //    table: "UserAddresses",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "UserAddresses",
            //    type: "nvarchar(100)",
            //    maxLength: 100,
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "TaxCode",
            //    table: "Provinces",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.AlterColumn<double>(
            //    name: "ZemanatBankiMablagh",
            //    table: "Credits",
            //    type: "float",
            //    nullable: true,
            //    oldClrType: typeof(double),
            //    oldType: "float");

            //migrationBuilder.AlterColumn<double>(
            //    name: "VasigheMelkiMablagh",
            //    table: "Credits",
            //    type: "float",
            //    nullable: true,
            //    oldClrType: typeof(double),
            //    oldType: "float");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreditExpDate",
            //    table: "Credits",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "TrakingCode",
            //    table: "Credits",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Darsad",
            //    table: "CreditPartners",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,2)");

            //migrationBuilder.AlterColumn<int>(
            //    name: "TypeFile",
            //    table: "CreditDocumentTypes",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "CreditDocumentTypes",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsRequired",
            //    table: "CreditDocumentTypes",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "Text",
            //    table: "CreditDocumentTypes",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Title",
            //    table: "CreditDocuments",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "TaxCode",
            //    table: "Cities",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Credit",
            //    table: "Carts",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "CooperationRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeMeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeNaghsh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ostan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shahr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePosti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tozihat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShomareTamas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileKartMeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileParvaneKasb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSanad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperationRequests", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "CreditBills",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreditId = table.Column<long>(type: "bigint", nullable: true),
            //        UserId = table.Column<long>(type: "bigint", nullable: false),
            //        Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Shobe = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShomareHesab = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShomareKart = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Price = table.Column<int>(type: "int", nullable: false),
            //        ConfirmPrice = table.Column<int>(type: "int", nullable: true),
            //        Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AdminMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        DatePay = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        IsDelete = table.Column<bool>(type: "bit", nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CreditBills", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CreditBills_Credits_CreditId",
            //            column: x => x.CreditId,
            //            principalTable: "Credits",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_CreditBills_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserLegals",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<long>(type: "bigint", nullable: false),
            //        CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CodeEghtesadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShomareSabt = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CodeNaghshTajer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TarikhTasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShenaseMeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NoeSherkat = table.Column<int>(type: "int", nullable: false),
            //        NoeMalekiyat = table.Column<int>(type: "int", nullable: false),
            //        NoeTamalok = table.Column<int>(type: "int", nullable: false),
            //        TozihatSherkat = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CodePostiNeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TelephoneSabet = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FileRuznameRasmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FileAkharinTaghirat = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FileSahebanEmza = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FileAgahiTasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsDelete = table.Column<bool>(type: "bit", nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserLegals", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserLegals_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_CreditBills_CreditId",
            //    table: "CreditBills",
            //    column: "CreditId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CreditBills_UserId",
            //    table: "CreditBills",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLegals_UserId",
            //    table: "UserLegals",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CooperationRequests");

            migrationBuilder.DropTable(
                name: "CreditBills");

            migrationBuilder.DropTable(
                name: "UserLegals");

            migrationBuilder.DropColumn(
                name: "CountColleague",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "MaxOrderCountColleague",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "PriceColleague",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "CreditExpDate",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "TrakingCode",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "CreditDocumentTypes");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "CreditDocumentTypes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CreditDocuments");

            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Carts");

            migrationBuilder.AlterColumn<double>(
                name: "ZemanatBankiMablagh",
                table: "Credits",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "VasigheMelkiMablagh",
                table: "Credits",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Darsad",
                table: "CreditPartners",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TypeFile",
                table: "CreditDocumentTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CreditDocumentTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
