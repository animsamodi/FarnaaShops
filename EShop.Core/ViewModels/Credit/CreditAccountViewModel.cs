using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditAccountViewModel
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public long CreditId { get; set; }
        //مشخصات حساب‌های بانکی جاری فعال شرکت / صاحب جواز کسب
        [Display(Name = "بانک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Bank { get; set; }
        [Display(Name = "شعبه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Shobe { get; set; }
        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        public string Jari { get; set; }
        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[\u06F0-\u06F90-9]+$", ErrorMessage = "لطفا {0} را عدد وارد کنید")]

        [MaxLength(16, ErrorMessage = "مقدار {0} نباید بیشتر از{1} باشد")]
        [MinLength(16, ErrorMessage = "مقدار {0} نباید کمتر ار{1} باشد")]
        public string ShomareKart { get; set; }
        [Display(Name = "شهرستان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^[آ-ی ]*$", ErrorMessage = "لطفا {0} را با حروف فارسی وارد کنید")]

        public string Shahrestan { get; set; }

 
    }
}