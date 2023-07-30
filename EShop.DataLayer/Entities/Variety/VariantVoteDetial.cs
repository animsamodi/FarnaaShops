using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;

namespace EShop.DataLayer.Entities.Variety
{
   public class VariantVoteDetial : BaseEntity
    {

        public byte Vote { get; set; }
        public int OrderDetailId { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
