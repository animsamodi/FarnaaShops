using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class product2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategories_Brands_BrandId",
                table: "BrandCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategories_Categories_CategoryId",
                table: "BrandCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRatings_Categories_CategoryId",
                table: "CategoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRatings_RatingAttributes_RatingAttributeId",
                table: "CategoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_Products_ProductId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewRatings_Products_ProductId",
                table: "ProductReviewRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewRatings_RatingAttributes_RatingAttributeId",
                table: "ProductReviewRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_Categories_CategoryId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_PropertyName_PropertyNameId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyName_PropertyGroup_PropertyGroupId",
                table: "PropertyName");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValue_PropertyName_PropertyNameId",
                table: "PropertyValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_ParentId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_SubId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategories_Brands_BrandId",
                table: "BrandCategories",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategories_Categories_CategoryId",
                table: "BrandCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRatings_Categories_CategoryId",
                table: "CategoryRatings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRatings_RatingAttributes_RatingAttributeId",
                table: "CategoryRatings",
                column: "RatingAttributeId",
                principalTable: "RatingAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperty_Products_ProductId",
                table: "ProductProperty",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty",
                column: "PropertyValueId",
                principalTable: "PropertyValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewRatings_Products_ProductId",
                table: "ProductReviewRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewRatings_RatingAttributes_RatingAttributeId",
                table: "ProductReviewRatings",
                column: "RatingAttributeId",
                principalTable: "RatingAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_Categories_CategoryId",
                table: "PropertyCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_PropertyName_PropertyNameId",
                table: "PropertyCategory",
                column: "PropertyNameId",
                principalTable: "PropertyName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyName_PropertyGroup_PropertyGroupId",
                table: "PropertyName",
                column: "PropertyGroupId",
                principalTable: "PropertyGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValue_PropertyName_PropertyNameId",
                table: "PropertyValue",
                column: "PropertyNameId",
                principalTable: "PropertyName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_ParentId",
                table: "SubCategories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_SubId",
                table: "SubCategories",
                column: "SubId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategories_Brands_BrandId",
                table: "BrandCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BrandCategories_Categories_CategoryId",
                table: "BrandCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRatings_Categories_CategoryId",
                table: "CategoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRatings_RatingAttributes_RatingAttributeId",
                table: "CategoryRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_Products_ProductId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewRatings_Products_ProductId",
                table: "ProductReviewRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewRatings_RatingAttributes_RatingAttributeId",
                table: "ProductReviewRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_Categories_CategoryId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_PropertyName_PropertyNameId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyName_PropertyGroup_PropertyGroupId",
                table: "PropertyName");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValue_PropertyName_PropertyNameId",
                table: "PropertyValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_ParentId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_SubId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategories_Brands_BrandId",
                table: "BrandCategories",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrandCategories_Categories_CategoryId",
                table: "BrandCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRatings_Categories_CategoryId",
                table: "CategoryRatings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRatings_RatingAttributes_RatingAttributeId",
                table: "CategoryRatings",
                column: "RatingAttributeId",
                principalTable: "RatingAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperty_Products_ProductId",
                table: "ProductProperty",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty",
                column: "PropertyValueId",
                principalTable: "PropertyValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewRatings_Products_ProductId",
                table: "ProductReviewRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewRatings_RatingAttributes_RatingAttributeId",
                table: "ProductReviewRatings",
                column: "RatingAttributeId",
                principalTable: "RatingAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_Categories_CategoryId",
                table: "PropertyCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_PropertyName_PropertyNameId",
                table: "PropertyCategory",
                column: "PropertyNameId",
                principalTable: "PropertyName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyName_PropertyGroup_PropertyGroupId",
                table: "PropertyName",
                column: "PropertyGroupId",
                principalTable: "PropertyGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValue_PropertyName_PropertyNameId",
                table: "PropertyValue",
                column: "PropertyNameId",
                principalTable: "PropertyName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_ParentId",
                table: "SubCategories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_SubId",
                table: "SubCategories",
                column: "SubId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
