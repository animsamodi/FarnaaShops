using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Product.Comment;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
   public interface ICommentService : IBaseService<Comment>
    {
        Tuple<List<CommentForUserViewModel>, List<ReviewRatingViewModel>, int> GetCommentForUser(long productId, int take);
        List<CommentForUserViewModel> GetCommentsByFilter(long productId, int pageNumber, int sort, int take);
        bool AddComment(Comment comment);
        List<UserCommentVM> GetUserComments(long userId);
        List<UserCommentAdminVM> GetUserCommentsForAdmin();
        bool ChangeUserCommentStatus(long id, EnumStatusComment statusComment);
        List<CommentForUserViewModel> GetAllCommentForUser(long productId, EnumTypeSystem typeSystem =EnumTypeSystem.Farnaa);
    }
}
