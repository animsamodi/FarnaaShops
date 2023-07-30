using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EndPoint.Web.Utilities;
using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Brand;
using EShop.Web.Attribute;

namespace EShop.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var SpecialBlog = _blogService.GetSpecialBlog();
           var newBlog = _blogService.GetNewBlog();
           ViewBag.Special = SpecialBlog;
           ViewBag.NewBlog = newBlog;
            return View();
        }
        [Route("Blog/{id}")]

        public IActionResult BlogDetail(long id)
        {
            var res = _blogService.FindBlodById(id);
            if (res == null)
                return RedirectToAction("Index");

            _blogService.AddViewCounter(id);

            var newBlog = _blogService.GetNewBlog();
             ViewBag.NewBlog = newBlog;
            return View(res);
        }
        [HttpGet]
        public IActionResult BlogList(int pagenumber = 1)
        {
            var content = _blogService.GetListBlog("", pagenumber);
            ViewBag.count = content.Item1;
             ViewBag.PageNumber = pagenumber;
             return View(content.Item2);
        }

    }
}
