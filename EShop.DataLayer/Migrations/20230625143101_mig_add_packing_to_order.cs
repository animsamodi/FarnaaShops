using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_packing_to_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PackingId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackingPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PackingTitle",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PackingId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PackingId",
                table: "Orders",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PackingId",
                table: "Carts",
                column: "PackingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Packings_PackingId",
                table: "Carts",
                column: "PackingId",
                principalTable: "Packings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Packings_PackingId",
                table: "Orders",
                column: "PackingId",
                principalTable: "Packings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Packings_PackingId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Packings_PackingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PackingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_PackingId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PackingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingTitle",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingId",
                table: "Carts");
        }
    }
}
