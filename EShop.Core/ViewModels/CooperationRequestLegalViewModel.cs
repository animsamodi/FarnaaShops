using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class CooperationRequestLegalViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public EnumRealLegal Type { get; set; }
        //
        [Display(Name = "نام شرکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string Name { get; set; }//Name Sherkat
 
        [Display(Name = "شناسه ملی شرکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public string CodeMeli { get; set; }//Code eghtesadi

        [Display(Name = "کد نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        public string CodeNaghsh { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا {0} را به درستی وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProvinceId { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CityId { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
        public string CodePosti { get; set; }
        [Display(Name = "توضیحات")]
        public string Tozihat { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShomareTamas { get; set; }
        //
   
        [Display(Name = "آگهی آخرین روزنامه")]

        public string FileParvaneKasb { get; set; }
        [Display(Name = "کپی سند مالکیت یا اجاره نامه رسمی")]

        public string FileSanad { get; set; }
        [Display(Name = "آگهی تاسیس شرکت ها")]

        public string FileKartMeli { get; set; }
        //
        [Display(Name = "نوع شرکت")]

        public EnumNoeSherkat? NoeSherkat { get; set; }

        [Display(Name = "نوع مالکیت")]

        public EnumNoeMalekiyat? NoeMalekiyat { get; set; }

        [Display(Name = "نوع تملک محل فعالیت")]

        public EnumNoeTamalok? NoeTamalok { get; set; }
        [Display(Name = "تلفن ثابت به همراه کد")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]


        public string TelephoneSabet { get; set; }
        //
        //
        [Display(Name = "نوع فروش")]

        public EnumNoeForush? NoeForush { get; set; }
      
        //

        public IFormFile FormFileKartMeli { get; set; }

        public IFormFile FormFileParvaneKasb { get; set; }
        public IFormFile FormFileSanad { get; set; }
        //
        [Display(Name = "وضعیت")]
        public EnumCooperationRequestStatus Status { get; set; }
        public string PrDateCheck { get; set; }
        [Display(Name = "توضیحات")]

        public string Description { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}