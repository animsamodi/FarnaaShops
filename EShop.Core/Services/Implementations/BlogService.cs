using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class BlogService : BaseService<Blog>, IBlogService
    {
        #region constructor

        private ApplicationDbContext _context;
        private readonly IUserService _userService;

        public BlogService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        #endregion


        public bool AddBlog(Blog blog)
        {
            try
            {
                blog.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(blog);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditBlog(Blog blog)
        {
            try
            {
                var nBlog = FindBlodById(blog.Id);

                nBlog.Id = blog.Id;
                nBlog.CreateDate = DateTime.Now;
                nBlog.LastUpdateDate = DateTime.Now;
                nBlog.TypeBlog = blog.TypeBlog;
                nBlog.Image = blog.Image;
                nBlog.ChangeUserId = blog.ChangeUserId;
                nBlog.KeyWord = blog.KeyWord;
                nBlog.PrDate = DateTime.Now.GetMonthPersian();
                nBlog.ShortText = blog.ShortText;
                nBlog.Tag = blog.Tag;
                nBlog.Title = blog.Title;
                nBlog.Text = blog.Text;




                nBlog.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(nBlog);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteBlog(long id)
        {
            try
            {
   var blog = FindBlodById(id);
            blog.SetRemoveDefaultValue(_userService.GetUserId());
            _context.Update(blog);
            _context.SaveChanges();
            return true;
            }
            catch (Exception e)
            {
                return false;
            }
         
        }

        public Blog FindBlodById(long id)
        {
            return _context.Blogs.Find(id);
        }

        public List<Blog> GetNewBlog()
        {
            return _context.Blogs.OrderByDescending(c => c.Id).Take(15).ToList();
        }

        public List<Blog> GetBestViewBlog()
        {
            return _context.Blogs.OrderByDescending(c => c.View).Take(15).ToList();
        }

        public List<Blog> GetSpecialBlog()
        {
            return _context.Blogs.Where(c=>c.TypeBlog != EnumTypeBlog.normal).OrderByDescending(c => c.Id).Take(15).ToList();
        }



        public Tuple<int, List<Blog>> GetListBlog(string searchText,int pageNumber)
        {
            int skip = (pageNumber - 1) * 25;

            var query = _context.Blogs
                .Where(c=>c.Title.Contains(searchText) 
                          || c.KeyWord.Contains(searchText)
                          || c.ShortText.Contains(searchText)
                          || c.Tag.Contains(searchText)
                          || c.Text.Contains(searchText)
                );

            return Tuple.Create(query.Count(), query.Skip(skip).Take(25).ToList());

        }

        public List<Blog> GetListBlogForAdmin()
        {
            return _context.Blogs.OrderByDescending(c => c.Id).ToList();
        }

        public void AddViewCounter(in long id)
        {
            var res = FindBlodById(id);
            if (res != null)
            {
                res.View++;
                _context.Update(res);
                _context.SaveChanges();
            }
        }
    }
}
