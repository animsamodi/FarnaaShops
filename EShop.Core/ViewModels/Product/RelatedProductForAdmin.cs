namespace EShop.Core.ViewModels.Product
{
    public class RelatedProductForAdmin
    {
        public long  ProductId { get; set; }
        public string  ProductTitle { get; set; }
        public string  EnTitle { get; set; }
        public string ProductImage { get; set; }
        public bool IsRelated { get; set; }
    }
}