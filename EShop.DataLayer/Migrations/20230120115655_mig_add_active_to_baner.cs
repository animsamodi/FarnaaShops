using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_active_to_baner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductViews_Products_ProductId",
                table: "ProductViews");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductViews_Users_UserId",
                table: "ProductViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductViews",
                table: "ProductViews");

            migrationBuilder.RenameTable(
                name: "ProductViews",
                newName: "UserProductViews");

            migrationBuilder.RenameIndex(
                name: "IX_ProductViews_UserId",
                table: "UserProductViews",
                newName: "IX_UserProductViews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductViews_ProductId",
                table: "UserProductViews",
                newName: "IX_UserProductViews_ProductId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BannerImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProductViews",
                table: "UserProductViews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductViews_Products_ProductId",
                table: "UserProductViews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductViews_Users_UserId",
                table: "UserProductViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductViews_Products_ProductId",
                table: "UserProductViews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductViews_Users_UserId",
                table: "UserProductViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProductViews",
                table: "UserProductViews");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BannerImages");

            migrationBuilder.RenameTable(
                name: "UserProductViews",
                newName: "ProductViews");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductViews_UserId",
                table: "ProductViews",
                newName: "IX_ProductViews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductViews_ProductId",
                table: "ProductViews",
                newName: "IX_ProductViews_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductViews",
                table: "ProductViews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductViews_Products_ProductId",
                table: "ProductViews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductViews_Users_UserId",
                table: "ProductViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
