using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;

namespace EShop.DataLayer.Entities.Cart
{
   public class GiftCard:BaseEntity
    {
        
        [MaxLength(20)]
        public string Code { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }

        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }
        public List<GiftCardTransaction> GiftCardTransactions { get; set; }
        public List<PaymentDetail> paymentDetails { get; set; }
    }
}
