using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Order
{
    public class Order : BaseEntity
    {

        [MaxLength(100)]
        [Display(Name = "مشتری")]
                public string ClientName { get; set; }
        [MaxLength(11)]
        [Display(Name = "تلفن مشتری")]

        public string ClientTel { get; set; }
        [MaxLength(150)]
        [Display(Name = "کد ملی مشتری")]

        public string ClientNatioalCode { get; set; }

        [MaxLength(500)]
        [Display(Name = "آدرس مشتری")]

        public string ClientAddress { get; set; }
        [MaxLength(10)]
        [Display(Name = "کد پستی مشتری")]

        public string ClientPostalCode { get; set; }
        [MaxLength(100)]
        [Display(Name = "تحویل گیرنده")]

        public string RecipientName { get; set; }
        [MaxLength(11)]
        [Display(Name = "تلفن تحویل کیرنده")]

        public string RecipientTel { get; set; }
        [MaxLength(500)]
        [Display(Name = "آدرس تحویل کیرنده")]

        public string RecipientAddress { get; set; }
        [MaxLength(10)]
        [Display(Name = "کد پستی تحویل گیرنده")]

        public string RecipientPostalCode { get; set; }
        public string GiftWrappingText { get; set; }

        public int GiftWrappingPrice { get; set; }
        [Display(Name = "روش ارسال")]

        public string ShipmentTitle { get; set; }
        [Display(Name = "مبلغ ارسال")]

        public int ShipmentPrice { get; set; }
        [Display(Name = "جمع مبلغ")]

        public long SumAmount { get; set; }
        [Display(Name = "مبلغ پرداخت شده")]

        public long AmountPayable { get; set; }
        [Display(Name = "کد Crm")]

        public int CrmValidationItem { get; set; }
        [Display(Name = "کد کاربر")]

        public long UserId { get; set; }
        [Display(Name = "کد ارسال تدبیر")]

        public long CrmSendCode { get; set; }
        [Display(Name = "وضعیت")]

        public EnumOrderStatus OrderStatus { get; set; }
        [Display(Name = "کد بیجک پستی")]

        public string TrackingCodePost { get; set; }
        [Display(Name = "شماره فاکتور تدبیر")]

        public string FactorNoTadbir { get; set; }
        public bool IsSendToTadbir { get; set; }
        public bool IsCheckPayState { get; set; }
        public bool IsColleauge{ get; set; }
        public bool IsColleaugeOrder{ get; set; }
        public long? ShipmentId { get; set; }
        //Delivery InPersonDelivery
        public string InPersonCode { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string PrDeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public bool NeedUseDeliveryCode { get; set; }

        //Delivery

        //Packing
        public long? PackingId { get; set; }
        [Display(Name = "روش بسته بندی")]

        public string PackingTitle { get; set; }
        [Display(Name = "مبلغ بسته بندی")]

        public int PackingPrice { get; set; }
        //

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        [ForeignKey(nameof(PackingId))]
        public Packing Packing { get; set; }
        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
        public List<GiftCardTransaction> GiftCardTransactions { get; set; }
        public List<PaymentDetail> PaymentDetails { get; set; }
        public List<SaleTransaction> SaleTransactions { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
