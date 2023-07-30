using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_range_colleague_plus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColleaguePlusPriceRanges",
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
                    table.PrimaryKey("PK_ColleaguePlusPriceRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColleaguePlusPriceRanges_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColleaguePlusPriceRanges_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColleaguePlusPriceRanges_ProductSeris_SeriId",
                        column: x => x.SeriId,
                        principalTable: "ProductSeris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColleaguePlusPriceRanges_BrandId",
                table: "ColleaguePlusPriceRanges",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ColleaguePlusPriceRanges_CategoryId",
                table: "ColleaguePlusPriceRanges",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ColleaguePlusPriceRanges_SeriId",
                table: "ColleaguePlusPriceRanges",
                column: "SeriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColleaguePlusPriceRanges");
        }
    }
}
