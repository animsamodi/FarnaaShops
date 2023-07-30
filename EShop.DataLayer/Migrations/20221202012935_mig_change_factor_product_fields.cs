using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_factor_product_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierFactorProducts_SupplierFactorProducts_SupplierFactorProductId",
                table: "SupplierFactorProducts");

            migrationBuilder.DropIndex(
                name: "IX_SupplierFactorProducts_SupplierFactorProductId",
                table: "SupplierFactorProducts");

            migrationBuilder.DropColumn(
                name: "SupplierFactorProductId",
                table: "SupplierFactorProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SupplierFactorProductId",
                table: "SupplierFactorProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierFactorProducts_SupplierFactorProductId",
                table: "SupplierFactorProducts",
                column: "SupplierFactorProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierFactorProducts_SupplierFactorProducts_SupplierFactorProductId",
                table: "SupplierFactorProducts",
                column: "SupplierFactorProductId",
                principalTable: "SupplierFactorProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
