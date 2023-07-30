using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Site
{
    public class Contact : BaseEntity
    {

        [Display(Name = "عنوان 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title1 { get; set; }
        [Display(Name = "ایمیل 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email1 { get; set; }
        [Display(Name = "تلفن 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone1 { get; set; }
        [Display(Name = "عنوان 2")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title2 { get; set; }
        [Display(Name = "21")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email2 { get; set; }
        [Display(Name = "تلفن 2")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone2 { get; set; }
        [Display(Name = "عنوان 3")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title3 { get; set; }
        [Display(Name = "ایمیل 3")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email3 { get; set; }
        [Display(Name = "تلفن 3")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone3 { get; set; }
        [Display(Name = "طول جغراقیایی")]

        public string Lat { get; set; }
        [Display(Name = "عرض جغرافیایی")]

        public string Lng { get; set; }
        [Display(Name = "متن")]

        public string Text { get; set; }
        [Display(Name = "آدرس")]

        public string Address { get; set; }
        [Display(Name = "کدپستی")]

        public string CodePosti { get; set; }
        //
        [Display(Name = "متا Title")]
        public string MetaTitle { get; set; }
        [Display(Name = "متا Description")]
        public string MetaDescription { get; set; }
        [Display(Name = "متا Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Canonical")]
        public string Canonical { get; set; }
        [Display(Name = "تگ های هدر")]
        public string HeaderTag { get; set; }
        [Display(Name = "اسکیما")]

        public string Schema { get; set; }
        public string BaseSchema { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }

    }
}