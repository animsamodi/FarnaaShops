using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class ReportOrder
    {

        [Display(Name = "شماره سفارش")]
        public long Id { get; set; }


        [Display(Name = "تاریخ سفارش")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public string PrDate { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public string PrDateDisplay { get; set; }

        [Display(Name = "نام مشتری")]
        public string ClientName { get; set; }
        [Display(Name = "تلفن مشتری")]

        public string ClientTel { get; set; }
        [Display(Name = "کد ملی مشتری")]

        public string ClientNatioalCode { get; set; }

        [Display(Name = "آدرس مشتری")]
        public string ClientAddress { get; set; }

        [Display(Name = "کد پستی مشتری")]
        public string ClientPostalCode { get; set; }
        [Display(Name = "نام تحویل گیرنده")]

        public string RecipientName { get; set; }
        [Display(Name = "تلفن تحویل گیرنده")]

        public string RecipientTel { get; set; }
        [Display(Name = "آدرس تحویل گیرنده")]

        public string RecipientAddress { get; set; }
        [Display(Name = "کد پستی تحویل گیرنده")]

        public string RecipientPostalCode { get; set; }
        [Display(Name = "مبلغ پرداخت شده")]

        public long AmountPayable { get; set; }
        [Display(Name = "حمل و نقل")]

        public string ShipmentTitle { get; set; }
        [Display(Name = "هزینه حمل و نقل")]

        public int ShipmentPrice { get; set; }
        [Display(Name = "جمع مبلغ")]

        public long SumAmount { get; set; }
        [Display(Name = "تعداد محصول")]

        public int Count { get; set; }
        [Display(Name = "قیمت واحد")]

        public int Price { get; set; }
        [Display(Name = "جمع مبلغ سطر")]

        public int SumPrice { get; set; }
        [Display(Name = "نام محصول")]

        public string FaTitle { get; set; }
        [Display(Name = "گارانتی")]

        public string Guarantee { get; set; }
        [Display(Name = "رنگ")]

        public string Color { get; set; }
        [Display(Name = "روش پرداخت")]

        public string PaymentMethod { get; set; }
        [Display(Name = "کد تراکنش")]

        public long? SaleReferenceId { get; set; }


    }


}
