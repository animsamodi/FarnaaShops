using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class IndexLayoutViewModel
    {
        public long Id { get; set; }
        public EnumTypeLayotIndex Type { get; set; }
        public int Order { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string BrandTitle { get; set; }
        public long? BrandId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public EnumSortLayoutIndex Sort { get; set; }
        public string Url { get; set; }
        public string Image1 { get; set; }
        public string ImageUrl1 { get; set; }
        public string Image2 { get; set; }
        public string ImageUrl2 { get; set; }
        public string Image3 { get; set; }
        public string ImageUrl3 { get; set; }
        public string Image4 { get; set; }
        public string ImageUrl4 { get; set; }
        public bool IsMain { get; set; }
        public string ParentCategoryEnName { get; set; }
        public string SideImage { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
    public class CreateIndexLayoutViewModel
    {
        public long Id { get; set; }

        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public EnumTypeLayotIndex Type { get; set; }

        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         public int Order { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public bool IsActive { get; set; }
        [Display(Name = "دسته بندی")]
         
        public long? CategoryId { get; set; }
        [Display(Name = "برند")]
         
        public long? BrandId { get; set; }
        [Display(Name = "عنوان")]
         
        public string Title { get; set; }
        [Display(Name = "تعداد محصول")]
         
        public int Count { get; set; }
        [Display(Name = "مرتب سازی")]
         
        public EnumSortLayoutIndex Sort { get; set; }
        [Display(Name = "لینک")]
         
        public string Url { get; set; }
        [Display(Name = "تصویر اصلی")]

        public string OrginalImage1 { get; set; }


        [Display(Name = "تصویر اول")]
         
        public string Image1 { get; set; }
        public IFormFile Image1FormFile { get; set; }


        [Display(Name = "لینک تصویر اول")]
         
        public string ImageUrl1 { get; set; }
        [Display(Name = "تصویر دوم")]
         
        public string Image2 { get; set; }
        public IFormFile Image2FormFile { get; set; }

        [Display(Name = "لینک تصویر دوم")]
         
        public string ImageUrl2 { get; set; }
        //
        [Display(Name = "تصویر پس زمینه")]
        public string BgImage { get; set; }
        public IFormFile BgImageFormFile { get; set; }
        //-
        [Display(Name = "تصویر سوم")]
        public string Image3 { get; set; }
        public IFormFile Image3FormFile { get; set; }

        [Display(Name = "لینک تصویر سوم")]
        public string ImageUrl3 { get; set; }
        [Display(Name = "تصویر چهارم")]
        public string Image4 { get; set; }
        public IFormFile Image4FormFile { get; set; }

        [Display(Name = "لینک تصویر چهارم")]
        public string ImageUrl4 { get; set; }

        [Display(Name = "تصویر کنار")]

        public string SideImage { get; set; }

        public IFormFile SideImageFormFile { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }


    }


    public class CreateCategotyMainViewModel
    {
        public long Id { get; set; }
        [Display(Name = "ترتیب")]

        public int Order { get; set; }
        [Display(Name = "تصویر")]

    
        public string Image { get; set; }
        public IFormFile ImageFormFile { get; set; }
        [Display(Name = "عنوان")]

        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Text { get; set; }
        [Display(Name = "اسکیما FAQ")] 
        public string FAQSchema { get; set; }
        public string EnTitle { get; set; }
        [Display(Name = "رنگ")]

        public string Color { get; set; }
        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [Display(Name = "نوع")]

        public EnumTypeCategotyMain Type { get; set; }
        [Display(Name = "وضعیت")]


        public bool IsActive { get; set; }
        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }



    }
    public class CategotyMainViewModel
    {
        public long Id { get; set; }
        public string EnTitle { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public List<CategotyMainProductsViewModel> CategotyMainProductsViewModels { get; set; }
        public List<Brand> Brands { get; set; }
        public string FAQSchema { get; set; }

    }
    public class CategotyMainProductsViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public long? CatId { get; set; }
        public long? BrandId { get; set; }
        public string BrandEnTitle { get; set; }
        public string CatEnTitle { get; set; }
        public int Order{ get; set; }

        public List<ProductViewModel> Products { get; set; }

    }

    public class SiteSettingViewModel
    {
        public long Id { get; set; }

        public string FavIcon { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Enamad { get; set; }
        public string FotterText { get; set; }
        public string FotterAbout { get; set; }
        public string Logo { get; set; }
        [Display(Name = "تصویر بالای سایت وب")]
        public string TopImageBannerWeb { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت وب")]

        public string TopImageBannerWebTitle { get; set; }
        public IFormFile ImageFormFileWeb { get; set; }
        public IFormFile ImageFormFileMobile { get; set; }
        [Display(Name = "تصویر بالا ی سایت موبایل")]

        public string TopImageBannerMobile { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileTitle { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت وب")]

        public bool ShowTopImageBannerWeb { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت موبایل")]

        public bool ShowTopImageBannerMobile { get; set; }
        [Display(Name = "لینک تصویر بالای سایت وب")]

        public string TopImageBannerWebUrl { get; set; }
                [Display(Name = "لینک تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileUrl { get; set; }
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
        [Display(Name = "متا Title")]
        public string BlogMetaTitle { get; set; }
        [Display(Name = "متا Description")]
        public string BlogMetaDescription { get; set; }
        [Display(Name = "متا Keywords")]
        public string BlogMetaKeywords { get; set; }
        [Display(Name = "Canonical")]
        public string BlogCanonical { get; set; }
        [Display(Name = "تگ های هدر")]
        public string BlogHeaderTag { get; set; }
        [Display(Name = "اسکیما")]
        public string BlogSchema { get; set; }
        public string BlogBaseSchema { get; set; }

        public string Robots { get; set; }
        //
        [Display(Name = "متن پایین صفحه اصلی")]

        public string HomeText { get; set; }
        [Display(Name = "متن فوتر")]

        public string FoterText { get; set; }
        [Display(Name = "فوتر سمت راست")]

        public string FoterRight { get; set; }
        [Display(Name = "فوتر سمت چپ")]

        public string FoterLeft { get; set; }
        //
        [Display(Name = "درگاه پیشفرض")]

        public EnumDefaultIpg DefaultIpg { get; set; }
        [Display(Name = "درگاه بانک ملت")]

        public bool ShowMelatIpg { get; set; }
        [Display(Name = "درگاه آسان پرداخت")]

        public bool ShowApIpg { get; set; }
        [Display(Name = "درگاه ایران کیش")]

        public bool ShowKishIpg { get; set; }
        [Display(Name = "درگاه زرین پال")]

        public bool ShowZarinPalIpg { get; set; }
        //
        [Display(Name = "درگاه بانک ملی")]

        public bool ShowMeliIpg { get; set; }

        public IFormFile ImageFormFileSearchBg { get; set; }
        [Display(Name = "تصویر پس زمینه هدر")]
        public string SearchBg { get; set; }


    }
    public class ColleaugeSettingViewModel
    {
        public long Id { get; set; }

 
        [Display(Name = "تصویر بالای سایت وب")]
        public string TopImageBannerWeb { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت وب")]

        public string TopImageBannerWebTitle { get; set; }
        public IFormFile ImageFormFileWeb { get; set; }
        public IFormFile ImageFormFileMobile { get; set; }
        [Display(Name = "تصویر بالا ی سایت موبایل")]

        public string TopImageBannerMobile { get; set; }
        [Display(Name = "عنوان تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileTitle { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت وب")]

        public bool ShowTopImageBannerWeb { get; set; }
        [Display(Name = "نمایش تصویر بالای سایت موبایل")]

        public bool ShowTopImageBannerMobile { get; set; }
        [Display(Name = "لینک تصویر بالای سایت وب")]

        public string TopImageBannerWebUrl { get; set; }
                [Display(Name = "لینک تصویر بالای سایت موبایل")]

        public string TopImageBannerMobileUrl { get; set; }


        [Display(Name = "ساعت شروع فعالیت همکار")]
        public string StartTime { get; set; }
        [Display(Name = "ساعت پایان فعالیت همکار")]

        public string EndTime { get; set; }
        [Display(Name = "استفاده همکار")]

        public bool IsActive { get; set; }

    }
    public class IndexMainMetaViewModel
    {
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
    }


}