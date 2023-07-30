using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Product
{
    public class ReviewService : BaseService<ProductReview>, IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ReviewService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public ProductReviewContentViewModel GetProductReviewForAdmin(int id, EnumTypeSystem enumTypeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.ProductReviews.Where(p => p.ProductId == id && p.TypeSystem == enumTypeSystem).Select(p => new ProductReviewContentViewModel
            {
                Review = p.Review,
                Summary = p.Summary,
                Negative = p.Negative,
                Positive = p.Positive,
                ProductReviewId = p.Id,
                ShortReview = p.ShortReview
            }).FirstOrDefault();
        }

        public List<RatingAttributeForAddReviewViewModel> GetRatingAttributeByCatId(long catid)
        {
            return _context.CategoryRatings.Where(c => c.CategoryId == catid).Include("RatingAttribute")
                .Select(c => new RatingAttributeForAddReviewViewModel
                {
                    RatingAttributeId = c.RatingAttribute.Id,
                    Title = c.RatingAttribute.Title
                }).ToList();
        }

        public List<RatingAttributeValueForAddReviewViewModel> GetProductRatingReview(int id,
            EnumTypeSystem enumTypeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.ProductReviewRatings.Where(p => p.ProductId == id && p.TypeSystem == enumTypeSystem).Select(p => new RatingAttributeValueForAddReviewViewModel
            {
                RatingAttributeId = p.RatingAttributeId,
                Id = p.Id,
                Value = p.Value
            }).ToList();
        }

        public bool EditRview(ProductReview review)
        {
            try
            {
                review = review.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(review);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool DeleteProductReviweRating(List<ProductReviewRating> productReviewRatings)
        {
            try
            {foreach (var productReviewRating in productReviewRatings)
            {
                  productReviewRating.LastUpdateDate = DateTime.Now;
                  productReviewRating.IsDelete = true;
                
             }
                _context.UpdateRange(productReviewRatings);
                 _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddOrUpdateProductReviweRating(List<ProductReviewRating> productReviewRatings)
        {
            try
            {foreach (var productReviewRating in productReviewRatings)
            {
                  productReviewRating.LastUpdateDate = DateTime.Now;
                
             }
                _context.UpdateRange(productReviewRatings);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ProductReview GetProductReview(long id)
        {
            var res = _context.ProductReviews.Find(id);
            return res;
        }
    }
}
