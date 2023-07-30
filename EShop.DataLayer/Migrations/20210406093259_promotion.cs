using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Brands_BrandId",
                table: "Promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Categories_CategoryId",
                table: "Promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantPromotions_Promotion_PromotionId",
                table: "VariantPromotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion");

            migrationBuilder.RenameTable(
                name: "Promotion",
                newName: "Promotions");

            migrationBuilder.RenameIndex(
                name: "IX_Promotion_CategoryId",
                table: "Promotions",
                newName: "IX_Promotions_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Promotion_BrandId",
                table: "Promotions",
                newName: "IX_Promotions_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Brands_BrandId",
                table: "Promotions",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Categories_CategoryId",
                table: "Promotions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantPromotions_Promotions_PromotionId",
                table: "VariantPromotions",
                column: "PromotionId",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Brands_BrandId",
                table: "Promotions");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Categories_CategoryId",
                table: "Promotions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantPromotions_Promotions_PromotionId",
                table: "VariantPromotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "Promotion");

            migrationBuilder.RenameIndex(
                name: "IX_Promotions_CategoryId",
                table: "Promotion",
                newName: "IX_Promotion_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Promotions_BrandId",
                table: "Promotion",
                newName: "IX_Promotion_BrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Brands_BrandId",
                table: "Promotion",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Categories_CategoryId",
                table: "Promotion",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantPromotions_Promotion_PromotionId",
                table: "VariantPromotions",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
