using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities
{
    public class SiteMenu : BaseEntity
    {


        [Display(Name = "کد")]
        public long Code { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }

        [Display(Name = "ترتیب ")]
        public int Sort { get; set; }

        [Display(Name = "ایکن")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Icon { get; set; }

        [NotMapped]
        public IFormFile IconImg { get; set; }

        [Display(Name = "نوع منو ")]
        public EnumTypeMenu Type { get; set; }
        public long? ParentCode { get; set; }

        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }

        [Display(Name = "سری")]
        public long? SeriId { get; set; }

        [Display(Name = "نگهداری برای تغییرات بعدی")]

        public bool SaveForNextChange { get; set; }

        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }

        //[ForeignKey("Code")]
        //[Display(Name = "والد ")]
        //[InverseProperty("SiteMenus")]
        //public SiteMenu SiteMenuParent { get; set; }

        //public List<SiteMenu> SiteMenus { get; set; }
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [ForeignKey("SeriId")]
        public ProductSeri ProductSeri { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
}