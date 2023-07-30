using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Credit
{
    public class CreditBill:BaseEntity
    {
         public long? CreditId { get; set; }
         public long UserId { get; set; }
        [Display(Name = "بانک")]
        public string Bank { get; set; }
        [Display(Name = "شعبه")]
        public string Shobe { get; set; }
        [Display(Name = "شماره حساب")]
        public string ShomareHesab { get; set; }
        [Display(Name = "شماره کارت")]
        public string ShomareKart { get; set; }
        [Display(Name = "مبلغ")]

        public int Price { get; set; }
        [Display(Name = "مبلغ تایید شده")]

        public int? ConfirmPrice { get; set; }
        [Display(Name = "تصویر فیش واریزی")]

        public string Image { get; set; }
        [Display(Name = "کد تراکنش")]

        public string Code { get; set; } 
        [Display(Name = "توضیحات")]

        public string Description { get; set; }
        [Display(Name = "پیام مدیر")]

        public string AdminMessage { get; set; }
        [Display(Name = "وضعیت")]


        public EnumCreditBillStatus Status { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime? DatePay { get; set; }
        [ForeignKey("CreditId")]
        public Credit Credit { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; }
    }
}