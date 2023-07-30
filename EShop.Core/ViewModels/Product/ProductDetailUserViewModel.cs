using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels.Product
{


    public class ProductDetailUserViewModel
    {
        public long ProductId { get; set; }
        public long BrandId { get; set; }
        public string BrandEnTitle { get; set; }

        public string BrandImage { get; set; }
        public string FaTitle { get; set; }
        public bool IsFovorite { get; set; }
        public string EnTitle { get; set; }
        public string ImgName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryEnName { get; set; }
        public long Catid { get; set; }
        public string BrandName { get; set; }
        public string KeyWord { get; set; }
         public string MetaTitle { get; set; }
         public string MetaDescription { get; set; }
         public string MetaKeywords { get; set; }
         public string Canonical { get; set; }
         public string HeaderTag { get; set; }
 
        public bool IsShowPopUp { get; set; }
        public bool Have3dFile { get; set; }
        public string PopUpContent { get; set; }
        public List<ProductGallleyViewModel> Gallery { get; set; }
        public List<ProductPropertyUserViewModel> ProductProperty{ get; set; }
        public string Schema { get; set; }
        public string FAQSchema { get; set; }

        public string BaseSchema { get; set; }

    }
    public class ProductGallleyViewModel
    {
        public string ImgUrl { get; set; }
        public long? ProductOptionId { get; set; }
    }
}
