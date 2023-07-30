using System;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Order
{
    public class CrmLog : BaseEntity
    {

        public long? OrderId { get; set; }
        public long? UserId { get; set; }
        public long? OrderDetailId { get; set; }
        public string Token { get; set; }
        
        public string Url { get; set; }
        public string Params { get; set; }
        public string Result { get; set; }
        public string ResultMessage { get; set; }
        public string ResultCode { get; set; }
        public DateTime DateCall { get; set; }
        public bool Status { get; set; }

    }
}