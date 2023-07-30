using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Order
{
   public class PaymentDetail:BaseEntity
    {
 
        public long OrderId { get; set; }
        public EnumPaymentType Type { get; set; }
        public long Price { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
        [MaxLength(50)]
        public string RefId { get; set; }
        public long? SaleReferenceId { get; set; }
        public int? TypeBank { get; set; }
        public string BankCodeReturn { get; set; }
        public string CardHolderPAN { get; set; }
        public string BankCodeMessage { get; set; }
        public DateTime? DatePay { get; set; }
        public string PrDatePay { get; set; }
         public long? DiscountCodeId { get; set; }
        public long? GiftCartId { get; set; }
        public EnumPaymentStatus PaymentStatus { get; set; }
        public string Authority { get; set; }

        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("DiscountCodeId")]
        public DiscountCode DiscountCode { get; set; }

        [ForeignKey("GiftCartId")] 
        public GiftCard GiftCard{ get; set; }
    }
}
