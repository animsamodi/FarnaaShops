using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_related_wear_product_to_orderdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderDetailId",
                table: "WarehouseProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WarehouseProductId",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_OrderDetailId",
                table: "WarehouseProducts",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_WarehouseProductId",
                table: "OrderDetails",
                column: "WarehouseProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_WarehouseProducts_WarehouseProductId",
                table: "OrderDetails",
                column: "WarehouseProductId",
                principalTable: "WarehouseProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProducts_OrderDetails_OrderDetailId",
                table: "WarehouseProducts",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_WarehouseProducts_WarehouseProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProducts_OrderDetails_OrderDetailId",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_OrderDetailId",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_WarehouseProductId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseProductId",
                table: "OrderDetails");
        }
    }
}
