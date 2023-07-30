
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using System.ComponentModel.DataAnnotations;

namespace EShop.DataLayer.Entities.Seo
{
    public class Redirect : BaseEntity
    {
        [Display(Name = "آدرس مبداء")]
        [Required]
        public string OldUrl { get; set; }
        [Display(Name = "آدرس مقصد")]
        [Required]
        public string NewUrl { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
