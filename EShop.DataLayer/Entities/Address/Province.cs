using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Order;

namespace EShop.DataLayer.Entities.Address
{
    public class Province:BaseEntity
    {
     

        [Display(Name = "نام استان")]
        [MaxLength(30, ErrorMessage = "مقدار {0} نباید بیشتراز{1} باشد")]
        public string ProvinceName { get; set; }
        public long? TaxCode { get; set; }

        public List<UserAddress> UserAddresses { get; set; }
        public List<City> Cities { get; set; }

        public List<Shipment> Shipmments { get; set; }
        public List<CooperationRequest> CooperationRequests { get; set; }


    }
}
