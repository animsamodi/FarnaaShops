using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class changeorderSenario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Orders_OrderId",
                table: "Shipment");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantVoteDetial_ShipmentDetails_OrderDetailId",
                table: "VariantVoteDetial");

            migrationBuilder.DropTable(
                name: "ShipmentDetails");

            migrationBuilder.DropIndex(
                name: "IX_VariantVoteDetial_OrderDetailId",
                table: "VariantVoteDetial");

            migrationBuilder.DropIndex(
                name: "IX_Shipment_OrderId",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "ShipmentDetailId",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "HowSend",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "ShippingType",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "SumAmount",
                table: "Shipment",
                newName: "PricePerAddProduct");

            migrationBuilder.RenameColumn(
                name: "ShippingCost",
                table: "Shipment",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PostalBarCode",
                table: "Shipment",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RefIf",
                table: "PaymentDetails",
                newName: "RefId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "VariantVoteDetial",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderDetailId1",
                table: "VariantVoteDetial",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Shipment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Shipment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Shipment",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientNatioalCode",
                table: "Orders",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientTel",
                table: "Orders",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GiftWrappingPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GiftWrappingText",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShipmentId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ShipmentId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SumPrice = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    SumPriceAfterDiscount = table.Column<int>(type: "int", nullable: false),
                    UnitDiscount = table.Column<int>(type: "int", nullable: false),
                    StorePlace = table.Column<bool>(type: "bit", nullable: false),
                    DiscountType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsGiftWrapping = table.Column<bool>(type: "bit", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantVoteDetial_OrderDetailId1",
                table: "VariantVoteDetial",
                column: "OrderDetailId1");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_CityId",
                table: "Shipment",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_ProvinceId",
                table: "Shipment",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentId",
                table: "Orders",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ShipmentId",
                table: "Carts",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_VariantId",
                table: "OrderDetails",
                column: "VariantId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VariantVoteDetial_OrderDetails_OrderDetailId1",
                table: "VariantVoteDetial",
                column: "OrderDetailId1",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_VariantVoteDetial_OrderDetails_OrderDetailId1",
                table: "VariantVoteDetial");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_VariantVoteDetial_OrderDetailId1",
                table: "VariantVoteDetial");

            migrationBuilder.DropIndex(
                name: "IX_Shipment_CityId",
                table: "Shipment");

            migrationBuilder.DropIndex(
                name: "IX_Shipment_ProvinceId",
                table: "Shipment");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipmentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ShipmentId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrderDetailId1",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Shipment");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientNatioalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientTel",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GiftWrappingPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GiftWrappingText",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Shipment",
                newName: "PostalBarCode");

            migrationBuilder.RenameColumn(
                name: "PricePerAddProduct",
                table: "Shipment",
                newName: "SumAmount");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Shipment",
                newName: "ShippingCost");

            migrationBuilder.RenameColumn(
                name: "RefId",
                table: "PaymentDetails",
                newName: "RefIf");

            migrationBuilder.AlterColumn<long>(
                name: "OrderDetailId",
                table: "VariantVoteDetial",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ShipmentDetailId",
                table: "VariantVoteDetial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "HowSend",
                table: "Shipment",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Shipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Shipment",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "ShippingType",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ShipmentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ShipmmentId = table.Column<long>(type: "bigint", nullable: false),
                    StorePlace = table.Column<bool>(type: "bit", nullable: false),
                    SumPrice = table.Column<int>(type: "int", nullable: false),
                    SumPriceAfterDiscount = table.Column<int>(type: "int", nullable: false),
                    UnitDiscount = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDetails_Shipment_ShipmmentId",
                        column: x => x.ShipmmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentDetails_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantVoteDetial_OrderDetailId",
                table: "VariantVoteDetial",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_OrderId",
                table: "Shipment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_ShipmmentId",
                table: "ShipmentDetails",
                column: "ShipmmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_VariantId",
                table: "ShipmentDetails",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Orders_OrderId",
                table: "Shipment",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantVoteDetial_ShipmentDetails_OrderDetailId",
                table: "VariantVoteDetial",
                column: "OrderDetailId",
                principalTable: "ShipmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
