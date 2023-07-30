using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.SiteMap;
using EShop.DataLayer.Migrations;

namespace EShop.Web.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStaticPageService _staticPageService;
        private readonly IBlogService _blogService;
        private readonly ISiteSettingService _siteSettingService;
        private IBrandService _brandService;
        private ICategoryService _categoryService;
        private IProductSeriService _productSeriService;

        public SiteMapController(IBlogService blogService, IProductService productService, IStaticPageService staticPageService, ISiteSettingService siteSettingService, IBrandService brandService, ICategoryService categoryService, IProductSeriService productSeriService)
        {
            _blogService = blogService;
            _productService = productService;
            _staticPageService = staticPageService;
            _siteSettingService = siteSettingService;
            _brandService = brandService;
            _categoryService = categoryService;
            _productSeriService = productSeriService;
        }
        /* [Route("robots.txt", Name = "GetRobotsText", OutputCache(Duration = 86400)]*/
        [Route("/robots.txt")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
        //public ContentResult RobotsText()
        public IActionResult RobotsText()
        {

            var robots = _siteSettingService.GetSiteRobots();

            return this.Content(robots, "text/plain", Encoding.UTF8);
        }
        [Route("/sitemap-test.xml")]
        public async Task<ActionResult> IndexTest()
        {
            var products = _productService.GetAllProduct().Where(c => c.IsPublished).ToList();
            //var blogs = _blogService.GetListBlogForAdmin();
            //var pages = _staticPageService.GetListStatic();

            var siteMapBuilder = new SiteMapBuilder();
            siteMapBuilder.AddUrl("https://farnaa.com/", modified: DateTime.Now, changeFrequency: ChangeFrequency.Daily, priority: 1.0);
            //siteMapBuilder.AddUrl("https://farnaa.com/blogs/", modified: DateTime.Now, changeFrequency: ChangeFrequency.Daily, priority: 1.0);
            foreach (var product in products)
            {
                siteMapBuilder.AddUrl("https://farnaa.com/product/" + product.Id + "/" + product.EnTitle.Replace(" ", "-"), modified: product.LastUpdateDate, changeFrequency: ChangeFrequency.Daily, priority: 0.8000);
            }
            //foreach (var page in pages)
            //{
            //    siteMapBuilder.AddUrl("https://farnaa.com/home/Page/" + page.Id , modified: page.CreateDate, changeFrequency: ChangeFrequency.Daily, priority: 0.8000);
            //}
            //foreach (var blog in blogs)
            //{
            //    siteMapBuilder.AddUrl("https://farnaa.com/blog/" + blog.Id, modified: blog.CreateDate, changeFrequency: ChangeFrequency.Daily, priority: 0.6000);
            //}
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }

        [Route("/sitemap.xml")]
        public async Task<ActionResult> Index()
        {


            #region New
            var siteMapBuilder = new SiteMapBuilder();
            siteMapBuilder.AddUrl("https://farnaa.com/product-sitemap.xml", modified: DateTime.Now, changeFrequency: ChangeFrequency.Always, priority: 1.0);
            siteMapBuilder.AddUrl("https://farnaa.com/brand-sitemap.xml", modified: DateTime.Now, changeFrequency: ChangeFrequency.Monthly, priority: 1.0);
            siteMapBuilder.AddUrl("https://farnaa.com/category-sitemap.xml", modified: DateTime.Now, changeFrequency: ChangeFrequency.Monthly, priority: 1.0);
            siteMapBuilder.AddUrl("https://farnaa.com/category-seri-sitemap.xml", modified: DateTime.Now, changeFrequency: ChangeFrequency.Monthly, priority: 1.0);
            siteMapBuilder.AddUrl("https://farnaa.com/page-sitemap.xml", modified: DateTime.Now, changeFrequency: ChangeFrequency.Monthly, priority: 1.0);
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion

            #region Old
            //var lstSiteMap = new List<SiteMapViewModel>();
            //lstSiteMap.Add(new SiteMapViewModel
            //{
            //    Url = "https://farnaa.com/product-sitemap.xml",
            //    LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //});
            //lstSiteMap.Add(new SiteMapViewModel
            //{
            //    Url = "https://farnaa.com/brand-sitemap.xml",
            //    LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //});
            //lstSiteMap.Add(new SiteMapViewModel
            //{
            //    Url = "https://farnaa.com/category-sitemap.xml",
            //    LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //});
            //lstSiteMap.Add(new SiteMapViewModel
            //{
            //    Url = "https://farnaa.com/category-seri-sitemap.xml",
            //    LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //});
            //lstSiteMap.Add(new SiteMapViewModel
            //{
            //    Url = "https://farnaa.com/page-sitemap.xml",
            //    LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //});
            //return View(lstSiteMap);
            #endregion

        }
        [Route("/product-sitemap.xml")]
        public async Task<ActionResult> SiteMapProduct()
        {
            #region New
            var siteMapBuilder = new SiteMapBuilder();
            var products = _productService.GetAllProductByCategory().Where(c => c.IsPublished).ToList();
            foreach (var product in products)
            {
                siteMapBuilder.AddUrl(
                    "https://farnaa.com/product/" + product.Id + "/" + product.Category.EnTitle.ToLower().Replace(" ", "-") + "/" + product.EnTitle.ToLower().Trim().Replace(" ", "-"),
                    modified: product.LastUpdateDate,
                    changeFrequency: ChangeFrequency.Daily,
                    priority: 0.8000);
            }


            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion

            #region Old
            //var products = _productService.GetAllProduct().Where(c => c.IsPublished).ToList();

            //var lstSiteMap = new List<SiteMapViewModel>();
            //foreach (var product in products)
            //{
            //    lstSiteMap.Add(new SiteMapViewModel
            //    {
            //        Url = "https://farnaa.com/product/" + product.Id + "/" + product.Category.EnTitle.ToLower().Replace(" ", "-") + "/" + product.EnTitle.ToLower().Trim().Replace(" ", "-"),
            //        LastMod = product.LastUpdateDate.ToShortDateString() + "  " + product.LastUpdateDate.ToShortTimeString()
            //    });
            //}

            //TempData["BackUrl"] = true;

            //return View("Index", lstSiteMap);
            #endregion


        }
        [Route("/brand-sitemap.xml")]
        public async Task<ActionResult> SiteMapBrand()
        {

            #region New
            var siteMapBuilder = new SiteMapBuilder();
            var categories = _categoryService.GetAllCategory();
            foreach (var category in categories)
            {
                if (category.Id != 29)
                {
  var brands = _brandService.GetBrandByCategoryId(category.Id);
                foreach (var brand in brands)
                {
                    siteMapBuilder.AddUrl(
                       "https://farnaa.com/category/" + category.EnTitle.ToLower().Replace(" ", "-") + "/" + brand.EnTitle.ToLower().Trim().Replace(" ", "-"),
                        modified: brand.LastUpdateDate,
                        changeFrequency: ChangeFrequency.Daily,
                        priority: 0.8000);
                }
                }
              
            }


            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion


            #region Old
            //var categories = _categoryService.GetAllCategory();
            //   var lstSiteMap = new List<SiteMapViewModel>();

            //   foreach (var category in categories)
            //   {
            //       var brands = _brandService.GetBrandByCategoryId(category.Id);
            //       foreach (var brand in brands)
            //       {
            //           lstSiteMap.Add(new SiteMapViewModel
            //           {
            //               //"https://farnaa.com/category/Powerbank/samsung"
            //               Url = "https://farnaa.com/category/" + category.EnTitle.ToLower().Replace(" ", "-") + "/" + brand.EnTitle.ToLower().Trim().Replace(" ", "-"),
            //               LastMod = brand.LastUpdateDate.ToShortDateString() + "  " + brand.LastUpdateDate.ToShortTimeString()
            //           });
            //       }
            //   }


            //   TempData["BackUrl"] = true;

            //   return View("Index", lstSiteMap);
            #endregion

        }
        [Route("/category-sitemap.xml")]
        public async Task<ActionResult> SiteMapCategory()
        {
            #region New
            var siteMapBuilder = new SiteMapBuilder();
            var categories = _categoryService.GetCategoriesForTree();

            foreach (var category in categories.Where(c => c.ParentId == null))
            {
                foreach (var category1 in categories.Where(c => c.ParentId == category.Id))
                {
                    siteMapBuilder.AddUrl(
                   "https://farnaa.com/category/" + category1.EnTitle.ToLower().Replace(" ", "-"),
                     modified: DateTime.Now,
                     changeFrequency: ChangeFrequency.Daily,
                     priority: 0.8000);

                    foreach (var category2 in categories.Where(c => c.ParentId == category1.Id))
                    {
                        siteMapBuilder.AddUrl(
                     "https://farnaa.com/category/" + category1.EnTitle.ToLower().Replace(" ", "-") + "/" + category2.EnTitle.ToLower().Replace(" ", "-"),
                     modified: DateTime.Now,
                     changeFrequency: ChangeFrequency.Daily,
                     priority: 0.8000);


                    }
                }
            }
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion

            #region Old
            //var categories = _categoryService.GetCategoriesForTree();
            //var lstSiteMap = new List<SiteMapViewModel>();

            //foreach (var category in categories.Where(c => c.ParentId == null))
            //{
            //    foreach (var category1 in categories.Where(c => c.ParentId == category.Id))
            //    {
            //        lstSiteMap.Add(new SiteMapViewModel
            //        {
            //            Url = "https://farnaa.com/category/" + category1.EnTitle.ToLower().Replace(" ", "-"),
            //            LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //        });
            //        foreach (var category2 in categories.Where(c => c.ParentId == category1.Id))
            //        {
            //            lstSiteMap.Add(new SiteMapViewModel
            //            {
            //                Url = "https://farnaa.com/category/" + category1.EnTitle.ToLower().Replace(" ", "-") + "/" + category2.EnTitle.ToLower().Replace(" ", "-"),
            //                LastMod = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString()
            //            });

            //        }
            //    }
            //}


            //TempData["BackUrl"] = true;

            //return View("Index", lstSiteMap);
            #endregion

        }
        [Route("/page-sitemap.xml")]
        public async Task<ActionResult> SiteMapPage()
        {


            #region New
            var pages = _staticPageService.GetListStatic();
            var siteMapBuilder = new SiteMapBuilder();

            foreach (var page in pages)
            {
                if (!string.IsNullOrEmpty(page.EnTitle))
                    siteMapBuilder.AddUrl(
                     "https://farnaa.com/page/" + page.EnTitle?.ToLower().Replace(" ", "-"),
                     modified: page.LastUpdateDate,
                     changeFrequency: ChangeFrequency.Daily,
                     priority: 0.8000);

            }


            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion


            #region Old
            //var pages = _staticPageService.GetListStatic();
            //var lstSiteMap = new List<SiteMapViewModel>();

            //foreach (var page in pages)
            //{
            //    if (!string.IsNullOrEmpty(page.EnTitle))
            //        lstSiteMap.Add(new SiteMapViewModel
            //        {
            //            Url = "https://farnaa.com/page/" + page.EnTitle?.ToLower().Replace(" ", "-"),
            //            LastMod = page.LastUpdateDate.ToShortDateString() + "  " + page.LastUpdateDate.ToShortTimeString()
            //        });
            //}


            //TempData["BackUrl"] = true;

            //return View("Index", lstSiteMap);
            #endregion

        }
        [Route("/category-seri-sitemap.xml")]
        public async Task<ActionResult> SiteMapSeri()
        {
            #region New
            var categories = _categoryService.GetAllCategory();
            var siteMapBuilder = new SiteMapBuilder();

            foreach (var category in categories)
            {
                var brands = _brandService.GetBrandByCategoryId(category.Id);
                foreach (var brand in brands)
                {
                    var series = _productSeriService.GetProductSeriesByCategoryIdAndBrandId(category.Id, brand.Id);
                    foreach (var productSeri in series)
                    {

                        siteMapBuilder.AddUrl(
                    "https://farnaa.com/category/" + category.EnTitle.ToLower().Replace(" ", "-") + "/" + brand.EnTitle.ToLower().Trim().Replace(" ", "-") + "/" + productSeri.EnTitle.ToLower().Trim().Replace(" ", "-"),
                    modified: productSeri.LastUpdateDate,
                    changeFrequency: ChangeFrequency.Daily,
                    priority: 0.8000);
                    }

                }
            }

            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
            #endregion
            #region Old
            //var categories = _categoryService.GetAllCategory();
            //var lstSiteMap = new List<SiteMapViewModel>();

            //foreach (var category in categories)
            //{
            //    var brands = _brandService.GetBrandByCategoryId(category.Id);
            //    foreach (var brand in brands)
            //    {
            //        var series = _productSeriService.GetProductSeriesByCategoryIdAndBrandId(category.Id, brand.Id);
            //        foreach (var productSeri in series)
            //        {
            //            lstSiteMap.Add(new SiteMapViewModel
            //            {
            //                Url = "https://farnaa.com/category/" + category.EnTitle.ToLower().Replace(" ", "-") + "/" + brand.EnTitle.ToLower().Trim().Replace(" ", "-") + "/" + productSeri.Title.ToLower().Trim().Replace(" ", "-"),
            //                LastMod = productSeri.LastUpdateDate.ToShortDateString() + "  " + productSeri.LastUpdateDate.ToShortTimeString()
            //            });
            //        }

            //    }
            //}


            //TempData["BackUrl"] = true;

            //return View("Index", lstSiteMap);
            #endregion

        }
    }
}
