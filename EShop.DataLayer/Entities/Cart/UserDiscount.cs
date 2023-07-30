using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Cart
{
    public class UserDiscount : BaseEntity
    {


        public long DiscountId { get; set; }
        public long UserId { get; set; }
        public bool IsUse { get; set; }
        [ForeignKey("DiscountId")]
        public DiscountCode Category { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; }

    }
}