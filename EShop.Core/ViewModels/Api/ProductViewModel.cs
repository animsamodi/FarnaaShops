using System;
using System.Collections.Generic;

namespace EShop.Core.ViewModels.Api
{

    public class FilterProductApiViewModel
    {
        public long?ProductId{ get; set; }
        public long?CategoryId{ get; set; }
        public long?BrandId{ get; set; }
        public string BrandName { get; set; }
        public string ProductName{ get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductApiViewModel
    {

        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string CategoryEnTitle { get; set; }
        public string CategoryFaTitle { get; set; }

        public long? CategoryId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandEnTitle { get; set; }
        public long? BrandId { get; set; }
        public string ImgName { get; set; }
        public string Url { get; set; }
        public long ProductId { get; set; }
        public int MainPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public DateTime date { get; set; }

        //public List<ProductPropertyApiViewModel> Property { get; set; }
        public List<ProductColorApiViewModel> VariantColor { get; set; }


    }

    public class ProductColorApiViewModel
    {
        public string Text { get; set; }
        public string Color { get; set; }



    }

    public class ProductPropertyApiViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
