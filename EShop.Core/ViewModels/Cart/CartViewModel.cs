using System.Collections.Generic;
using EShop.DataLayer.Entities.Promotion;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels.Cart
{
  public  class CartPageViewModel
    {
        public long CartId { get; set; }
        public long VariantId { get; set; }
        public long CartDetialId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string EnTitle { get; set; }
        public string Guarantee { get; set; }
        public string ProductOption { get; set; }
        public string ProductOptionValue { get; set; }
        public string ImgName { get; set; }
        public int CartCount { get; set; }
        public int CartPrice { get; set; }
        public string SellerName { get; set; }
        public int Weight { get; set; }
        public string CategoryName { get; set; }
        public int MainPrice { get; set; }
        public int SpecialPrice { get; set; }
        public int MaxOrderCount { get; set; }
        public byte PromotionType { get; set; }
        public int RemianingCount { get; set; }
        public bool IsActiveCart { get; set; }
    }

    public class CartPageEditViewModel
    {
        public long CartDetailId { get; set; }
        public long CartId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public long VariantId { get; set; }
        public bool IsActiveCart { get; set; }
        public int ChangeType { get; set; }
    }

    public class ShippingPageViewModel
    {
        public List<ShippingPageProduct>Products{ get; set; }
        public long? AddressId { get; set; }
        public long? ShippingId{ get; set; }
        public int ShippingPrice { get; set; }
        public int DiscountPrice { get; set; } 
        public long UserCreditPrice { get; set; } 
        public int CreditPrice { get; set; } 
        
    }  public class ShippingPageProduct
    {

        public string ProductName { get; set; }
        public string ImagName { get; set; }
        public string ProductOption { get; set; }
        public string ProductOptionValue { get; set; }
        public string Guarantee { get; set; }
        public int CartCount { get; set; }
        public int MainPrice { get; set; }
        public int SpecialPrice { get; set; }
        
    }

    public class CartDetailForDiscountViewModel
    {
        public int Count { get; set; }
        public int MainPrice { get; set; }
        public int SpecialPrice { get; set; }
        public List<long> CategoryId { get; set; }
    }

    public class CartDetailForSubmitViewModel
    {
        public string ProductSpecCode { get; set; }
        public long CartDetailId { get; set; }
        public long VariantId { get; set; }
        public int CartCount { get; set; }
        public int CartPrice { get; set; }
        public int Weight { get; set; }
        public int MainPrice { get; set; }
        public int SpecialPrice { get; set; }
        public int MaxOrderCount { get; set; }
        public int RemianingCount { get; set; }
        public int Dely { get; set; }
        public List<long> CategoryId { get; set; }
        public Variant variant { get; set; }
        public VariantPromotion VariantPromotion { get; set; }
        public byte PromotionType { get; set; }
    }
    public class OrderDetailForCrmViewModel
    {
        public string ProductSpecCode { get; set; }
        public string ProductColor { get; set; }
        public string ProductGaranty { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int SumPrice { get; set; }
        public int Discount { get; set; }
        public int SumPriceAfterDiscount { get; set; }
        public int UnitDiscount { get; set; }
        public bool StorePlace { get; set; }
        public byte DiscountType { get; set; }
 
        public long VariantId { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }

    }
    public class CheckOutPaymentVM
    {
        public long OrderId { get; set; }
        public long Price { get; set; }
        public long? SaleReferenceId { get; set; }
        public string PrDatePay { get; set; }
        public EnumPaymentStatus PaymentStatus { get; set; }

    }
}
