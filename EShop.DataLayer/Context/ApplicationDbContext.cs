using EShop.Core.ViewModels;
using EShop.DataLayer.Configurations;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Components;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Entities.Ofogh;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.Promotion;
using EShop.DataLayer.Entities.Property;
using EShop.DataLayer.Entities.Seller;
using EShop.DataLayer.Entities.Seo;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.QueryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EShop.DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion

        #region Db Sets

        public DbSet<ColleaugeSetting> ColleaugeSettings { get; set; }
        public DbSet<OfoghHistory> OfoghHistories { get; set; }
        public DbSet<CooperationRequest> CooperationRequests { get; set; }
        //
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ReleaseNote> ReleaseNotes { get; set; }
        public DbSet<EducationalDoc> EducationalDocs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<CategoryRating> CategoryRatings { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<RatingAttribute> RatingAttributes { get; set; }
        public DbSet<ProductReviewRating> ProductReviewRatings { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<RelatedProduct> RelatedProducts { get; set; }
        public DbSet<ProductAccessories> ProductAccessorieses { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<PropertyGroup> PropertyGroups { get; set; }
        public DbSet<PropertyName> PropertyNames { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<CommentRating> CommentRatings { get; set; }
        public DbSet<UserCommentRating> UserCommentRatings { get; set; }
        public DbSet<MainMenu> MainMenus { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerImage> BannerImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Guarantee> Guarantees { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<VariantPromotion> VariantPromotions { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LimitOrder> LimitOrders { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SaleTransaction> SaleTransactions { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<CrmLog> CrmLogs { get; set; }
        public DbSet<CrmAccount> CrmAccounts { get; set; }
        public DbSet<OrderLimit> OrderLimits { get; set; }
        public DbSet<AsanPardakhtLog> AsanPardakhtLogs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<BlockPostalCode> BlockPostalCodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLegal> UserLegals { get; set; }
        public DbSet<UserProductFovorites> UserProductFovoriteses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permisson> Permissons { get; set; }
        public DbSet<RolePermisson> RolePermissons { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<CategotyMain> CategotyMains { get; set; }
        public DbSet<IndexLayout> IndexLayouts { get; set; }
        public DbSet<CategoryBrandPage> CategoryBrandPages { get; set; }

        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditAccount> CreditAccounts { get; set; }
        public DbSet<CreditDocument> CreditDocuments { get; set; }
        public DbSet<CreditDocumentType> CreditDocumentTypes { get; set; }
        public DbSet<CreditPartner> CreditPartners { get; set; }
        public DbSet<Redirect> Redirects { get; set; }
        public DbSet<CreditBill> CreditBills { get; set; }
        public DbSet<UiCircleComponent> UiCiricleComponents { get; set; }
        public DbSet<ProductSeri> ProductSeris { get; set; }
        public DbSet<PageSeo> PageSeos { get; set; }

        //Supplier
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierFactor> SupplierFactors { get; set; }
        public DbSet<SupplierFactorProduct> SupplierFactorProducts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        //
        public DbSet<ProductPricePage> ProductPricePages { get; set; }
        public DbSet<ColleaguePriceRange> ColleaguePriceRanges { get; set; }
        //
        public DbSet<SiteMenu> SiteMenus { get; set; }
        public DbSet<FilterPrice> FilterPrices { get; set; }
        public DbSet<FilterProperty> FilterProperties { get; set; }
        public DbSet<UserProductView> UserProductViews { get; set; }
        //
        public DbSet<SiteFotterMenu> SiteFotterMenus { get; set; }
        public DbSet<SiteFotterLink> SiteFotterLinks { get; set; }
        public DbSet<ProductOtherPage> ProductOtherPages { get; set; }
        public DbSet<SearchSuggestion> SearchSuggestions { get; set; }
        public DbSet<UserSearch> UserSearches { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PlusPriceRange> PlusPriceRanges { get; set; }
        public DbSet<ColleaguePlusPriceRange> ColleaguePlusPriceRanges { get; set; }
        public DbSet<Packing> Packings { get; set; }



        #endregion

        #region QuerModel
        public DbSet<HeaderSearchModel> HeaderSearches { get; set; }
        #endregion


        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            Expression<Func<BaseEntity, bool>> filterExpr = c => !c.IsDelete;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);
                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
            modelBuilder.AddRestrictDeleteBehaviorConvention();
        }

        #endregion


        public override int SaveChanges()
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
            return result;
        }
        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                try
                                {
                                    if ((property.CurrentValue != null && property.OriginalValue != null) && !property.OriginalValue.Equals(property.CurrentValue) && propertyName != "LastUpdateDate")
                                    {
                                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    }
                                }
                                catch (Exception e)
                                {
                                }


                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                var audit = auditEntry.ToAudit();

                // TO DO
                // Call Logger
                if (!string.IsNullOrEmpty(audit.NewValues) || !string.IsNullOrEmpty(audit.OldValues))
                    Audits.Add(audit);
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0) return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                // TO DO
                // Call Logger
                Audits.Add(auditEntry.ToAudit());
            }

            SaveChanges();
        }

    }
}