using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class changeRelatedproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct_Products_ProductId1",
                table: "RelatedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProduct_Products_ProductId2",
                table: "RelatedProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedProduct",
                table: "RelatedProduct");

            migrationBuilder.RenameTable(
                name: "RelatedProduct",
                newName: "RelatedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedProduct_ProductId2",
                table: "RelatedProducts",
                newName: "IX_RelatedProducts_ProductId2");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedProduct_ProductId1",
                table: "RelatedProducts",
                newName: "IX_RelatedProducts_ProductId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedProducts",
                table: "RelatedProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProducts_Products_ProductId1",
                table: "RelatedProducts",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProducts_Products_ProductId2",
                table: "RelatedProducts",
                column: "ProductId2",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProducts_Products_ProductId1",
                table: "RelatedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedProducts_Products_ProductId2",
                table: "RelatedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedProducts",
                table: "RelatedProducts");

            migrationBuilder.RenameTable(
                name: "RelatedProducts",
                newName: "RelatedProduct");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedProducts_ProductId2",
                table: "RelatedProduct",
                newName: "IX_RelatedProduct_ProductId2");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedProducts_ProductId1",
                table: "RelatedProduct",
                newName: "IX_RelatedProduct_ProductId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedProduct",
                table: "RelatedProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct_Products_ProductId1",
                table: "RelatedProduct",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedProduct_Products_ProductId2",
                table: "RelatedProduct",
                column: "ProductId2",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
