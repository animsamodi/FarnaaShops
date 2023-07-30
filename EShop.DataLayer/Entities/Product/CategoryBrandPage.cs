using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Entities.Product.FAQ;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Product
{
    public class CategoryBrandPage:BaseEntity
    {
 
        [Display(Name = "عنوان فارسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }
        
        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(300, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string EnTitle { get; set; }



        [Display(Name = "متن")]
        public string Text { get; set; }
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
        public string BaseSchema { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [InverseProperty("CategoryBrandPages")]
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [InverseProperty("CategoryBrandPages")]
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        
        [Display(Name = "اسکیما FAQ")]
        public string FAQSchema { get; set; }

        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
}
