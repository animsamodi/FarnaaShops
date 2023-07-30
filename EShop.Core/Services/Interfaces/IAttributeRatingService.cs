using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Product;

namespace EShop.Core.Services.Interfaces
{
  public  interface IAttributeRatingService : IBaseService<RatingAttribute>
    {
        List<RatingAttribute> GetRatingAttributes();
        long AddRatingAttribute(RatingAttribute ratingAttribute);
        bool AddCategoryRating(List<CategoryRating> categoryRatings);
    }
}
