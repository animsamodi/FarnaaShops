using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_shop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleTransaction_Orders_OrderId",
                table: "SaleTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleTransaction_Variants_VariantId",
                table: "SaleTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleTransaction",
                table: "SaleTransaction");

            migrationBuilder.RenameTable(
                name: "SaleTransaction",
                newName: "SaleTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_SaleTransaction_VariantId",
                table: "SaleTransactions",
                newName: "IX_SaleTransactions_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleTransaction_OrderId",
                table: "SaleTransactions",
                newName: "IX_SaleTransactions_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleTransactions",
                table: "SaleTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleTransactions_Orders_OrderId",
                table: "SaleTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleTransactions_Variants_VariantId",
                table: "SaleTransactions",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleTransactions_Orders_OrderId",
                table: "SaleTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleTransactions_Variants_VariantId",
                table: "SaleTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleTransactions",
                table: "SaleTransactions");

            migrationBuilder.RenameTable(
                name: "SaleTransactions",
                newName: "SaleTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_SaleTransactions_VariantId",
                table: "SaleTransaction",
                newName: "IX_SaleTransaction_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleTransactions_OrderId",
                table: "SaleTransaction",
                newName: "IX_SaleTransaction_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleTransaction",
                table: "SaleTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleTransaction_Orders_OrderId",
                table: "SaleTransaction",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleTransaction_Variants_VariantId",
                table: "SaleTransaction",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
