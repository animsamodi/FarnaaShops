using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Core.ViewModels
{
    public class UserEditProfile
    {
        public long Id { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = "نام")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string Name { get; set; }
        [Display(Name = " نام خانوادگی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string Family { get; set; }
        [Display(Name = "رمز عبور جدید")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string NewPassword { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "لطفا {0} را با اعداد انگلیسی وارد کنید")]

        public string Phone { get; set; }


        [Display(Name = "کد ملی")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "لطفا {0} را با اعداد انگلیسی وارد کنید")]

        public string NatioalCode { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string Email { get; set; }
        public long? UserAddressId { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProvinceId { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CityId { get; set; }

 
        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string PostalAddress { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "لطفا {0} را با اعداد انگلیسی وارد کنید")]

        public string PostalCode { get; set; }

        //
        //
        [Display(Name = "شماره کارت")]
        public string CartNo { get; set; }
        [Display(Name = "شماره شبا")]
        public string ShebaNo { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string BDate { get; set; }
        //
        //

        #region Legal

        public long? UserLegalId { get; set; }

        [Display(Name = "نام شرکت")]
 
        public string CompanyName { get; set; }

        [Display(Name = "کد اقتصادی شرکت")]
         [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string CodeEghtesadi { get; set; }

        [Display(Name = "شماره ی ثبت شرکت")]
         [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string ShomareSabt { get; set; }

        [Display(Name = "کد نقش تاجر")]
         [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string CodeNaghshTajer { get; set; }

        [Display(Name = "تاریخ تاسیس شرکت")]
 
        public string TarikhTasis { get; set; }

 

        [Display(Name = "شناسه ی ملی شرکت")]
         [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string ShenaseMeli { get; set; }

        [Display(Name = "نوع شرکت")]
 
        public EnumNoeSherkat? NoeSherkat { get; set; }

        [Display(Name = "نوع مالکیت")]
 
        public EnumNoeMalekiyat? NoeMalekiyat { get; set; }

        [Display(Name = "نوع تملک محل فعالیت")]
 
        public EnumNoeTamalok? NoeTamalok { get; set; }
 


        [Display(Name = "توضیحات")]
 
        public string TozihatSherkat { get; set; }

        [Display(Name = "نشانی محل کار")]
 
        public string NeshaniMahaleKar { get; set; }

        [Display(Name = "کد پستی")]
         [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
        [Remote(action: "VerifyPostcode", controller: "Credit", "User")]

        public string CodePostiNeshaniMahaleKar { get; set; }
 
        [Display(Name = "تلفن ثابت به همراه کد")]
         [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]


        public string TelephoneSabet { get; set; }

        //
        // [Display(Name = "روزنامه رسمی")]
        //public string FileRuznameRasmi { get; set; }
        [Display(Name = "آگهی آخرین روزنامه")]
        public string FileAkharinTaghirat { get; set; }
        [Display(Name = "آگهی تاسیس شرکت ها")]
        public string FileSahebanEmza { get; set; }
        [Display(Name = "کپی سند مالکیت یا اجاره نامه رسمی")]
        public string FileAgahiTasis { get; set; }
        //
        [Display(Name = "کاربر حقوقی هستم")]

        public bool IsHoghughi { get; set; }

        //public IFormFile FormFileRuznameRasmi { get; set; }
        public IFormFile FormFileAkharinTaghirat { get; set; }
        public IFormFile FormFileSahebanEmza { get; set; }
        public IFormFile FormFileAgahiTasis { get; set; }



        //

        #endregion




    }
}