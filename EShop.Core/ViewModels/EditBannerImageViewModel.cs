using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class EditBannerImageViewModel
    {
        public long Id { get; set; }
        [Display(Name = "عنوان")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشید")]
        public string Title { get; set; }


        [Display(Name = "عکس")]
        public IFormFile Img { get; set; }
 

        public string CurrentImgName { get; set; }

 

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }

 
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
}