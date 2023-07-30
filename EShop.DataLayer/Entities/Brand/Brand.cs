using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Entities.Variety;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop.DataLayer.Entities.Brand
{
    public class Brand:BaseEntity
    {
    

        [Display(Name = "عنوان فارسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FaTitle { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "عکس")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string ImgName { get; set; }
        public string OrginalImage { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Descrption { get; set; }

        [Display(Name = "کلمات کلیدی")]
         public string KeyWord { get; set; }

         [Display(Name = "اسکیما FAQ")]
         public string FAQSchema { get; set; }
         [Display(Name = "ترتیب")]
         public int Order { get; set; }
         [Display(Name = "نمایش در صفحه اصلی")]

        public bool IsShowInFirstPage { get; set; }

        public List<BrandCategory> BrandCategories { get; set; }
        public List<Product.Product> Products { get; set; }
        public List<IndexLayout> IndexLayouts { get; set; }
        public List<CategotyMain> CategotyMain { get; set; }
        public List<CategoryBrandPage> CategoryBrandPages { get; set; }
        public List<ProductSeri> ProductSeris { get; set; }
        public List<PageSeo> PageSeos { get; set; }
        public List<ColleaguePriceRange> ColleaguePriceRanges { get; set; }
        public List<SiteMenu> SiteMenus { get; set; }
        public List<FilterPrice> FilterPrices { get; set; }

        public List<FilterProperty> FilterProperties { get; set; }
        public List<ProductOtherPage> ProductOtherPages { get; set; }
        public List<PlusPriceRange> PlusPriceRanges { get; set; }
        public List<ColleaguePlusPriceRange> ColleaguePlusPriceRanges { get; set; }

    }
}
