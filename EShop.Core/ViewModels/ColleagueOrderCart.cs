using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
    public class ColleagueOrderCart
    {

        [Display(Name = "همکار")]

        public long UserId { get; set; }
        [Display(Name = "اعتبار")]

        public int? Credit { get; set; }
        [Display(Name = "آدرس")]

        public long AddressId { get; set; }
        [Display(Name = "روش ارسال")]

        public long ShippingId { get; set; }
        [Display(Name = "درصد بیمه")]

        public int insurance { get; set; }
        [Display(Name = "روش پرداخت")]

        public EnumPaymentTypeColleaugeCart Type { get; set; }

        public List<ColleagueOrderCartDetail> Details { get; set; }

    }
}