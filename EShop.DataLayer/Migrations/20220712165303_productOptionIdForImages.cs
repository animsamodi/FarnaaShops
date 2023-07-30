using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class productOptionIdForImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductOptionId",
                table: "ProductImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductOptionId",
                table: "ProductImages",
                column: "ProductOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductOptions_ProductOptionId",
                table: "ProductImages",
                column: "ProductOptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductOptions_ProductOptionId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductOptionId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductOptionId",
                table: "ProductImages");
        }
    }
}
