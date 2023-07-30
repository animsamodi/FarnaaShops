using System;
using System.IO;
using EndPoint.Web.Utilities;
using EShop.Admin.Helper;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.Logging.AuditLog.Models;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class BlogController : BaseAdminController
    {
        private IBlogService _blogService;
        private IImageUploadService _imageUploadService;

        public BlogController(IBlogService blogService, HttpContextAccessor httpContextAccessor,
            Logging.AuditLog.IAuditService logger,IHttpContextAccessor contextAccessor, IImageUploadService imageUploadService) 
            : base(logger,contextAccessor)
        {
            _blogService = blogService;
            _imageUploadService = imageUploadService;
        }



        public IActionResult Index()
        {
            var res = _blogService.GetListBlogForAdmin();
            return View(res);
        }

        public IActionResult CreateBlog()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateBlog(CreateBlogViewModel blog)
        {
            var userId = ClaimUtility.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return View(blog);

            }
            string imgname = "";
            if (blog.ImgName != null)
            {
                if (ImageSecurity.Imagevalidator(blog.ImgName))
                {
                   // imgname = blog.ImgName.SaveImage("", "wwwroot/uploads");
                    //string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", imgname);
                    //string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tumb", imgname);
                                imgname = _imageUploadService.Upload(blog.ImgName);
                                ;
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا یک فایل درست انتحاب کنید");
                    return View(blog);

                }
            }

            Blog b = new Blog
            {

                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                TypeBlog = blog.TypeBlog,
                Image = imgname,
                ChangeUserId = userId,
                KeyWord = blog.KeyWord,
                PrDate = DateTime.Now.GetMonthPersian(),
                ShortText = blog.ShortText,
                Tag = blog.Tag,
                Title = blog.Title,
                Text = blog.Text,
                View = 0,
                MetaTitle = blog.MetaTitle,
                MetaDescription = blog.MetaDescription,
                MetaKeywords = blog.MetaKeywords,
                Canonical = blog.Canonical,
                HeaderTag = blog.HeaderTag,
                Schema = blog.Schema


            };

            TempData["res"] = "faild";
            if (_blogService.EditBlog(b))
            {
                _logger.CreateAuditScope(new AuditLog<Blog>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = b,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("Index");
        }
        public IActionResult EditBlog(long id)
        {
            var blog = _blogService.FindBlodById(id);
            if (blog == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction("Index");
            }




            CreateBlogViewModel viewModel = new CreateBlogViewModel()
            {
                Id = blog.Id,
                Writer = blog.Writer,
                TypeBlog = blog.TypeBlog,
                Image = blog.Image,
                ShortText = blog.ShortText,
                Text = blog.Text,
                KeyWord = blog.KeyWord,
                Tag = blog.Tag,
                Title = blog.Title,
                MetaTitle = blog.MetaTitle,
                MetaDescription = blog.MetaDescription,
                MetaKeywords = blog.MetaKeywords,
                Canonical = blog.Canonical,
                HeaderTag = blog.HeaderTag,
                Schema = blog.Schema
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditBlog(CreateBlogViewModel blog)
        {
            var userId = ClaimUtility.GetUserId(User);

            if (!ModelState.IsValid)
            {

                return View(blog);

            }
            string filename = blog.Image;
            if (blog.ImgName != null)
            {
                if (ImageSecurity.Imagevalidator(blog.ImgName))
                {
                    try
                    {
                        filename.DeleteImage("wwwroot/uploads");

                    }
                    catch (Exception e)
                    {
                    }

                    filename = "";
                    //filename = blog.ImgName.SaveImage(filename, "wwwroot/uploads");
                    //string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", filename);
                    //string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/tumb", filename);
                                    filename = _imageUploadService.Upload(blog.ImgName);
                                    ;
                }
                else
                {

                    ModelState.AddModelError("DesktopImg", "لطفا یک فایل درست انتحاب کنید");
                    return View(blog);
                }
            }

            Blog b = new Blog
            {
                Id = blog.Id,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                TypeBlog = blog.TypeBlog,
                Image = filename,
                ChangeUserId = userId,
                KeyWord = blog.KeyWord,
                PrDate = DateTime.Now.GetMonthPersian(),
                ShortText = blog.ShortText,
                Tag = blog.Tag,
                Title = blog.Title,
                Text = blog.Text,
                MetaTitle = blog.MetaTitle,
                MetaDescription = blog.MetaDescription,
                MetaKeywords = blog.MetaKeywords,
                Canonical = blog.Canonical,
                HeaderTag = blog.HeaderTag,
                Schema = blog.Schema


            };

            TempData["res"] = "faild";
            if (_blogService.EditBlog(b))
            {
                _logger.CreateAuditScope(new AuditLog<Blog>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = b,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteBlog(long id)
        {
            var res = _blogService.DeleteBlog(id);

            TempData["res"] = res ? "success" : "faild";
            return RedirectToAction("Index");

        }

    }
}