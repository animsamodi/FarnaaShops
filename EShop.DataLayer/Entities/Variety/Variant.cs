using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Cart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Entities.Promotion;

namespace EShop.DataLayer.Entities.Variety
{
    public class Variant : BaseEntity
    {
        //Colleague

        [Display(Name = "قیمت همکار")]
        public int PriceColleague { get; set; }
        [Display(Name = "موجودی همکار")]
        public int CountColleague { get; set; }
        [Display(Name = "حداکثر تعداد خرید همکار")]
        public int MaxOrderCountColleague { get; set; }
        [Display(Name = "قیمت همکار از مشتری حساب شود ؟")]
        public bool GetColleaguePriceFromOrginal { get; set; }

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
        public int ShopCount { get; set; }
        [Display(Name = "حداکثر تعداد خرید")]
        public int MaxOrderCount { get; set; }

        public int VoteCount { get; set; }
        public string PurchaseConsentPercent { get; set; }

        public byte TotallySatisfied { get; set; }
        public byte Satisfied { get; set; }
        public byte Neutral { get; set; }
        public int DisSatisfied { get; set; }
        public int TotallyDisSatisfied { get; set; }
        public int ReserveCount { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Display(Name = "تاریخ فروش")]

        public DateTime? DateSell { get; set; }

        [Display(Name = "آپشن")]
        public long ProductOptionId { get; set; }
        [Display(Name = "گارانتی")]
        public long GuaranteeId { get; set; }
        public long SellerId { get; set; }
        public long ProductId { get; set; }

        public bool SellingColleauge { get; set; }
        [ForeignKey("ProductOptionId")]
        public ProductOption productOption { get; set; }
        [ForeignKey("GuaranteeId")]
        public Guarantee Guarantee { get; set; }
        [ForeignKey("SellerId")]
        public Seller.Seller Seller { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product Product { get; set; }

        public List<ProductPrice> ProductPrices { get; set; }
        public List<Cart.Cart> Carts { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<VariantPromotion> VariantPromotions { get; set; }
        public List<SaleTransaction> SaleTransactions { get; set; }
        public List<DiscountCode> DiscountCodes { get; set; }
        public List<SupplierFactorProduct> SupplierFactorProducts { get; set; }

    }
}
