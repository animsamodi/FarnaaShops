using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Banner
{
  public  class BannerImage:BaseEntity
    {
 
        [Display(Name = "عنوان")]
        [MaxLength(100,ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشید")]
        public string Title { get; set; }
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشید")]
        public string ImageName { get; set; }
        public string OrginalImage { get; set; }

        [Display(Name = "لینک")]
        [MaxLength(250, ErrorMessage = "مقدار {0} نباید بیشتر از {1} باشید")]
        public string Link { get; set; }
        public byte Discount { get; set; }
        public int? Sort { get; set; }
        [MaxLength(10)]
        public string Color { get; set; }
        [Display(Name = "نوع بنر")]

        public EnumBannerType BannerType { get; set; }
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
}
