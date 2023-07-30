using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Product
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        private ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CommentService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public Tuple<List<CommentForUserViewModel>, List<ReviewRatingViewModel>, int> GetCommentForUser(long productId, int take)
        {
            var comments = _context.Comments
                .Include(c => c.User)
                .Where(c => c.ProductId == productId && c.StatusComment == EnumStatusComment.Confirm)
                .Select(c => new CommentForUserViewModel
                {
                    CommentId = c.Id,
                    CommentTitle = c.CommentTitle,
                    CommentText = c.CommentText,
                    Positive = c.Positive,
                    Negative = c.Negative,
                    Like = c.CommentLike,
                    DisLike = c.CommentDisLike,
                    CreateDate = c.CreateDate,
                    UserName = c.User.FullName,
                    Mobile = c.Mobile,
                    Name = c.Name
                });

            var count = comments.Count();

            var ratings = _context.CommentRatings
                .Include(c => c.RatingAttribute)
                .Select(c => new ReviewRatingViewModel
                {
                    Title = c.RatingAttribute.Title,
                    Value = c.Value
                }).ToList();



            return Tuple.Create(comments.Take(take).ToList(), ratings.ToList(), count);
        }

        public List<CommentForUserViewModel> GetCommentsByFilter(long productId, int pageNumber, int sort, int take)
        {
            int skip = (pageNumber - 1) * take;
            IQueryable<Comment> comment = _context.Comments.Where(c => c.ProductId == productId && c.StatusComment == EnumStatusComment.Confirm);
            if (sort == 2)
                comment = comment.OrderByDescending(c => c.CommentLike);
            if (sort == 3)
                comment = comment.OrderByDescending(c => c.CreateDate);

            List<CommentForUserViewModel> commentList = comment.Select(c => new CommentForUserViewModel
            {
                CommentId = c.Id,
                CommentTitle = c.CommentTitle,
                CommentText = c.CommentText,
                Positive = c.Positive,
                Negative = c.Negative,
                Like = c.CommentLike,
                DisLike = c.CommentDisLike,
                CreateDate = c.CreateDate,
                UserName = c.User.FullName,
                Name = c.Name,
                Mobile = c.Mobile
            }).Skip(skip).Take(take).ToList();
            return commentList;
        }

        public bool AddComment(Comment comment)
        {
            try
            {
                comment.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(comment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<UserCommentVM> GetUserComments(long userId)
        {
            var quary = _context.Comments.Where(c => c.UserId == userId)
                .Include(c => c.Product).ThenInclude(p=>p.Category)
                .Select(c => new UserCommentVM
                {
                    ProductId = c.ProductId,
                    StatusComment = c.StatusComment,
                    CommentTitle = c.CommentTitle,
                    CommentText = c.CommentText,
                    Positive = c.Positive,
                    Negative = c.Negative,
                    Id = c.Id,
                    ProductImage = c.Product.ImgName,
                    ProductTitle = c.Product.FaTitle,
                    productCategoryName=c.Product.Category.EnTitle
                }).OrderByDescending(c => c.Id).ToList();
            return quary;
        }

        public List<UserCommentAdminVM> GetUserCommentsForAdmin()
        {
            var quary = _context.Comments
                .Include(c => c.Product)
                .Include(c=>c.User)
                .Select(c => new UserCommentAdminVM
                {
                    ProductId = c.ProductId,
                    StatusComment = c.StatusComment,
                    CommentTitle = c.CommentTitle,
                    CommentText = c.CommentText,
                    Positive = c.Positive,
                    Negative = c.Negative,
                    Id = c.Id,
                    ProductImage = c.Product.ImgName,
                    ProductTitle = c.Product.FaTitle,
                    RegisterDate = c.CreateDate.GetMonthPersian(),
                    RegisterTime = c.CreateDate.ToShortTimeString(),
                    UserId = c.UserId,
                    UserName = c.User.FullName,
                    UserNationalCode = c.User.NatioalCode,
                    Name = c.Name,
                    Mobile = c.Mobile,
                    TypeSystem = c.TypeSystem
                    

                    
                }).OrderByDescending(c => c.Id).ToList();
            return quary;
        }

        public bool ChangeUserCommentStatus(long id, EnumStatusComment statusComment)
        {

            try
            {
                var comment = _context.Comments.Find(id);
                comment.StatusComment = statusComment;
                comment.SetEditDefaultValue(_userService.GetUserId());
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<CommentForUserViewModel> GetAllCommentForUser(long productId, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var comments = _context.Comments
                .Include(c => c.User)
                .Where(c => c.ProductId == productId && c.StatusComment == EnumStatusComment.Confirm && c.TypeSystem == typeSystem)
                .Select(c => new CommentForUserViewModel
                {
                    CommentId = c.Id,
                    CommentTitle = c.CommentTitle,
                    CommentText = c.CommentText,
                    Positive = c.Positive,
                    Negative = c.Negative,
                    Like = c.CommentLike,
                    DisLike = c.CommentDisLike,
                    CreateDate = c.CreateDate,
                    UserName = c.User.FullName,
                    Mobile = c.Mobile,
                    Name = c.Name
                });

 
 



            return comments.ToList();
        }
    }
}
