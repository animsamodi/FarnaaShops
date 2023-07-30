using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_discount_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "DiscountCodes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodes",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "DiscountCodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VariantId",
                table: "DiscountCodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_ProductId",
                table: "DiscountCodes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_VariantId",
                table: "DiscountCodes",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Products_ProductId",
                table: "DiscountCodes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Variants_VariantId",
                table: "DiscountCodes",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Products_ProductId",
                table: "DiscountCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Variants_VariantId",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_ProductId",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_VariantId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "DiscountCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
