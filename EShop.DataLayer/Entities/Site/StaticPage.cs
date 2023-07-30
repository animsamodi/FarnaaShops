using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.DataLayer.Entities.Site
{
    public class StaticPage : BaseEntity

    {
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان انگلیسی")]
        public string EnTitle { get; set; }

        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public string OrginalImage { get; set; }

        [Display(Name = "ویدئو")]
        public string Video { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "متن کوتاه")]
        public string ShortText { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
        [Display(Name = "بازدید")]
        public int View { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeStaticPage TypeStaticPage { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
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
        [Display(Name = "نو ایندکس")]
        public bool IsNoIndex { get; set; }
        [Display(Name = "بنر")]

        public string Banner { get; set; }
        [Display(Name = "بنر موبایل")]

        public string BannerMobile { get; set; }
        [NotMapped]
        public IFormFile BannerImg { get; set; }
        [NotMapped]
        public IFormFile BannerMobileImg { get; set; }
        [Display(Name = "لینک بنر")]

        public string BannerUrl { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }
}
