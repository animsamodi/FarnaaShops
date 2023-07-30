using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Api;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Enum;
using EShop.DataLayer.QueryModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        ProductDetailUserViewModel GetProductDetailUser(long id, long userId);
        Product GetProductNameWithId(long id);
        ProductReviewViewModel GetProductReview(long productId, EnumTypeSystem typeSystem =EnumTypeSystem.Farnaa);
        List<ProductPropertyUserViewModel> GetProperty(long productId);
        Tuple<int, List<ProductListViewModel>> GetProductsForAdmin(string searchtext, int pagenumber, int brnad, int category, int state, int take);
        Tuple<int, List<ProductListViewModel>> GetProductsForAdminColleague(string searchtext, int pagenumber, int brnad, int category, int state, int take);
        Tuple<int, List<ProductListViewModel>> GetProductsForAdminPlus(string searchtext, int pagenumber, int brnad, int category, int state, int take);
        Tuple<int, List<SearchPageViewModel>> GetProductsForCategory(int pagenumber, int brnad, int category, int take);
        long AddProduct(Product product);
        long GetProductCategoryId(long id);
        List<CompareViewModel> GetProductForCompare(List<long?> idlist);
        List<CompareProductViewModel> GetProductForCompare(long catid);
        List<CompareProductViewModel> GetProductForCompareByBrandId(long brandid, string Name, long catid);
        List<CompareProductViewModel> GetProductForCompareByName(string Name, long catid);
        List<ProductPromotionIndexViewModel> GetProductPromotionForIndex();
        List<ProductPricesChartViewModel> GetProductPricesForChart(long productid);
        ProductPricesChartViewModel GetProductPriceForChartByProductOptionId(long productid, long id);
        List<HeaderSearchModel> HeaderSearch(string text);

        public ProductSearchWithFilterViewModel SearchPage(SearchWithFilterDto filter, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);

        public ProductSearchWithFilterViewModel SearchInProductsWithFilterInBrand(FilterDto dto, long brandId);
        public ProductSearchWithFilterViewModel SearchInProductsWithFilterInCategory(FilterDto dto, long categoryId,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);

        List<SearchPageViewModel> GetProductSuggest();
        List<ProductViewModel> GetProductNew();
        List<ProductViewModel> GetProductBestView();
        List<ProductViewModel> GetProductBestSelling(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<ProductViewModel> GetListRelatedProduct(long id, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        Product FindProductById(long id);
        bool UpdateProduct(Product product);
        List<Product> GetAllProduct();
        List<RelatedProductForAdmin> GetRelatedProductForAdmin(long productId);
        List<RelatedProductForAdmin> GetAccessoriesProductForAdmin(long productId);
        bool ChangeRelatedProduct(long productId, List<long> relatedProducts);
        bool ChangeAccessoriesProduct(long productId, List<long> relatedProducts);
        List<ProductViewModel> GetListSimilarProduct(long id, EnumTypeSystem typeSystem=EnumTypeSystem.Farnaa);
        bool DeleteProduct(long id);
        int CountUseCategoryInProducts(int catId);
        void AddProductView(long id);
        void AddProductSell(long id);
        List<string> HeaderSearch2(string text);
        List<ProductViewModel> GetListProductAccessories(long id);
        Task<List<Product>> GetByCategoryIdAndBrandId(long categoryId, long brandId);
        List<ProductViewModel> GetProductListPrice(long? categoryId, long? brandId, long? seriId, EnumProductPricePageOrder order);
        List<ProductViewModel> GetListAvailableSoonProducts();
        List<ProductViewModel> GetListDiscountProducts(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<Product> GetAllProductByCategory();
        List<ProductApiViewModel> GetListProductForApi(FilterProductApiViewModel filter);
        List<ProductViewModel> GetColleaugeSpecialSaleProduct();
    }
}
