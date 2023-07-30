using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
   public interface IReviewService : IBaseService<ProductReview>
    {
        ProductReviewContentViewModel GetProductReviewForAdmin(int id, EnumTypeSystem enumTypeSystem = EnumTypeSystem.Farnaa);
        List<RatingAttributeForAddReviewViewModel> GetRatingAttributeByCatId(long catid);
        List<RatingAttributeValueForAddReviewViewModel> GetProductRatingReview(int id, EnumTypeSystem enumTypeSystem = EnumTypeSystem.Farnaa);
        bool EditRview(ProductReview review);
        bool DeleteProductReviweRating(List<ProductReviewRating> productReviewRatings);
        bool AddOrUpdateProductReviweRating(List<ProductReviewRating> productReviewRatings);
        ProductReview GetProductReview(long id);
    }
}
