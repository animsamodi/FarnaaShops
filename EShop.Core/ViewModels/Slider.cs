using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
   public class CreateSliderViewModel
    {

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "عکس دسکتاپ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile DesktopImg { get; set; }
        [Display(Name = "عکس موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile MobileImg { get; set; }

    

    

        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Sort { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public EnumTypeSlider Type { get; set; }
         [Display(Name = "نوع سیستم")]
         [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public EnumTypeSystem TypeSystem { get; set; }
    }

    public class EditSliderViewModel
    {
        public long SliderId { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Description { get; set; }

      
        [Display(Name = "عکس دسکتاپ")]
        public IFormFile DesktopImg { get; set; }
        [Display(Name = "عکس موبایل")]
        public IFormFile MobileImg { get; set; }
 

        public string CurrentImgName { get; set; }
        public string CurrentImgNameMobile { get; set; }


        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Sort { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public EnumTypeSlider Type { get; set; }
         [Display(Name = "نوع سیستم")]
         [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public EnumTypeSystem TypeSystem { get; set; }
    }
}
