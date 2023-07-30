using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities
{
    public class SupplierFactor : BaseEntity
    {
        [Display(Name = "تامین کننده")]

        public long SupplierId { get; set; }
        [Display(Name = "تاریخ سفارش")]

        public DateTime? DateOrder { get; set; }
        [Display(Name = "تاریخ سفارش")]

        public string PrDateOrder { get; set; }
        [Display(Name = "تاریخ تحویل")]

        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "تاریخ تحویل")]

        public string PrDeliveryDate { get; set; }
        [Display(Name = "فاکتور")]

        public string FactorImg { get; set; }
        [Display(Name = "توضیحات")]

        public string Description { get; set; }
        [Display(Name = "شماره فاکتور")]

        public string FactorNumber { get; set; }
        [Display(Name = "کد تحویل پست")]

        public string PostCode { get; set; }
        [Display(Name = "جمع مبلغ")]

        public double SumPrice { get; set; }
        [Display(Name = "مبلغ پرداخت شده")]

        public double PaymentPrice { get; set; }
        [Display(Name = "وضعیت")]

        public EnumSupplierFactorStatus Status{ get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
        //
        public List<SupplierFactorProduct> SupplierFactorProducts { get; set; }

    }
}