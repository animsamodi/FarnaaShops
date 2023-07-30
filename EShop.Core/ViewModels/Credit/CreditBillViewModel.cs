using System;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditBillViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public long? CreditId { get; set; }        [Display(Name = "کد رایانه کاربر")]

        public long UserId { get; set; }
        [Display(Name = "بانک")]
        public string Bank { get; set; }
        [Display(Name = "شعبه")]        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Shobe { get; set; }
        [Display(Name = "شماره حساب")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public string ShomareHesab { get; set; }
        [Display(Name = "شماره کارت")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public string ShomareKart { get; set; }
        [Display(Name = "مبلغ")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "مبلغ تایید شده")]

        public int? ConfirmPrice { get; set; }
        [Display(Name = "تصویر فیش واریزی")]

        public string Image { get; set; }
        public IFormFile FormFile { get; set; }

        [Display(Name = "کد تراکنش")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public string Code { get; set; }
        [Display(Name = "توضیحات")]

        public string Description { get; set; }
        [Display(Name = "پیام مدیر")]

        public string AdminMessage { get; set; }
        [Display(Name = "وضعیت")]


        public EnumCreditBillStatus Status { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        public DateTime? DatePay { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string PrDatePay { get; set; }

        //user

        [Display(Name = "نام و نام خانوادگی")]
         public string FullName { get; set; }
         [Display(Name = "شماره تلفن")]
          public string Phone { get; set; }
         [Display(Name = "کد ملی")]
          public string NatioalCode { get; set; }
    }
}