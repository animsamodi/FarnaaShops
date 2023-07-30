using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Category
{
    public class SubCategory:BaseEntity
    {
       

        public long ParentId { get; set; }

        public long SubId { get; set; }

        [InverseProperty("ParentCategory")]
        [ForeignKey("ParentId")]
        public EShop.DataLayer.Entities.Category.Category ParentCategory { get; set; }

        [InverseProperty("SubCategory")]
        [ForeignKey("SubId")]
        public EShop.DataLayer.Entities.Category.Category SubCat { get; set; }
    }
}
