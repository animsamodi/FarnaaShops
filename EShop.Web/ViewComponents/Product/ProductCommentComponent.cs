using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.ViewComponents.Product
{
    public class ProductCommentComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public ProductCommentComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public async Task<IViewComponentResult> InvokeAsync(long productId)
        {
            return await Task.FromResult(View("ProductComment",new  Tuple<long,List<CommentForUserViewModel>>(productId, _commentService.GetAllCommentForUser(productId))));
        }
    }
}