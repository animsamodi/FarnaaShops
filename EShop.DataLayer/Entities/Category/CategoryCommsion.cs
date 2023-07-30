using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Category
{
  public  class CategoryCommsion:BaseEntity
    {
      
        public byte Value { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
