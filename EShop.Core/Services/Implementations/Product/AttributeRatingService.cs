using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Product;

namespace EShop.Core.Services.Implementations.Product
{
    public class AttributeRatingService : BaseService<RatingAttribute>, IAttributeRatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AttributeRatingService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<RatingAttribute> GetRatingAttributes()
        {
            return _context.RatingAttributes.ToList();
        }

        public long AddRatingAttribute(RatingAttribute ratingAttribute)
        {
            try
            {
                ratingAttribute.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(ratingAttribute);
                _context.SaveChanges();
                return ratingAttribute.Id;
            }
            catch
            {
                return 0;
            }
        }

        public bool AddCategoryRating(List<CategoryRating> categoryRatings)
        {
            try
            {foreach (var categoryRating in categoryRatings)
            {
                categoryRating.IsDelete = false;
                categoryRating.CreateDate = DateTime.Now;
                categoryRating.LastUpdateDate = DateTime.Now;
                
             }
                _context.AddRange(categoryRatings);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
