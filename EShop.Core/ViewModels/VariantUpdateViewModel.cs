using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.User;

namespace EShop.Core.ViewModels
{
    public class VariantUpdateViewModel 
    {
        public long Id { get; set; }

        //Colleague

        [Display(Name = "قیمت همکار")]
        public int PriceColleague { get; set; }
        [Display(Name = "موجودی همکار")]
        public int CountColleague { get; set; }
        [Display(Name = "حداکثر تعداد خرید همکار")]
        public int MaxOrderCountColleague { get; set; }

        //Colleague
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
        [Display(Name = "قیمت")]
        public int Price { get; set; }
        [Display(Name = "قیمت با تخفیف")]
        public int SepcialPrice { get; set; }
        [Display(Name = "موجودی")]
        public int Count { get; set; }
         
        [Display(Name = "حداکثر تعداد خرید")]
        public int MaxOrderCount { get; set; }
        public int? ChangePrice{ get; set; }
        public int IncreaseCount { get; set; }
        public int DecreaseCount { get; set; }
        public long? ChangeUserId { get; set; }

        [Display(Name = "قیمت همکار از مشتری حساب شود ؟")]
        public bool GetColleaguePriceFromOrginal { get; set; }


    }
}