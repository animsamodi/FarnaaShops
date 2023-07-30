using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Order
{
    public class Shipment : BaseEntity
    {
        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "عنوان برای Crm")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TitleCrm { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public DateTime? SendDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "مبلغ ارسال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "مبلغ به ازای هر محصول اضافه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PricePerAddProduct { get; set; }
        [Display(Name = "استان")]

        public long? ProvinceId { get; set; }
        [Display(Name = "شهر")]

        public long? CityId { get; set; }
        [Display(Name = "چک کردن پیش فروش")]

        public bool CheckPreSell { get; set; }
        [Display(Name = "نیاز به تولید کد تحویل میباشد ؟")]

        public bool NeedGenerateDeliveryCode { get; set; }

        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeShipment Type { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public List<Cart.Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }

    }
}
