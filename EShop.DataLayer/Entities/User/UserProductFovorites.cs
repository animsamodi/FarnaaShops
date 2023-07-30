using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.User
{
    public class UserProductFovorites : BaseEntity
    {
     
        public long ProductId { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }
        [ForeignKey("ProductId")]

        public Product.Product Product { get; set; }
    }
}
