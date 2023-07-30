using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_related_wear_product_to_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "WarehouseProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_OrderId",
                table: "WarehouseProducts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProducts_Orders_OrderId",
                table: "WarehouseProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProducts_Orders_OrderId",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_OrderId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "WarehouseProducts");
        }
    }
}
