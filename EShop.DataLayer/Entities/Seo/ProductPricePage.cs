using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Seri
{
    public class ProductPricePage : BaseEntity
    {
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]

        public string EnTitle { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [Display(Name = "سری")]

        public long? SeriId { get; set; }
        [Display(Name = "متن")]

        public string Text { get; set; }
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
        [Display(Name = "لینک")]

        public string Url { get; set; }
        [Display(Name = "ترتیب")]

        public EnumProductPricePageOrder Order { get; set; }
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

        [Display(Name = "اسکیما")]
        public string Schema { get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [ForeignKey("SeriId")]
        public ProductSeri ProductSeri { get; set; }

        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }
    }
}