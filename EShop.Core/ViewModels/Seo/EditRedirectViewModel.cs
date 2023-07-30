using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels.Seo
{
    public class EditRedirectViewModel
    {
        public long Id { get; set; }

        [Display(Name = "آدرس قدیمی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
        //   ErrorMessage = "لطفا {0} معتبر وارد کنید")]
        public string OldUrl { get; set; }

        [Display(Name = "آدرس جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
        //    ErrorMessage = "لطفا {0} معتبر وارد کنید")]
        public string NewUrl { get; set; }

        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
