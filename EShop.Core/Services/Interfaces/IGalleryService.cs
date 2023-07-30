using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Product;

namespace EShop.Core.Services.Interfaces
{
    public interface IGalleryService : IBaseService<ProductImage>
    {
        List<ProductImage> GetProductImagesForAdmin(long id);
        bool AddProductImage(ProductImage productImage);
        ProductImage FindImageById(long id);
        bool DeleteImage(ProductImage image);
    }
}
