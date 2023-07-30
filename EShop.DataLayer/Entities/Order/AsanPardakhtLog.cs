using System;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Order
{
    public class AsanPardakhtLog : BaseEntity
    {

        public long? OrderId { get; set; }
        public long? UserId { get; set; }
        public long? PaymentDetailId { get; set; }
         
        public string Method { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Headers { get; set; }
        public string Params { get; set; }
        public string Contents { get; set; }
        public bool Status { get; set; }
        public string Result { get; set; }
         

    }
}