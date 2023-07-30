using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Property
{
   public class PropertyCategory : BaseEntity
    {
         public long PropertyNameId { get; set; }
        public long CategoryId { get; set; }

        [InverseProperty("PropertyCategories")]
        [ForeignKey("CategoryId")]
        public Category.Category Category { get; set; }
        [InverseProperty("PropertyCategories")]
        [ForeignKey("PropertyNameId")]
        public PropertyName PropertyName { get; set; }
    }
}
