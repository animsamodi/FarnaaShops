using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditHaghighiViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Display(Name = "نوع")]

        public EnumRealLegal RealLegal { get; set; }
        //مشخصات فردی (صاحب جواز کسب)

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]


        public string Family { get; set; }

        [Display(Name = "شماره ی ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        [Remote(action: "VerifyNationalNumber", controller: "Credit", "User")]

        public string NationalNumber { get; set; }
        [Display(Name = "شماره ی شناسنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string ShomareShenasname { get; set; }

        [Display(Name = "رایانامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا {0} را به درستی وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Remote(action: "VerifyBDate", controller: "Credit", "User")]

        public string BDate { get; set; }



        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Father { get; set; }

        [Display(Name = "محل صدور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string PlaceIssue { get; set; }

        //مشخصات فروشگاه


        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string CompanyName { get; set; }

        [Display(Name = "شماره ی پروانه ی کسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string CodeEghtesadi { get; set; }

        [Display(Name = "رسته شغلی پروانه کسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string ShomareSabt { get; set; }

        [Display(Name = "کد نقش تاجر در سامانه ی جامع تجارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string CodeNaghshTajer { get; set; }

        [Display(Name = "تاریخ صدور پروانه ی کسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string TarikhTasis { get; set; }

        #region Haghighi
        [Display(Name = "تاریخ انقضای پروانه کسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string TarikhEngheza { get; set; }

        [Display(Name = "وضعیت محل فعالیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumVaziyatMahal VaziyatMahal { get; set; }

        [Display(Name = "نوع تملک محل فعالیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumNoeTamalokMahal NoeTamalokMahal { get; set; }
        #endregion

         


        [Display(Name = "توضیحات")]
 
        public string TozihatSherkat { get; set; }

        [Display(Name = "نشانی محل کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string NeshaniMahaleKar { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
        [Remote(action: "VerifyPostcode", controller: "Credit", "User")]

        public string CodePostiNeshaniMahaleKar { get; set; }

        //[Display(Name = "نشانی محل ارسال بار و مدارک")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        //public string NeshaniMahaleErsal { get; set; }

        //[Display(Name = "کد پستی")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        //[MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]

        //public string CodePostiNeshaniMahaleErsal { get; set; }

        [Display(Name = "تلفن ثابت به همراه کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]

        public string TelephoneSabet { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]

        public string Mobile { get; set; }

        //[Display(Name = "فکس")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        //public string Fax { get; set; }
        //مشخصات شرکا / سهامداران شرکت که نام شان در جواز کسب درج شده است

         


        //اطلاعات جهت ایجاد اعتبار
        [Display(Name = "نوع خرید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public EnumNoeKharid NoeKharid { get; set; }

        //برای ایجاد اعتبار چه تضمینی در اختیار شرکت قرار می‌دهید؟
        [Display(Name = "ضمانت‌نامه بانکی به مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double? ZemanatBankiMablagh { get; set; }
        [Display(Name = "وثیقه ی ملکی به ارزش تقریبی")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double? VasigheMelkiMablagh { get; set; }
        //مودی در سازمان امور مالیاتی شامل کدامیک از موارد زیر است؟
         //[Display(Name = "مودی در سازمان امور مالیاتی")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]  
         //public EnumMudiMaliyati MudiMaliyati { get; set; }
        //
        [Display(Name = "پیغام مدیریت")]
        public string AdminMessage { get; set; }
        [Display(Name = "پیغام کاربر")]

        public string UserMessage { get; set; }
        [Display(Name = "وضعیت")]

        public EnumCreditStatus CreditStatus { get; set; }

        [Display(Name = "مبلغ تایید شده")]
        public int AcceptPrice { get; set; }
        [Display(Name = "تاریخ اعتبار")]

        public DateTime? CreditExpDate { get; set; }

        //
        public List<CreditAccountViewModel> Accounts { get; set; }
        public List<CreditDocumentViewModel> Documents { get; set; }
        public List<CreditPartnerViewModel> Partners { get; set; }
    }
}