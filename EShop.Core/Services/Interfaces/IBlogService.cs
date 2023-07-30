using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IBlogService : IBaseService<Blog>
    {

        bool AddBlog(Blog blog);
        bool EditBlog(Blog blog);
        bool DeleteBlog(long id);
        Blog FindBlodById(long id);

        List<Blog> GetNewBlog();
        List<Blog> GetBestViewBlog();
        List<Blog> GetSpecialBlog();
        Tuple<int, List<Blog>> GetListBlog(string searchText, int pageNumber);

        List<Blog> GetListBlogForAdmin();
        void AddViewCounter(in long id);
    }
}