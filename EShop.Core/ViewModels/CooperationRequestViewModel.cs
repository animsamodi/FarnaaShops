using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class CooperationRequestRealViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public EnumRealLegal Type { get; set; }
        //
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string Name { get; set; }//Name Sherkat
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]
        public string Family { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(10, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
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
        [Display(Name = "کپی کارت ملی صاحب جواز کسب")]

        public string FileKartMeli { get; set; }
        [Display(Name = "کپی پروانه کسب")]

        public string FileParvaneKasb { get; set; }
        [Display(Name = "(اختیاری)کپی سند مالکیت یا اجاره نامه رسمی")]

        public string FileSanad { get; set; }
        //
        //
        //
        [Display(Name = "نوع فروش")]

        public EnumNoeForush? NoeForush { get; set; }
        [Display(Name = "شناسه صنفی")]

        public string ShenaseSenfi { get; set; }
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