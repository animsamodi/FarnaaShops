using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Seri
{
    public class ProductSeri : BaseEntity
    {
        [Display(Name = "تصویر")]

        public string Image { get; set; }
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]

        public string EnTitle { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
        [Display(Name = "متا Title")]
        public string MetaTitle { get; set; }
        [Display(Name = "متا Description")]
        public string MetaDescription { get; set; }
        [Display(Name = "متا Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Canonical")]
        public string Canonical { get; set; }
        [Display(Name = "تگ های هدر")]
        public string HeaderTag { get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }

        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }

        public List<PageSeo> PageSeos { get; set; }
        public List<Product.Product> Products { get; set; }
        public List<ColleaguePriceRange> ColleaguePriceRanges { get; set; }
        public List<SiteMenu> SiteMenus { get; set; }
        public List<FilterPrice> FilterPrices { get; set; }
        public List<FilterProperty> FilterProperties { get; set; }
        public List<PlusPriceRange> PlusPriceRanges { get; set; }
        public List<ColleaguePlusPriceRange> ColleaguePlusPriceRanges { get; set; }


    }
}