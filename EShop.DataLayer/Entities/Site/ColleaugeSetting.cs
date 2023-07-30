using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Site
{
    public class ColleaugeSetting : BaseEntity
    {
        
        public string TopImageBannerWeb { get; set; }
        public string TopImageBannerWebUrl { get; set; }
        public string TopImageBannerMobileUrl { get; set; }
        public string TopImageBannerWebTitle { get; set; }
        public string TopImageBannerMobile { get; set; }
        public string TopImageBannerMobileTitle { get; set; }
        public bool ShowTopImageBannerWeb { get; set; }
        public bool ShowTopImageBannerMobile { get; set; }
        public string StartTime { get; set; }
        public string EndTime{ get; set; }
        public bool IsActive{ get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }


    }


}