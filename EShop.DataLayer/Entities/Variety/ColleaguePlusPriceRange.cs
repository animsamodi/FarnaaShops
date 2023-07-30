using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Seri;

namespace EShop.DataLayer.Entities.Variety
{
    public class ColleaguePlusPriceRange : BaseEntity
    {


        [Display(Name = "دسته بندی")]

        public long? CategoryId { get; set; }
        [Display(Name = "برند")]

        public long? BrandId { get; set; }
        [Display(Name = "سری")]

        public long? SeriId { get; set; }
        [Display(Name = "حداقل قیمت")]

        public int MinPrice { get; set; }
        [Display(Name = "حداکثر قیمت")]

        public int MaxPrice { get; set; }
        [Display(Name = "درصد تغییر قیمت")]

        public int ChangePricePercent { get; set; }
        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }

        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [ForeignKey("BrandId")]
        public Brand.Brand Brand { get; set; }
        [ForeignKey("SeriId")]
        public ProductSeri ProductSeri { get; set; }


    }
}