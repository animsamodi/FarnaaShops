namespace EShop.Core.ViewModels
{
    public class FavoriteListForProfileViewModel
    {
        public long FavoriteId { get; set; }
        public long Productid { get; set; }
        public string ProductTitle { get; set; }
        public string ProductCategory { get; set; }
        public string EnTitle { get; set; }
        public string ImgName { get; set; }
        public int price { get; set; }
        public byte Star { get; set; }
    }
}
