using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class shop_ig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisson_Permisson_ParentId",
                table: "Permisson");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_Products_ProductId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperty_PropertyValues_PropertyValueId",
                table: "ProductProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_Categories_CategoryId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategory_PropertyNames_PropertyNameId",
                table: "PropertyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermisson_Permisson_PermissonId",
                table: "RolePermisson");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermisson_Role_RoleId",
                table: "RolePermisson");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductFovorites_Products_ProductId",
                table: "UserProductFovorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductFovorites_Users_UserId",
                table: "UserProductFovorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProductFovorites",
                table: "UserProductFovorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermisson",
                table: "RolePermisson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyCategory",
                table: "PropertyCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProperty",
                table: "ProductProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permisson",
                table: "Permisson");

            migrationBuilder.RenameTable(
                name: "UserProductFovorites",
                newName: "UserProductFovoriteses");

            migrationBuilder.RenameTable(
                name: "RolePermisson",
                newName: "RolePermissons");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "PropertyCategory",
                newName: "PropertyCategories");

            migrationBuilder.RenameTable(
                name: "ProductProperty",
                newName: "ProductProperties");

            migrationBuilder.RenameTable(
                name: "Permisson",
                newName: "Permissons");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductFovorites_UserId",
                table: "UserProductFovoriteses",
                newName: "IX_UserProductFovoriteses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductFovorites_ProductId",
                table: "UserProductFovoriteses",
                newName: "IX_UserProductFovoriteses_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermisson_RoleId",
                table: "RolePermissons",
                newName: "IX_RolePermissons_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermisson_PermissonId",
                table: "RolePermissons",
                newName: "IX_RolePermissons_PermissonId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyCategory_PropertyNameId",
                table: "PropertyCategories",
                newName: "IX_PropertyCategories_PropertyNameId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyCategory_CategoryId",
                table: "PropertyCategories",
                newName: "IX_PropertyCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProperty_PropertyValueId",
                table: "ProductProperties",
                newName: "IX_ProductProperties_PropertyValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProperty_ProductId",
                table: "ProductProperties",
                newName: "IX_ProductProperties_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Permisson_ParentId",
                table: "Permissons",
                newName: "IX_Permissons_ParentId");

            migrationBuilder.AddColumn<long>(
                name: "UserAddressId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProductFovoriteses",
                table: "UserProductFovoriteses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissons",
                table: "RolePermissons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyCategories",
                table: "PropertyCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProperties",
                table: "ProductProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissons",
                table: "Permissons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    MinOrderAmount = table.Column<int>(type: "int", nullable: true),
                    MaxUseCount = table.Column<int>(type: "int", nullable: true),
                    ForFirstOrder = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountCodes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guarantees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeaderSearches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OtherTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderSearches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientTel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    RecipientAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RecipientPostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SumAmount = table.Column<int>(type: "int", nullable: false),
                    AmountPayable = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateProductAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CmpId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Promotion_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimleySupply = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PostingWarranty = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    NoReturend = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PurchaseConsent = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<long>(type: "bigint", nullable: false),
                    BalanceReceivable = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryTime = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCardTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftCardId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCardTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCardTransaction_GiftCards_GiftCardId",
                        column: x => x.GiftCardId,
                        principalTable: "GiftCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftCardTransaction_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RefIf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiscountCodeId = table.Column<long>(type: "bigint", nullable: true),
                    GiftCartId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_DiscountCodes_DiscountCodeId",
                        column: x => x.DiscountCodeId,
                        principalTable: "DiscountCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_GiftCards_GiftCartId",
                        column: x => x.GiftCartId,
                        principalTable: "GiftCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HowSend = table.Column<byte>(type: "tinyint", nullable: false),
                    PostalBarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCost = table.Column<int>(type: "int", nullable: false),
                    SumAmount = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipment_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SepcialPrice = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ShopCount = table.Column<int>(type: "int", nullable: false),
                    MaxOrderCount = table.Column<int>(type: "int", nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseConsentPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotallySatisfied = table.Column<byte>(type: "tinyint", nullable: false),
                    Satisfied = table.Column<byte>(type: "tinyint", nullable: false),
                    Neutral = table.Column<byte>(type: "tinyint", nullable: false),
                    DisSatisfied = table.Column<int>(type: "int", nullable: false),
                    TotallyDisSatisfied = table.Column<int>(type: "int", nullable: false),
                    ReserveCount = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProductOptionId = table.Column<long>(type: "bigint", nullable: false),
                    GuaranteeId = table.Column<long>(type: "bigint", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Guarantees_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "Guarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_ProductOptions_ProductOptionId",
                        column: x => x.ProductOptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DiscountPrice = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<byte>(type: "tinyint", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    ProductOptionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductOptions_ProductOptionId",
                        column: x => x.ProductOptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UnitDiscount = table.Column<int>(type: "int", nullable: false),
                    SumPriceAfterDiscount = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleTransaction_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleTransaction_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDetails",
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
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    ShipmmentId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "VariantPromotions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<byte>(type: "tinyint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    MaxOrderCount = table.Column<int>(type: "int", nullable: true),
                    ReminaingCount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    PromotionId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantPromotions_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VariantPromotions_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coockie = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    ShippingType = table.Column<bool>(type: "bit", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_UserAddresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "UserAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VariantVoteDetial",
                columns: table => new
                {
                    VariantVoteDetialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vote = table.Column<byte>(type: "tinyint", nullable: false),
                    ShipmentDetailId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantVoteDetial", x => x.VariantVoteDetialId);
                    table.ForeignKey(
                        name: "FK_VariantVoteDetial_ShipmentDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "ShipmentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    IsActiveCart = table.Column<bool>(type: "bit", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDetails_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartDetails_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserAddressId",
                table: "Users",
                column: "UserAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_VariantId",
                table: "CartDetails",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AddressId",
                table: "Carts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_VariantId",
                table: "Carts",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_CategoryId",
                table: "DiscountCodes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_UserId",
                table: "GiftCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardTransaction_GiftCardId",
                table: "GiftCardTransaction",
                column: "GiftCardId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardTransaction_OrderId",
                table: "GiftCardTransaction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_DiscountCodeId",
                table: "PaymentDetails",
                column: "DiscountCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_GiftCartId",
                table: "PaymentDetails",
                column: "GiftCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_OrderId",
                table: "PaymentDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductOptionId",
                table: "ProductPrices",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_VariantId",
                table: "ProductPrices",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_BrandId",
                table: "Promotion",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_CategoryId",
                table: "Promotion",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleTransaction_OrderId",
                table: "SaleTransaction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleTransaction_VariantId",
                table: "SaleTransaction",
                column: "VariantId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_CityId",
                table: "UserAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_ProvinceId",
                table: "UserAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantPromotions_PromotionId",
                table: "VariantPromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantPromotions_VariantId",
                table: "VariantPromotions",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_GuaranteeId",
                table: "Variants",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductOptionId",
                table: "Variants",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_SellerId",
                table: "Variants",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantVoteDetial_OrderDetailId",
                table: "VariantVoteDetial",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissons_Permissons_ParentId",
                table: "Permissons",
                column: "ParentId",
                principalTable: "Permissons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Products_ProductId",
                table: "ProductProperties",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_PropertyValues_PropertyValueId",
                table: "ProductProperties",
                column: "PropertyValueId",
                principalTable: "PropertyValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategories_Categories_CategoryId",
                table: "PropertyCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCategories_PropertyNames_PropertyNameId",
                table: "PropertyCategories",
                column: "PropertyNameId",
                principalTable: "PropertyNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissons_Permissons_PermissonId",
                table: "RolePermissons",
                column: "PermissonId",
                principalTable: "Permissons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissons_Roles_RoleId",
                table: "RolePermissons",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductFovoriteses_Products_ProductId",
                table: "UserProductFovoriteses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductFovoriteses_Users_UserId",
                table: "UserProductFovoriteses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserAddresses_UserAddressId",
                table: "Users",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissons_Permissons_ParentId",
                table: "Permissons");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_Products_ProductId",
                table: "ProductProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_PropertyValues_PropertyValueId",
                table: "ProductProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategories_Categories_CategoryId",
                table: "PropertyCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCategories_PropertyNames_PropertyNameId",
                table: "PropertyCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissons_Permissons_PermissonId",
                table: "RolePermissons");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissons_Roles_RoleId",
                table: "RolePermissons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductFovoriteses_Products_ProductId",
                table: "UserProductFovoriteses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductFovoriteses_Users_UserId",
                table: "UserProductFovoriteses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserAddresses_UserAddressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "GiftCardTransaction");

            migrationBuilder.DropTable(
                name: "HeaderSearches");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "SaleTransaction");

            migrationBuilder.DropTable(
                name: "VariantPromotions");

            migrationBuilder.DropTable(
                name: "VariantVoteDetial");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "DiscountCodes");

            migrationBuilder.DropTable(
                name: "GiftCards");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "ShipmentDetails");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Guarantees");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserAddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProductFovoriteses",
                table: "UserProductFovoriteses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissons",
                table: "RolePermissons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyCategories",
                table: "PropertyCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProperties",
                table: "ProductProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissons",
                table: "Permissons");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserProductFovoriteses",
                newName: "UserProductFovorites");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RolePermissons",
                newName: "RolePermisson");

            migrationBuilder.RenameTable(
                name: "PropertyCategories",
                newName: "PropertyCategory");

            migrationBuilder.RenameTable(
                name: "ProductProperties",
                newName: "ProductProperty");

            migrationBuilder.RenameTable(
                name: "Permissons",
                newName: "Permisson");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductFovoriteses_UserId",
                table: "UserProductFovorites",
                newName: "IX_UserProductFovorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductFovoriteses_ProductId",
                table: "UserProductFovorites",
                newName: "IX_UserProductFovorites_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissons_RoleId",
                table: "RolePermisson",
                newName: "IX_RolePermisson_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissons_PermissonId",
                table: "RolePermisson",
                newName: "IX_RolePermisson_PermissonId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyCategories_PropertyNameId",
                table: "PropertyCategory",
                newName: "IX_PropertyCategory_PropertyNameId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyCategories_CategoryId",
                table: "PropertyCategory",
                newName: "IX_PropertyCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProperties_PropertyValueId",
                table: "ProductProperty",
                newName: "IX_ProductProperty_PropertyValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProperties_ProductId",
                table: "ProductProperty",
                newName: "IX_ProductProperty_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissons_ParentId",
                table: "Permisson",
                newName: "IX_Permisson_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProductFovorites",
                table: "UserProductFovorites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermisson",
                table: "RolePermisson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyCategory",
                table: "PropertyCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProperty",
                table: "ProductProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permisson",
                table: "Permisson",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisson_Permisson_ParentId",
                table: "Permisson",
                column: "ParentId",
                principalTable: "Permisson",
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
                name: "FK_ProductProperty_PropertyValues_PropertyValueId",
                table: "ProductProperty",
                column: "PropertyValueId",
                principalTable: "PropertyValues",
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
                name: "FK_PropertyCategory_PropertyNames_PropertyNameId",
                table: "PropertyCategory",
                column: "PropertyNameId",
                principalTable: "PropertyNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermisson_Permisson_PermissonId",
                table: "RolePermisson",
                column: "PermissonId",
                principalTable: "Permisson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermisson_Role_RoleId",
                table: "RolePermisson",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductFovorites_Products_ProductId",
                table: "UserProductFovorites",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductFovorites_Users_UserId",
                table: "UserProductFovorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
