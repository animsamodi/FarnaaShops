using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Categories_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Products_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_PropertyName_PropertyNameId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyName_PropertyGroup_PropertyGroupId",
                table: "PropertyName");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValue_PropertyName_PropertyNameId",
                table: "PropertyValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValue",
                table: "PropertyValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyName",
                table: "PropertyName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyGroup",
                table: "PropertyGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "PropertyValue",
                newName: "PropertyValues");

            migrationBuilder.RenameTable(
                name: "PropertyName",
                newName: "PropertyNames");

            migrationBuilder.RenameTable(
                name: "PropertyGroup",
                newName: "PropertyGroups");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "ProductCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyValue_PropertyNameId",
                table: "PropertyValues",
                newName: "IX_PropertyValues_PropertyNameId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyName_PropertyGroupId",
                table: "PropertyNames",
                newName: "IX_PropertyNames_PropertyGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValues",
                table: "PropertyValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyNames",
                table: "PropertyNames",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyGroups",
                table: "PropertyGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperty_PropertyValues_PropertyValueId",
                table: "ProductProperty",
                column: "PropertyValueId",
                principalTable: "PropertyValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategory_PropertyNames_PropertyNameId",
                table: "PropertyCategory",
                column: "PropertyNameId",
                principalTable: "PropertyNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyNames_PropertyGroups_PropertyGroupId",
                table: "PropertyNames",
                column: "PropertyGroupId",
                principalTable: "PropertyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_PropertyNames_PropertyNameId",
                table: "PropertyValues",
                column: "PropertyNameId",
                principalTable: "PropertyNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_PropertyValues_PropertyValueId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_PropertyNames_PropertyNameId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyNames_PropertyGroups_PropertyGroupId",
                table: "PropertyNames");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_PropertyNames_PropertyNameId",
                table: "PropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValues",
                table: "PropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyNames",
                table: "PropertyNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyGroups",
                table: "PropertyGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "PropertyValues",
                newName: "PropertyValue");

            migrationBuilder.RenameTable(
                name: "PropertyNames",
                newName: "PropertyName");

            migrationBuilder.RenameTable(
                name: "PropertyGroups",
                newName: "PropertyGroup");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyValues_PropertyNameId",
                table: "PropertyValue",
                newName: "IX_PropertyValue_PropertyNameId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyNames_PropertyGroupId",
                table: "PropertyName",
                newName: "IX_PropertyName_PropertyGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValue",
                table: "PropertyValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyName",
                table: "PropertyName",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyGroup",
                table: "PropertyGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                column: "Id");

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
                name: "FK_ProductProperty_PropertyValue_PropertyValueId",
                table: "ProductProperty",
                column: "PropertyValueId",
                principalTable: "PropertyValue",
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
        }
    }
}
