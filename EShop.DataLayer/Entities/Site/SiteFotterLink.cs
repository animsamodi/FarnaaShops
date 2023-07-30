using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities.Site
{
    public class SiteFotterLink : BaseEntity
    {
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeFotterLink Type { get; set; }
        [Display(Name = "تصویر")]
         public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageImg { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "فونت ایکن")]
         public string FontIcon { get; set; }
        [Display(Name = "عنوان انگلیسی")]
         public string EnTitle { get; set; }
        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Order { get; set; }
        [Display(Name = "لینک")]
         public string Url { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}