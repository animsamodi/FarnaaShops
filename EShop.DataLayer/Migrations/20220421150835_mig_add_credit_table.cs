using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_credit_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcceptPrice",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHoghughi",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CreditDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealLegal = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RealLegal = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShomareShenasname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Father = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceIssue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeEghtesadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShomareSabt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeNaghshTajer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhTasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhEngheza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaziyatMahal = table.Column<int>(type: "int", nullable: false),
                    NoeTamalokMahal = table.Column<int>(type: "int", nullable: false),
                    ShenaseMeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoeSherkat = table.Column<int>(type: "int", nullable: false),
                    NoeMalekiyat = table.Column<int>(type: "int", nullable: false),
                    NoeTamalok = table.Column<int>(type: "int", nullable: false),
                    TozihatSherkat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostiNeshaniMahaleKar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeshaniMahaleErsal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostiNeshaniMahaleErsal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneSabet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SahamdarHagheEmza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoeKharid = table.Column<int>(type: "int", nullable: false),
                    ZemanatBankiMablagh = table.Column<double>(type: "float", nullable: false),
                    VasigheMelkiMablagh = table.Column<double>(type: "float", nullable: false),
                    MudiMaliyati = table.Column<int>(type: "int", nullable: false),
                    AdminMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditStatus = table.Column<int>(type: "int", nullable: false),
                    AcceptPrice = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditId = table.Column<long>(type: "bigint", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shobe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jari = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShomareKart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shahrestan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditAccounts_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditId = table.Column<long>(type: "bigint", nullable: false),
                    CreditDocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditDocuments_CreditDocumentTypes_CreditDocumentTypeId",
                        column: x => x.CreditDocumentTypeId,
                        principalTable: "CreditDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditDocuments_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditPartners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Darsad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditPartners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditPartners_Credits_CreditId",
                        column: x => x.CreditId,
                        principalTable: "Credits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditAccounts_CreditId",
                table: "CreditAccounts",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDocuments_CreditDocumentTypeId",
                table: "CreditDocuments",
                column: "CreditDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDocuments_CreditId",
                table: "CreditDocuments",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditPartners_CreditId",
                table: "CreditPartners",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_UserId",
                table: "Credits",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditAccounts");

            migrationBuilder.DropTable(
                name: "CreditDocuments");

            migrationBuilder.DropTable(
                name: "CreditPartners");

            migrationBuilder.DropTable(
                name: "CreditDocumentTypes");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropColumn(
                name: "AcceptPrice",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsHoghughi",
                table: "Users");
        }
    }
}
