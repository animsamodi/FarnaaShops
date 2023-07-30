using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Order
{
    public  class LimitOrder:BaseEntity
    {
 
        public int? Price { get; set; }
        public int? Day { get; set; }
        public bool IsActive { get; set; }
 

    }
}