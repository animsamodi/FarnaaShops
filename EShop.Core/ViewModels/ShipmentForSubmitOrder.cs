namespace EShop.Core.ViewModels
{
    public class ShipmentForSubmitOrder
    {
        public long ShipmentId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int PricePerAddProduct { get; set; }
        public long? ProvinceId { get; set; }
        public long? CityId { get; set; }
 

    }
}