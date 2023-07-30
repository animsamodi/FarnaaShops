using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_user_legal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePay",
                table: "CreditBills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLegals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeEghtesadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShomareSabt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeNaghshTajer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhTasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShenaseMeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoeSherkat = table.Column<int>(type: "int", nullable: false),
                    NoeMalekiyat = table.Column<int>(type: "int", nullable: false),
                    NoeTamalok = table.Column<int>(type: "int", nullable: false),
                    TozihatSherkat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostiNeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneSabet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileRuznameRasmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileAkharinTaghirat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSahebanEmza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileAgahiTasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLegals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLegals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLegals_UserId",
                table: "UserLegals",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLegals");

            migrationBuilder.DropColumn(
                name: "DatePay",
                table: "CreditBills");
        }
    }
}
