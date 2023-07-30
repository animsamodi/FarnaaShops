using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Variety;

namespace EShop.DataLayer.Entities.Seller
{
   public class Seller:BaseEntity
    {
  
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(500)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(11)]
        public string Phone { get; set; }

        public DateTime RegisterDate { get; set; }
        [MaxLength(4)]
        public string TimleySupply { get; set; }
        [MaxLength(4)]
        public string PostingWarranty { get; set; }
        [MaxLength(4)]
        public string NoReturend { get; set; }
        [MaxLength(4)]
        public string PurchaseConsent { get; set; }
        public int VoteCount { get; set; }
        public string Vote { get; set; }

        [Column(TypeName ="bigint")]
        public int Balance { get; set; }
        [Column(TypeName = "bigint")]
        public int BalanceReceivable { get; set; }
        public byte DeliveryTime { get; set; }

        public List<Variant> Variants { get; set; }
 
    }
}
