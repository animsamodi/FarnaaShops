using EndPoint.Web.Utilities;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.Web.Areas.User.Controllers;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EShop.DataLayer.Entities.Seri;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using EShop.Core.Services.Implementations;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IPropertyService _propertyService;
        private readonly IBannerService _bannerService;
        private readonly IStaticPageService _staticPageService;
        private readonly ICategoryMainService _categoryMainService;
        private ICategoryBrandPageService _categoryBrandPageService;
        private IUserService _userService;
        private ISiteSettingService _siteSettingService;
        private IProductPricePageService _productPricePageService;
        //
        private IFaqService _faqService;
        private IContactService _contactService;
     
        public HomeController(IBrandService brandService, ICategoryService categoryService, IProductService productService, IPropertyService propertyService, IBannerService bannerService, IStaticPageService staticPageService, ICategoryMainService categoryMainService, ICategoryBrandPageService categoryBrandPageService, IUserService userService, ISiteSettingService siteSettingService, IProductPricePageService productPricePageService, IFaqService faqService, IContactService contactService)
        {
            _brandService = brandService;
            _categoryService = categoryService;
            _productService = productService;
            _propertyService = propertyService;
            _bannerService = bannerService;
            _staticPageService = staticPageService;
            _categoryMainService = categoryMainService;
            _categoryBrandPageService = categoryBrandPageService;
            _userService = userService;
            _siteSettingService = siteSettingService;
            _productPricePageService = productPricePageService;
            _faqService = faqService;
            _contactService = contactService;
   
        }

        public IActionResult Index()
        {
            //return Redirect(string.Format("{0}/Purchase/Index?token={1}", "https://sadad.shaparak.ir", "000000111124525252"));


            if (ClaimUtility.IsColleauge(User))
                return RedirectToAction("IndexColleauge");
            ViewData["schema"] = SchemaHelper.WebSiteSchema;


            //_userService.CheckUserFullName();
            string KeyBannerImage = "BannerImageChache";
            List<BannerImageViewModel> bannerImage = new List<BannerImageViewModel>();
         
                bannerImage = _bannerService.GetListBanner();
            
            ViewBag.BannerImage = bannerImage;
            //ViewBag.Suggest = _productService.GetProductSuggest();

            string HomeText ="";
         
                HomeText =  _siteSettingService.GetSiteSeoSetting().HomeText.ToLazyLoadImage();
         


            ViewBag.HomeFotterText = HomeText;
            return View();
        }

        public IActionResult MainCategory(string title)
        {
            return RedirectToAction("MainCategory", "Category", new { title });
        }

        public IActionResult SubCategory(string categoryName, string title)
        {
            return RedirectToAction("SubCategory", "Category", new { title, categoryName });
        }

        public IActionResult IndexColleauge([FromQuery] SearchWithFilterDto filterDto)
        {

            int skip = (filterDto.page - 1) * 32;

            if (filterDto.page == -1)
                skip = 0;

            //TODO Clean this section
            filterDto.JustColleauge = true;
            ProductSearchWithFilterViewModel res = _productService.SearchPage(filterDto);

            if (filterDto.q != null && res.Products.Count == 1 && res.Products.FirstOrDefault().FaTitle.Contains(filterDto.q))
                return RedirectToAction("Index", "Product",
                    new { id = res.Products.FirstOrDefault().ProductId, name = res.Products.FirstOrDefault().EnTitle.ToUrlFormat() ?? res.Products.FirstOrDefault().FaTitle.ToUrlFormat() });
            string query = "";
            if (!String.IsNullOrEmpty(filterDto.q))
                query = "&q=" + filterDto.q;

            var disByCategory = res.Products.Select(c => c.CategoryId).Distinct();
            if (disByCategory.Count() == 1)
            {
                filterDto.catId = disByCategory.ToList();
                query += "&catid=" + filterDto.catId.First();
            }

            if (filterDto.catId != null && filterDto.catId.Count > 0)
            {
                //TODO Add Category filter
                // var t = _brandService.GetBrandByCategoryId(filterDto.catId);
                // ViewData["brand"] = _brandService.GetBrandByCategoryId(filterDto.catId);
                // ViewData["property"] = _propertyService.GetPropertyForSearch(filterDto.catId);
                // var parent = GetParent(filterDto.catId, GetCategories(), true);
                // ViewData["category"] = CreateCategoryHtml.GetHtmlCategory(parent, filterDto.catId, null, query);
            }

            var categories = GetAllProductsCategories();
            res.SideBarData.Brands = _brandService.GetAllBrands()
                .Select(b => new BrandFilterItem(b.Id, b.EnTitle, b.FaTitle)).ToList();
            res.SideBarData.Categories = categories;
            res.Category = null;

            return View(res);

        }




        [Route("compare/{id}")]
        [Route("compare/{id}/{id2}")]
        [Route("compare/{id}/{id2}/{id3}")]
        [Route("compare/{id}/{id2}/{id3}/{id4}")]
        public IActionResult Compare(long id, long? id2, long? id3, long? id4)
        {
            List<long?> idlist = new List<long?> { id, id2, id3, id4 };
            var temp = _productService.GetProductForCompare(idlist);
            if (temp.Count > 0)
            {
                var products = temp.GroupBy(c => c.ProductId).Select(r => new CompareViewModel
                {
                    FaTitle = r.FirstOrDefault().FaTitle,
                    EnTitle = r.FirstOrDefault().EnTitle ?? r.FirstOrDefault().FaTitle,
                    CategoryId = r.FirstOrDefault().CategoryId,
                    Price = r.Where(c => c.Price != 0).Min(c => c.Price),
                    ProductId = r.Key,
                    Gallery = r.FirstOrDefault().Gallery,
                    Properties = r.FirstOrDefault().Properties
                }).ToList();
                foreach (var item in products)
                {
                    if (item.CategoryId != products[0].CategoryId)
                        return View();
                }


                var propertycategory = _propertyService.GetProductGroupForCompare(products[0].CategoryId);
                var category = _categoryService.FindCategoryById(products[0].CategoryId).FaTitle;
                var t = products.OrderBy(c => idlist.IndexOf(c.ProductId)).ToList();
                return View(Tuple.Create(t, propertycategory, category));
            }
            return View();
        }


        public IActionResult MobileCategory()
        {
            var res = _categoryMainService.GetListForUser();

            return View(res);
        }

        [HttpPost]
        public IActionResult GetBrandForCompare(int id)
        {
            return Json(_brandService.GetBrandByCategoryId(id));
        }

        [HttpPost]
        public IActionResult GetProductForCompareSearch(int brandid = 0, string name = "", int catid = 0)
        {
            if (name == null)
                name = "";
            if (brandid == 0)
                return View(_productService.GetProductForCompareByName(name, catid));
            return View(_productService.GetProductForCompareByBrandId(brandid, name, catid));
        }

        [HttpPost]
        public IActionResult GetProductForCompare(int catid)
        {
            var query = _productService.GetProductForCompare(catid);
            return View("~/Views/Home/GetProductForCompareSearch.cshtml", query);
        }


        [SanitizeInput]
        [Route("HeaderSearch2")]
        public IActionResult HeaderSearch2()
        {
            var htmlsanitaizer = new HtmlSanitizer();

            var name = htmlsanitaizer.Sanitize(HttpContext.Request.Query["term"].ToString());
            var names = _productService.HeaderSearch2(name);
            var searchtext = '"' + name + '*' + '"';
            //var query = _productService.HeaderSearch(searchtext).Select(c => c.OtherTitle).ToList();
            //return Ok(query);
            return Ok();
        }

        [SanitizeInput]
        [Route("HeaderSearch")]
        public IActionResult HeaderSearch(string q)
        {

            var searchtext = '"' + q + '*' + '"';
            var query = _productService.HeaderSearch(searchtext);
            if (query.Count <= 0)
                return Json(new { status = false });


            List<HeaderSearchSimilarCategoryViewModel> category = new List<HeaderSearchSimilarCategoryViewModel>();
            List<string> suggest = new List<string>();

            if (q.TrimStart().Contains(' '))
            {
                foreach (var item in query)
                {
                    item.OtherTitle.Replace(',', ' ');
                    Regex r = new Regex(@"(\w*" + q + @"\w*)");
                    MatchCollection mc = r.Matches(item.OtherTitle);

                    for (int i = 0; i < mc.Count; i++)
                    {
                        if (mc[i].Value.Contains(q))
                        {
                            if (!category.Any(c => c.CategoryName == item.CategoryName))
                            {
                                category.Add(new HeaderSearchSimilarCategoryViewModel
                                {
                                    CategoryName = item.CategoryName,
                                    Word = mc[i].Value
                                });
                            }

                            if (!suggest.Any(c => c == mc[i].Value))
                            {
                                suggest.Add(mc[i].Value);
                                goto end;
                            }
                        }
                    }
                end:
                    continue;
                }
            }
            else
            {
                foreach (var item in query)
                {
                    item.OtherTitle.Replace(',', ' ');
                    var split = item.OtherTitle.Split(' ');
                    foreach (var split_item in split)
                    {
                        if (split_item.Contains(q))
                        {
                            if (!category.Any(c => c.CategoryName == item.CategoryName))
                            {
                                category.Add(new HeaderSearchSimilarCategoryViewModel
                                {
                                    CategoryName = item.CategoryName,
                                    Word = split_item
                                });
                            }

                            if (!suggest.Any(c => c == split_item))
                            {
                                suggest.Add(split_item);
                                goto end;
                            }
                        }
                    }

                end:
                    continue;
                }

            }

            return Json(new { status = true, res = category, suggest = suggest });
        }
        [HttpPost]
        public IActionResult SearchFilterIndex(int catId, int min_price, int max_price)
        {
            return RedirectToAction(nameof(Search), new { catId, min_price, max_price });
        }



        [SanitizeInput]
        [Route("Search")]
        public IActionResult Search([FromQuery] SearchWithFilterDto filterDto)
        {
            if (ClaimUtility.IsColleauge(User))
                return RedirectToAction("IndexColleauge", filterDto);

            int skip = (filterDto.page - 1) * 32;

            if (filterDto.page == -1)
                skip = 0;

            //TODO Clean this section
            ProductSearchWithFilterViewModel res = _productService.SearchPage(filterDto);

            if (filterDto.q != null && res.Products.Count == 1 && res.Products.FirstOrDefault().FaTitle.Contains(filterDto.q))
                return RedirectToAction("Index", "Product",
                    new { id = res.Products.FirstOrDefault().ProductId, name = res.Products.FirstOrDefault().EnTitle.ToUrlFormat() ?? res.Products.FirstOrDefault().FaTitle.ToUrlFormat() });
            string query = "";
            if (!String.IsNullOrEmpty(filterDto.q))
                query = "&q=" + filterDto.q;

            var disByCategory = res.Products.Select(c => c.CategoryId).Distinct();
            if (disByCategory.Count() == 1)
            {
                filterDto.catId = disByCategory.ToList();
                query += "&catid=" + filterDto.catId.First();
            }

            if (filterDto.catId != null && filterDto.catId.Count > 0)
            {
                //TODO Add Category filter
                // var t = _brandService.GetBrandByCategoryId(filterDto.catId);
                // ViewData["brand"] = _brandService.GetBrandByCategoryId(filterDto.catId);
                // ViewData["property"] = _propertyService.GetPropertyForSearch(filterDto.catId);
                // var parent = GetParent(filterDto.catId, GetCategories(), true);
                // ViewData["category"] = CreateCategoryHtml.GetHtmlCategory(parent, filterDto.catId, null, query);
            }

            var categories = GetAllProductsCategories();
            res.SideBarData.Brands = _brandService.GetAllBrands()
                .Select(b => new BrandFilterItem(b.Id, b.EnTitle, b.FaTitle)).ToList();
            res.SideBarData.Categories = categories;
            res.Category = null;
            ViewBag.SearchText = filterDto.q;
            return View(res);

        }




        private List<MainCategoryFilterItem> GetAllProductsCategories()
        {
            var mainCategoryFilterItems = new List<MainCategoryFilterItem>();

            var All = GetCategories();
            var farnaaProductsSubcat = All.Where(p => p.ParentId == 29).ToList();
            foreach (var cat in farnaaProductsSubcat)
            {
                var subs = All.Where(p => p.ParentId == cat.CategoryId).Select(
                    s => new CategoryFilterItem(s.CategoryId, s.CategoryFaTitle, s.CategoryEnTitle)).ToList();

                mainCategoryFilterItems.Add(new MainCategoryFilterItem(cat.CategoryId, cat.CategoryEnTitle,
                    cat.CategoryFaTitle, subs));
            }

            return mainCategoryFilterItems;
        }

        private List<SearchCategoryViewModel> GetCategories()
        {
            var subcategory = _categoryService.GetAllSubCategory();
            var category = _categoryService.GetAllCategory();

            List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            foreach (var item in category.Where(q => q.IsDelete == false))
            {
                var subid = subcategory.Where(c => c.SubId == item.Id && !c.IsDelete);

                if (subid.Count() > 0)
                {
                    foreach (var item2 in subid)
                    {
                        categories.Add(new SearchCategoryViewModel
                        {
                            CategoryId = item.Id,
                            ParentId = item2.ParentId,
                            Title = item.FaTitle,
                            CategoryEnTitle = item.EnTitle,
                            CategoryFaTitle = item.FaTitle
                        });
                    }
                }
                else
                {
                    categories.Add(new SearchCategoryViewModel
                    {
                        CategoryId = item.Id,
                        ParentId = null,
                        Title = item.FaTitle,
                        CategoryFaTitle = item.FaTitle,
                        CategoryEnTitle = item.EnTitle
                    });
                }
            }
            return categories;
        }

        private List<SearchCategoryViewModel> GetCategoriesMobile()
        {
            var subcategory = _categoryService.GetAllSubCategory();
            var category = _categoryService.GetAllCategory().Where(c => c.Id != 29);

            List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            foreach (var item in category)
            {
                var subid = subcategory.Where(c => c.SubId == item.Id);

                if (subid.Count() > 0)
                {
                    foreach (var item2 in subid)
                    {
                        categories.Add(new SearchCategoryViewModel
                        {
                            CategoryId = item.Id,
                            ParentId = item2.ParentId,
                            Title = item.FaTitle,
                            haveChild = subcategory.Any(c => c.ParentId == item.Id),
                            CategoryFaTitle = item.FaTitle,
                            CategoryEnTitle = item.EnTitle
                        });
                    }
                }
                else
                {
                    categories.Add(new SearchCategoryViewModel
                    {
                        CategoryId = item.Id,
                        ParentId = null,
                        Title = item.FaTitle,
                        haveChild = subcategory.Any(c => c.ParentId == item.Id),
                        CategoryEnTitle = item.EnTitle
                    });
                }
            }
            return categories;
        }


        private List<SearchCategoryViewModel> GetParent(long? categoryid, List<SearchCategoryViewModel> value, bool is_first)
        {
            List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            if (is_first)
            {
                var child = value.Where(c => c.ParentId == categoryid);
                if (child.Count() > 0)
                    categories.AddRange(child);
            }

            var res = value.FirstOrDefault(c => c.CategoryId == categoryid);
            if (res != null)
            {
                categories.Add(new SearchCategoryViewModel
                {
                    CategoryId = res.CategoryId,
                    ParentId = res.ParentId,
                    Title = res.Title,
                    CategoryEnTitle = res.CategoryEnTitle,

                });

                if (res.ParentId != null)
                    categories.AddRange(GetParent(res.ParentId, value, false));
            }


            return categories;
        }

        public List<SearchCategoryViewModel> GetParentList(List<long> categoryid, List<SearchCategoryViewModel> value)
        {
            List<SearchCategoryViewModel> categories = new List<SearchCategoryViewModel>();

            foreach (var item in categoryid)
            {
                var res = value.FirstOrDefault(c => c.CategoryId == item);
                if (res != null)
                {
                    categories.Add(new SearchCategoryViewModel
                    {
                        CategoryId = res.CategoryId,
                        ParentId = res.ParentId,
                        Title = res.Title,
                        CategoryFaTitle = res.CategoryFaTitle,
                        CategoryEnTitle = res.CategoryEnTitle
                    });

                    if (res.ParentId != null)
                        categories.AddRange(GetParent(res.ParentId, value, false));
                }
            }

            return categories;
        }



        [Route("Page/{title}")]
        public IActionResult Page(string title)
        {
            var content = _staticPageService.GetStaticPageByTitle(title);
            return View(content);
        }


        public IActionResult ListCategoryBrandProduct(int brnad = 0, int category = 0, int pagenumber = 1)
        {
            int skip = (pagenumber - 1) * 32;

            var content = _productService.SearchPage(new SearchWithFilterDto() { page = pagenumber });
            ViewBag.count = content.Products.Count.ToString();
            ViewBag.brnad = brnad;
            ViewBag.category = category;
            ViewBag.PageNumber = pagenumber;
            return RedirectToAction("Index", "Home");
            //  return View( content.Products);
        }


        [Route("PriceList/{category}")]
        [Route("PriceList/{category}/{brand}")]
        [Route("PriceList/{category}/{brand}/{seri}")]
        public IActionResult PriceList(string category, string brand, string seri)
        {
            var productPricePage = _productPricePageService.GetPriceListDetails(category, brand, seri);
            if (productPricePage == null)
                return RedirectToAction("Index");

            var productList = _productService.GetProductListPrice(productPricePage.CategoryId, productPricePage.BrandId,
                productPricePage.SeriId, productPricePage.Order);
            return View(new Tuple<ProductPricePage, List<ProductViewModel>>(productPricePage, productList));
        }


        [Route("contact-us")]
        public IActionResult ContactUs()
        {
            var res = _contactService.GetActiveRow();
            return View(res);
        }
        [Route("faq")]
        public IActionResult Faq()
        {
            var res = _faqService.GetListForUser();
            return View(res);
        }
    }
}