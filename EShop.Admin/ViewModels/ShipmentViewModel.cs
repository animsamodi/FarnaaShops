namespace EShop.Admin.ViewModels
{
    public class ShipmentViewModel
    {
        public ShipmentViewModel(string title, int price)
        {
            this.title = title;
            this.price = price;
        }

        public string title { get; set; }
        public int price { get; set; }
    }
}