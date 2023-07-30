using System;
using System.Collections.Generic;

namespace EShop.Core.ViewModels.Product
{
    public class ProductViewModel
    {
        
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string CategoryEnTitle { get; set; }
        public string CategoryFaTitle { get; set; }

        public long? CategoryId{ get; set; }
        public string BrandTitle { get; set; }
        public string BrandEnTitle { get; set; }
        public int BrandOrder { get; set; }

        public long? BrandId{ get; set; }
        public string ImgName { get; set; }
        public long ProductId { get; set; }
        public long? VariantId { get; set; }
        public int MainPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public int Percent { get; set; }
        public DateTime date { get; set; }

        public List<ProductPromotionIndexPropertyViewModel> Property { get; set; }
        public List<VariantColorViewModel> VariantColor { get; set; }


    } 
    public class ProductPromotionIndexViewModel
    {
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string CategoryTitle { get; set; }
        public string ImgName { get; set; }
        public long ProductId { get; set; }
        public int MainPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int Percent { get; set; }
        public DateTime date { get; set; }
        public List<ProductPromotionIndexPropertyViewModel> Property { get; set; }


    }
    public class VariantColorViewModel
    {
        public string Text { get; set; }
        public string Color { get; set; }
 


    }

    public class ProductPromotionIndexPropertyViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductPricesChartViewModel
    {
        public long ProductOptionId { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public string Date { get; set; }
        public string Seller { get; set; }
        public string Guarantee { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class ReturnProductPricesChartViewModel
    {
        public string Color { get; set; }
        public List<ProductPricesChartViewModel> productPrices{ get; set; }
    }

    public class HeaderSearchSimilarCategoryViewModel
    {
        public string Word { get; set; }
        public string CategoryName { get; set; }
    }

    public class SearchPageViewModel
    {
        public long ProductId { get; set; }
        public int Sell { get; set; }
        public int View { get; set; }
        public long CategoryId { get; set; }
        public string CategoryProduct { get; set; }
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string GuranateeTitle { get; set; }
        public int? MainPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public string Img { get; set; }
        public byte? PromotionType { get; set; }
    }
}
