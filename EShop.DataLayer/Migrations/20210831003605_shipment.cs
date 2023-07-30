using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class shipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Shipment_ShipmentId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipment_ShipmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Cities_CityId",
                table: "Shipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Provinces_ProvinceId",
                table: "Shipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipment",
                table: "Shipment");

            migrationBuilder.RenameTable(
                name: "Shipment",
                newName: "Shipments");

            migrationBuilder.RenameIndex(
                name: "IX_Shipment_ProvinceId",
                table: "Shipments",
                newName: "IX_Shipments_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Shipment_CityId",
                table: "Shipments",
                newName: "IX_Shipments_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipments",
                table: "Shipments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Shipments_ShipmentId",
                table: "Carts",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shipments_ShipmentId",
                table: "Orders",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Cities_CityId",
                table: "Shipments",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Provinces_ProvinceId",
                table: "Shipments",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Shipments_ShipmentId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipments_ShipmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Cities_CityId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Provinces_ProvinceId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipments",
                table: "Shipments");

            migrationBuilder.RenameTable(
                name: "Shipments",
                newName: "Shipment");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_ProvinceId",
                table: "Shipment",
                newName: "IX_Shipment_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_CityId",
                table: "Shipment",
                newName: "IX_Shipment_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipment",
                table: "Shipment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Shipment_ShipmentId",
                table: "Carts",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shipment_ShipmentId",
                table: "Orders",
                column: "ShipmentId",
                principalTable: "Shipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Cities_CityId",
                table: "Shipment",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Provinces_ProvinceId",
                table: "Shipment",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
