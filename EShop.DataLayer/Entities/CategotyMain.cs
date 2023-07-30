using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities
{
    public class CategotyMain : BaseEntity
    {
        public string Image { get; set; }
        public string OrginalImage { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string EnTitle { get; set; }
        public string Color { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public int Order { get; set; }
        public EnumTypeCategotyMain Type { get; set; }

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
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}