using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels
{
    public class VariantPromotionsViewModel
    {
        [Display(Name = "کد رایانه")]
        public long VariantPromotionId { get; set; }

        [Display(Name = "مبلغ با تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Price { get; set; }
        [Display(Name = "درصد تخفیف")]
        public byte Percent { get; set; }
        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int Count { get; set; }
        [Display(Name = "حداکثر تعداد سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public int? MaxOrderCount { get; set; }
        [Display(Name = "تعداد باقیمانده")]
        public int ReminaingCount { get; set; }
        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime EndDate { get; set; }
        public long VariantId { get; set; }


    }
}