using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Promotion;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class VariantsForProductDetailViewModel
    {
        public long VariantId { get; set; }
        [Display(Name = "قیمت")]

        public int MainPrice { get; set; }
        [Display(Name = "قیمت با تخفیف")]

        public int SellerPrice { get; set; }
        [Display(Name = "موجودی")]

        public int Count { get; set; }
        public int ShopCount { get; set; }
        public int VoteCount { get; set; }
        public string PurchaseConsentPercent { get; set; }
        public byte TotallySatisfied { get; set; }
        public byte Satisfied { get; set; }
        public byte Neutral { get; set; }
        public int DisSatisfied { get; set; }
        public int TotallyDisSatisfied { get; set; }
        public long SellerId { get; set; }
        [Display(Name = "گارانتی")]

        public string Guarantee { get; set; }
        [Display(Name = "آپشن")]
        public int MaxOrderCount { get; set; }
        public long ProductOptionId { get; set; }
        public string ProductOption { get; set; }
        public string ProductOptionCode { get; set; }
        public string? ProductOptionCodeTwo { get; set; }

        public byte? DiscountType { get; set; }
        public DateTime? DiscountEndDate { get; set; }
    }
    public class UserOrder
    {
        public long? OrderId { get; set; }
        public long? PaymentDetailId { get; set; }
        public long? UserId { get; set; }
        //
         public string ClientName { get; set; }
         public string ClientTel { get; set; }
         public string ClientNatioalCode { get; set; }

         public string ClientAddress { get; set; }
         public string ClientPostalCode { get; set; }
        //
        public string RecipientName { get; set; }
        [MaxLength(11)]
        public string RecipientTel { get; set; }
        [MaxLength(150)]
        public string RecipientAddress { get; set; }
        [MaxLength(10)]
        public string RecipientPostalCode { get; set; }
        public int? ShipmentPrice { get; set; }
        public string ShipmentTitle { get; set; }
        public long SumAmount { get; set; }
        public long AmountPayable { get; set; }
        public long? Discount { get; set; }
        public EnumOrderStatus? OrderStatus { get; set; }
        public string TrackingCodePost { get; set; }
        public string PrDatePay { get; set; }
        public DateTime? DatePay { get; set; }
        public EnumPaymentStatus? PaymentStatus { get; set; }
        public long? SaleReferenceId { get; set; }
        public List<UserOrderDetail> OrderDetails { get; set; }

    }
    public class UserOrderAdmin
    {
        public long? OrderId { get; set; }
        public long? PaymentDetailId { get; set; }
        public long? UserId { get; set; }

        public string ClientName { get; set; }
        public string ClientTel { get; set; }
        public string ClientNatioalCode { get; set; }
        public string RecipientName { get; set; }
        [MaxLength(11)]
        public string RecipientTel { get; set; }
        [MaxLength(150)]
        public string RecipientAddress { get; set; }
        [MaxLength(10)]
        public string RecipientPostalCode { get; set; }
        public int? ShipmentPrice { get; set; }
        public string ShipmentTitle { get; set; }
        public long SumAmount { get; set; }
        public long AmountPayable { get; set; }
        public long? Discount { get; set; }
        public EnumOrderStatus? OrderStatus { get; set; }
        public string TrackingCodePost { get; set; }
        public string PrDate { get; set; }
        public string PrDatePay { get; set; }
        public DateTime? DatePay { get; set; }
        public DateTime? CreateDate { get; set; }
        public long PCreateDate { get; set; }
        public EnumPaymentStatus? PaymentStatus { get; set; }
        public string RefId { get; set; }
        public long? SaleReferenceId { get; set; }
        public string BankCodeReturn { get; set; }
        public string CardHolderPAN { get; set; }
        public string BankCodeMessage { get; set; }
        public string FactorNoTadbir { get; set; }
        public int CrmValidationItem { get; set; }

        public bool IsSendToTadbir { get; set; }
        //Delivery InPersonDelivery
        public string InPersonCode { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string PrDeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public bool NeedUseDeliveryCode { get; set; }
        //Delivery
        public bool IsColleauge { get; set; }
        public bool IsColleaugeOrder { get; set; }

        public List<UserOrderDetail> OrderDetails { get; set; }
        [Display(Name = "روش پرداخت")]

        public EnumPaymentTypeColleaugeCart TypePayment { get; set; }

    }
    public class FactorPostModel
    {
        public string PostCode { get; set; }
        public string FactorNo { get; set; }
    }
    public class PreFactorFactorModel
    {
        public string PreFactorNo { get; set; }
        public string FactorNo { get; set; }
    }
    public class ConvertPreFactorToFactorViewModel
    {
        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime EndDate { get; set; }
        [Display(Name = "فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile File { get; set; }


        public List<PreFactorFactorModel> PreFactorFactors { get; set; }
    }
    public class ChangeOrderStateViewModel
    {
        [Display(Name = "روش ارسال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public long ShipmentId { get; set; }
        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime EndDate { get; set; }

        [Display(Name = "تغییر وضعیت به")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public EnumOrderStatus OrderStatus { get; set; }
        [Display(Name = "فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile File { get; set; }


        public List<string> FactorNo { get; set; }
    }
    public class PostCodeViewModel
    {

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public DateTime EndDate { get; set; }


        [Display(Name = "فایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile File { get; set; }


        public List<FactorPostModel> FactorPostModels { get; set; }
    }

    public class UserPaymentSearchAdmin
    {
        public long? OrderId { get; set; }
        public string RefId { get; set; }
 
        public string PrDatePay { get; set; }
        public string PSDate { get; set; }
        public string PEDate { get; set; }
 
        public int? PageNumber { get; set; }
          
    }
    public class UserOrderSearchAdmin
    {
        public long? OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientTel { get; set; }
        public string ClientNatioalCode { get; set; }

        public EnumOrderStatus? OrderStatus { get; set; }
        public EnumYesNo? SendTadbir { get; set; }
        public EnumTypeSystem TypeSystem { get; set; }
        public string PrDatePay { get; set; }
        public string PSDate { get; set; }
        public string PEDate { get; set; }
 
        public int? PageNumber { get; set; }
        public string Shipment { get; set; }
        //
        public bool? Delivered { get; set; }

        public bool? WaitConfairmAnbar { get; set; }
        public bool? ConfairmAnbar { get; set; }
        public bool? IsColleaugeOrder { get; set; }

    }

    public class UserOrderDetail
    {
        public long? OrderId { get; set; }
        public long? VariantId { get; set; }
        public long? ProductId { get; set; }

        public int? Count { get; set; }
        public int? Price { get; set; }
        public int? SumPrice { get; set; }
        public int? Discount { get; set; }
        public int? SumPriceAfterDiscount { get; set; }
        public string Color { get; set; }
        public string ColorValue { get; set; }
        public string Guarantee { get; set; }
        public string Image { get; set; }
        public string FaTitle { get; set; }
 

    }
 
}
