using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels
{
    public class TopHeaderViewModel 
    {
        public long Id { get; set; }
        [Display(Name = "تصویر بالای سایت وب")]
        public string TopImageBannerWeb { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت وب")]

        public string TopImageBannerWebTitle { get; set; }
 
        [Display(Name = "تصویر بالا ی سایت موبایل")]

        public string TopImageBannerMobile { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileTitle { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت وب")]

        public bool ShowTopImageBannerWeb { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت موبایل")]

        public bool ShowTopImageBannerMobile { get; set; }
        [Display(Name = "لینک تصویر بالای سایت وب")]

        public string TopImageBannerWebUrl { get; set; }
        [Display(Name = "لینک تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileUrl { get; set; }




    }
}