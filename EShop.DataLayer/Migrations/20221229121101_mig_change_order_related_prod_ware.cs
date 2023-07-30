using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_order_related_prod_ware : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
