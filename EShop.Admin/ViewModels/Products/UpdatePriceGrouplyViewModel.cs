using System.ComponentModel.DataAnnotations;

namespace EShop.Admin.ViewModels.Products
{
    public class UpdatePriceGrouplyViewModel
    {
        [Display(Name = "درصد تغییر قیمت")]
        public int Percent { get; set; }

        [Display(Name = "برند")]
        public long BrandId { get; set; }

        [Display(Name = "دسته بندی")]
        public long CategoryId { get; set; }
    }
}
