using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;

namespace EShop.DataLayer.Entities.Address
{
   public class City:BaseEntity
    {
 

        [Display(Name = "نام شهر")]
        [MaxLength(30, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string CityName { get; set; }

        public long ProvinceId { get; set; }
        public long? TaxCode { get; set; }
        public List<UserAddress> UserAddresses { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }


        public List<Shipment> Shipmments { get; set; }
        public List<CooperationRequest> CooperationRequests { get; set; }

    }
}
