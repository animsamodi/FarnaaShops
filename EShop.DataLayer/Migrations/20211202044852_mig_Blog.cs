using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_Blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "VariantVoteDetial",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "VariantPromotions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "UserProductFovoriteses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "UserDiscounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "UserCommentRatings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "UserAddresses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "SubCategories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "StaticPages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Sliders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "SiteSettings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Shipments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Sellers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "SaleTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "RolePermissons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "RelatedProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "RatingAttributes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Provinces",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "PropertyValues",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "PropertyNames",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "PropertyGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "PropertyCategories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Promotion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sell",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductReviews",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductReviewRatings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductProperties",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductPrices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "ProductCategories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Permissons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "PaymentDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "OrderLimits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "MainMenus",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "LimitOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "HeaderSearches",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Guarantees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "GiftCardTransaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "GiftCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "DiscountCodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CrmLogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CrmAccounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Comments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CommentRatings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CommentLikes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CategoryRatings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "CartDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Brands",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "BrandCategories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "BlockPostalCodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Banners",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "BannerImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChangeUserId",
                table: "Audits",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "VariantPromotions");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "UserProductFovoriteses");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "UserDiscounts");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "UserCommentRatings");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "StaticPages");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "SaleTransactions");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "RolePermissons");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "RelatedProducts");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "RatingAttributes");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "PropertyValues");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "PropertyNames");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "PropertyGroups");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "PropertyCategories");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sell",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductReviewRatings");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductProperties");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductOptions");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Permissons");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "OrderLimits");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "MainMenus");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "LimitOrders");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "HeaderSearches");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Guarantees");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "GiftCardTransaction");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "GiftCards");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CrmLogs");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CrmAccounts");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CommentRatings");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CommentLikes");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CategoryRatings");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "BrandCategories");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "BlockPostalCodes");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "BannerImages");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Audits");
        }
    }
}
