using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_table_range_plus_variant_plus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountPlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GetPlusPriceFromOrginal",
                table: "Variants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxOrderCountPlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PricePlus",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SepcialPlusPrice",
                table: "Variants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlusPriceRanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    SeriId = table.Column<long>(type: "bigint", nullable: true),
                    MinPrice = table.Column<int>(type: "int", nullable: false),
                    MaxPrice = table.Column<int>(type: "int", nullable: false),
                    ChangePricePercent = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlusPriceRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlusPriceRanges_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlusPriceRanges_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlusPriceRanges_ProductSeris_SeriId",
                        column: x => x.SeriId,
                        principalTable: "ProductSeris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlusPriceRanges_BrandId",
                table: "PlusPriceRanges",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PlusPriceRanges_CategoryId",
                table: "PlusPriceRanges",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlusPriceRanges_SeriId",
                table: "PlusPriceRanges",
                column: "SeriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlusPriceRanges");

            migrationBuilder.DropColumn(
                name: "CountPlus",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "GetPlusPriceFromOrginal",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "MaxOrderCountPlus",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "PricePlus",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "SepcialPlusPrice",
                table: "Variants");
        }
    }
}
