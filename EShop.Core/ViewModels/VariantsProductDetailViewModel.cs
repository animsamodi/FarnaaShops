using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels
{
    public class VariantsProductDetailViewModel
    {
        [Display(Name = "کد رایانه")]
        public long VariantId { get; set; }
        [Display(Name = "قیمت")]

        public int MainPrice { get; set; }
        [Display(Name = "قیمت با تخفیف")]

        public int SellerPrice { get; set; }
        [Display(Name = "تعداد فروش رفته")]
        public int SellCount { get; set; }
        [Display(Name = "موجودی")]
        public int Count { get; set; }
        public long ProductId { get; set; }
        public long SellerId { get; set; }
        [Display(Name = "گارانتی")]
        public string Guarantee { get; set; }
        [Display(Name = "آپشن")]
        public string ProductOption { get; set; }
        [Display(Name = "عنوان فارسی محصول")]
        public string ProductFaTitle { get; set; }
        [Display(Name = "عنوان لاتین محصول")]
        public string ProductEnTitle { get; set; }
        public string ProductOptionCode { get; set; }
        [Display(Name = "تصویر")]

        public string Image { get; set; }

        [Display(Name = "حداکثر تعداد خرید")]
        public int? MaxOrderCount { get; set; }
        [Display(Name = "تاریخ فروش")]

        public DateTime? DateSell { get; set; }
        public string DateSellString { get; set; }
        //
        [Display(Name = "قیمت همکار")]
        public int PriceColleague { get; set; }
        [Display(Name = "موجودی همکار")]
        public int CountColleague { get; set; }
        [Display(Name = "حداکثر تعداد خرید همکار")]
        public int? MaxOrderCountColleague { get; set; }
        [Display(Name = "قیمت همکار از مشتری حساب شود ؟")]
        public bool GetColleaguePriceFromOrginal { get; set; }
        //
        //Plus

        [Display(Name = "قیمت پلاس")]
        public int PricePlus { get; set; }
        [Display(Name = "قیمت با تخفیف پلاس")]
        public int SepcialPlusPrice { get; set; }
        [Display(Name = "موجودی پلاس")]
        public int CountPlus { get; set; }
        [Display(Name = "حداکثر تعداد خرید پلاس")]
        public int MaxOrderCountPlus { get; set; }
        [Display(Name = "قیمت پلاس از مشتری حساب شود ؟")]
        public bool GetPlusPriceFromOrginal { get; set; }

        //Plus
        //Colleague Plus

        [Display(Name = "قیمت پلاس همکار")]
        public int PriceColleaguePlus { get; set; }
        [Display(Name = "موجودی پلاس همکار")]
        public int CountColleaguePlus { get; set; }
        [Display(Name = "حداکثر تعداد خرید پلاس همکار")]
        public int MaxOrderCountColleaguePlus { get; set; }
        [Display(Name = "قیمت پلاس همکار از مشتری حساب شود ؟")]
        public bool GetColleaguePricePlusFromOrginal { get; set; }

        //Colleague Plus

    }
}