using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_supplier_product_alguritms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierFactorProducts_ProductOptions_ProductOptionId",
                table: "SupplierFactorProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierFactorProducts_Products_ProductId",
                table: "SupplierFactorProducts");

            migrationBuilder.DropIndex(
                name: "IX_SupplierFactorProducts_ProductId",
                table: "SupplierFactorProducts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SupplierFactorProducts");

            migrationBuilder.RenameColumn(
                name: "ProductOptionId",
                table: "SupplierFactorProducts",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierFactorProducts_ProductOptionId",
                table: "SupplierFactorProducts",
                newName: "IX_SupplierFactorProducts_VariantId");

            migrationBuilder.AlterColumn<long>(
                name: "GuaranteeId",
                table: "SupplierFactorProducts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFactorProducts_Variants_VariantId",
                table: "SupplierFactorProducts",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierFactorProducts_Variants_VariantId",
                table: "SupplierFactorProducts");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "SupplierFactorProducts",
                newName: "ProductOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierFactorProducts_VariantId",
                table: "SupplierFactorProducts",
                newName: "IX_SupplierFactorProducts_ProductOptionId");

            migrationBuilder.AlterColumn<long>(
                name: "GuaranteeId",
                table: "SupplierFactorProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "SupplierFactorProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierFactorProducts_ProductId",
                table: "SupplierFactorProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFactorProducts_ProductOptions_ProductOptionId",
                table: "SupplierFactorProducts",
                column: "ProductOptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFactorProducts_Products_ProductId",
                table: "SupplierFactorProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
