using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities
{
    public class WarehouseProduct : BaseEntity
    {
        [Display(Name = "انبار")]
        public long? WarehouseId { get; set; }
        [Display(Name = "محصول تامین")]
        public long SupplierFactorProductId { get; set; }
        [Display(Name = "کاربر تحویل گیرنده")]
        public long? DeliveryUserId { get; set; }
        [Display(Name = "کاربر ترخیص کننده")]
        public long? ClearanceUserId { get; set; }
        [Display(Name = "کاربر خریدار")]
        public long? BuyerUserId { get; set; }
        [Display(Name = "محصول سفارش")]
        public long? OrderDetailId { get; set; }
        
        [Display(Name = "کد افق")]
        public string Code { get; set; }
        [Display(Name = "IMEI")]
        public string IMEI { get; set; }
        [Display(Name = "تاریخ ورود")]
        public string DeliveryDate { get; set; }
        [Display(Name = "زمان ورود")]
        public string DeliveryTime { get; set; }
        [Display(Name = "تاریخ خروج")]
        public string ClearanceDate { get; set; }
        [Display(Name = "زمان خروج")]
        public string ClearanceTime { get; set; }
        [Display(Name = "مبلغ نهایی")]
        public decimal FinalPrice { get; set; }
        [Display(Name = "استفاده شده است ؟")]
        public bool IsUse { get; set; }

         


        [ForeignKey(nameof(OrderDetailId))]
        public Order.OrderDetail OrderDetail { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }

        [ForeignKey(nameof(SupplierFactorProductId))]
        public SupplierFactorProduct SupplierFactorProduct { get; set; }

        [ForeignKey(nameof(DeliveryUserId))]
        [InverseProperty("WarehouseProductsDeliveryUser")]
        public User.User DeliveryUser { get; set; }
        [ForeignKey(nameof(ClearanceUserId))]
        [InverseProperty("WarehouseProductsClearanceUser")]
        public User.User ClearanceUser { get; set; }
        [ForeignKey(nameof(BuyerUserId))]
        [InverseProperty("WarehouseProductsBuyerUser")]
        public User.User BuyerUser { get; set; }

        
    }
}