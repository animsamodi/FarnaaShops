using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Page;
using EShop.Core.ViewModels.Product;
using EShop.Core.ViewModels.Seo;
using EShop.DataLayer.Enum.Product;
using Infrastructure.CacheHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Nest;
// ReSharper disable All

namespace EShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICategoryMainService _categoryMainService;
        private readonly ICategoryService _categoryService;
        private ICategoryBrandPageService _categoryBrandPageService;
        private readonly IProductService _productService;
        private IUserService _userService;
        private IProductSeriService _productSeriService;
        private IPageSeoService _pageSeoService;
        public CategoryController(IBrandService brandService, ICategoryMainService categoryMainService, ICategoryService categoryService, IProductService productService, ICategoryBrandPageService categoryBrandPageService, IUserService userService, IProductSeriService productSeriService, IPageSeoService pageSeoService)
        {
            _brandService = brandService;
            _categoryMainService = categoryMainService;
            _categoryService = categoryService;
            _productService = productService;
            _categoryBrandPageService = categoryBrandPageService;
            _userService = userService;
            _productSeriService = productSeriService;
            _pageSeoService = pageSeoService;
        }
        [Route("category/{id:long}/{title}")]
        [Route("category/{title}")]
        public IActionResult MainCategory(long id, string title, FilterDto filterDto)
        {
            CategoryPageDto pageDto = new CategoryPageDto
            {
                CategoryPageWithFilterViewModel = new MainCategoryPageWithFilterViewModel
                {
                    Category = new CategoryViewModel
                    {
                        ParentCategory = new CategoryViewModel()
                    },
                    CategotyMainProductsViewModels = new List<CategotyMainProductsViewModel>
                    {
                        new CategotyMainProductsViewModel
                        {
                            Products = new List<ProductViewModel>
                            {
                                new ProductViewModel
                                {
                                    Property = new List<ProductPromotionIndexPropertyViewModel>(),
                                    VariantColor = new List<VariantColorViewModel>()
                                }
                            }
                        }
                    },
                    SideBarData = new SideBarViewModel(),
                    PaggingData = new PaggingViewModel(),
                    
                }
            };
            if (id == 0 && filterDto.Seri == null && filterDto.availablestock == false && filterDto.discounted == false && filterDto.max_price == 0 && filterDto.max_price == 0 && filterDto.page == 1 && filterDto.sort == EnumSortOnProducts.MainPriceDesc && filterDto.brand == null)
            {
                if (filterDto.brand == null)
                {
                  
                        pageDto = getCategoryPageDto(id, title, filterDto);
                        if (!string.IsNullOrEmpty(pageDto.ReturnUrl))
                            return RedirectToAction(pageDto.ReturnUrl);
                    
                }
                else
                {
                    
                }
            }
            else
            {
                pageDto = getCategoryPageDto(id, title, filterDto);
                if (!string.IsNullOrEmpty(pageDto.ReturnUrl))
                    return RedirectToAction(pageDto.ReturnUrl);
            }

    
        

        ViewData["pageStructure"] = pageDto.Structure;
            ViewData["Title"] = pageDto.Structure?.PageTitle;
            return View(pageDto.CategoryPageWithFilterViewModel);

        }

        private CategoryPageDto getCategoryPageDto(long id, string title, FilterDto filterDto)
        {
            CategoryPageDto pageDto = new CategoryPageDto();

            CategotyMainViewModel category = new CategotyMainViewModel();
            var catId = _categoryService.GetCategoryIdByTitle(title.Replace("-", ""));
            if (id != default)
            {
                category = _categoryMainService.GetCategotyMainDetail(id);
                pageDto.ReturnUrl = Url.Action("MainCategory", "Category", new { title = category.EnTitle });
                return pageDto;
            }
            else
                category = _categoryMainService.GetCategotyMainDetailGetByTitle(title);
            //New Code 14010921
            if (category == null)
            {
                var parentId = _categoryService.GetParentCategory(catId).FirstOrDefault();
                if (parentId == 0)
                {
                    pageDto.ReturnUrl = Url.Action("Search", "Home");
                    return pageDto;

                }
                var parent = _categoryService.FindCategoryById(parentId);
                pageDto.ReturnUrl = Url.Action("SubCategory", new { categoryName = parent.EnTitle, title });
                return pageDto;


            }
            //
            var subcategories = _categoryService.GetSubCategoryById(catId);
            var categories = CreateCategoryHtml.GetCategoryTreeDropDownItems(subcategories);
            var active_item = catId;
            var brands = _brandService.GetBrandForFilterItems(catId);
            var result = _categoryMainService.MainCategoryWithFilter(category.Id, filterDto);

            //result.FAQSchema = category.FAQSchema;
            result.SideBarData.Brands = brands.Select(b => new BrandFilterItem(b.Id, b.EnTitle, b.FaTitle)).ToList();
            result.SideBarData.Categories = categories;
            result.Category = new CategoryViewModel(category.Id,
                category.Title, category.EnTitle, new CategoryViewModel(category.Id, category.Title, category.EnTitle));
            pageDto.CategoryPageWithFilterViewModel = result;
            pageDto.Structure =
                _pageSeoService.GetPageStructure(catId, brands.Count == 1 ? brands.FirstOrDefault().Id : null, null);

            return pageDto;
        }

        [Route("main/{key}/{title}")]
        public IActionResult CategoryBrand(string key, string title)
        {
            try
            {
                var data = key.Split('-');

                var res = _categoryBrandPageService.GetDataByCategoryAndBrand(Convert.ToInt64(data[0]), data[1]);
                if (res == null)
                    return RedirectToAction("Index");

                return RedirectToActionPermanent(nameof(SubCategory), new { title = res.Brand.EnTitle.ToUrlFormat(), categoryName = res.Category.EnTitle.ToUrlFormat() });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");

            }
        }



        [Route("category/{categoryName}/{title}")]
        [Route("category/{categoryName}/{title}/{seriName}")]
        public IActionResult SubCategory(string categoryName, string title, string seriName,
          [FromQuery] FilterDto filterDto)
        {


            try
            {

                var SelectedSeriFa = "";


                    ViewBag.PageNumber = filterDto.page;
                    int skip = (filterDto.page - 1) * 32;

                    if (filterDto.page == -1)
                        skip = 0;

                    var res = _categoryBrandPageService.GetDataByCategoryAndBrand(categoryName, title);

                    if (res != null)
                    {
                        filterDto.brand = new List<long?>() { res?.BrandId };
                    if (!string.IsNullOrEmpty(seriName))
                    {
                        var seriFa = _productSeriService.GetSeriByFaTitleAndBrandId(seriName,res.BrandId);
                        if (seriFa != null)
                            return RedirectToActionPermanent(nameof(SubCategory), new { categoryName = categoryName, title = title, seriName = seriFa.EnTitle.ToUrlFormat() });

                        var seri = _productSeriService.GetSeriByEnTitleAndBrandId(seriName, res.BrandId);
                        if (seri != null)
                        {
                            SelectedSeriFa = seri.Title;
                            if (filterDto.Seri == null)
                                filterDto.Seri = new List<long?>();
                            
                            filterDto.Seri.Add(seri.Id);
                        }
                    }

                    var result =
                            _productService.SearchInProductsWithFilterInCategory(filterDto, res.CategoryId.Value);
                        result.Category = new CategoryViewModel(res.Id,
                            res.FaTitle, res.EnTitle,
                            new CategoryViewModel(res.CategoryId.Value, res.Category.FaTitle, res.Category.EnTitle));
                        result.FAQSchema = res.FAQSchema;
                        result.SideBarData.Categories = new List<MainCategoryFilterItem>();
                        //
                        var series =
                            _productSeriService.GetProductSeriesByCategoryIdAndBrandId(res.Category.Id, res.BrandId);
                        result.SideBarData.Series =
                            series.Select(b => new SeriFilterItem(b.Id, b.EnTitle, b.Title)).ToList();

                        //
                        var parent = GetParent(res.Category.Id, GetCategories(), true);
                        if (filterDto.Seri != null && filterDto.Seri.Count == 1)
                        {
                            var pageStructure =
                                _pageSeoService.GetPageStructure(res.CategoryId, res.BrandId,
                                    filterDto.Seri.FirstOrDefault());
                            ViewData["pageStructure"] = pageStructure;
                            ViewData["Title"] = pageStructure?.PageTitle;

                            //var pageSeo = _pageSeoService.GetDataByItems(null, null, filterDto.Seri.FirstOrDefault());
                            //if (pageSeo != null)
                            //{
                            //    ViewData["pageStructure"] = new PageStructureViewModel(pageSeo.Text, pageSeo.MetaTitle, pageSeo.MetaDescription, false, pageSeo.MetaKeywords, pageSeo.Schema, pageSeo.Title);
                            //    ViewData["Title"] = pageSeo.MetaTitle;
                            //}
                            //else
                            //{
                            //    ViewData["pageStructure"] = new PageStructureViewModel(res.Text, res.MetaTitle, res.MetaDescription, false, res.MetaKeywords, res.Schema, res.FaTitle);
                            //    ViewData["Title"] = res.MetaTitle;
                            //}

                        }
                        else
                        {
                            //ViewData["pageStructure"] = new PageStructureViewModel(res.Text, res.MetaTitle, res.MetaDescription, false, res.MetaKeywords, res.Schema, res.FaTitle);
                            //ViewData["Title"] = res.MetaTitle;
                            var pageStructure =
                                _pageSeoService.GetPageStructure(res?.CategoryId, res?.BrandId, null);
                            ViewData["pageStructure"] = pageStructure;
                            ViewData["Title"] = pageStructure?.PageTitle;
                        }
                        //    var isSeri = _productSeriService.GetSeriByTitle(seriName);
                        //    var isBrnad = _brandService.GetBrandByTitle(seriName);
                        //    if (isSeri != null || isBrnad == null)
                        //    {
                        //        isSearchByBrand = false;
                        //    }
                    result.SelectedCategory = categoryName;
                        result.SelectedBrand = title;
                         
                        result.SelectedSeri = seriName;
                        result.SelectedSeriFa = SelectedSeriFa;

                        return View(nameof(SubCategory), result);
                    }
                    else
                    {
                        long catId = 0;
                        string catFAQSchema = "";
                        string catFaTitle = "";
                        string catEnTitle = "";
                        string catDescrption = "";
                        string catMetaTitle = "";
                        var category = _categoryService.GetCategoryByTitle(title);
                        var parent = _categoryService.GetCategoryByTitle(categoryName);
                        if (category != null)
                        {
                            catId = category.Id;
                            catFAQSchema = category.FAQSchema;
                            catFaTitle = category.FaTitle;
                            catEnTitle = category.EnTitle;
                            catDescrption = category.Descrption;
                            catMetaTitle = category.MetaTitle;
                        }
                        else if (parent != null)
                        {
                            catId = parent.Id;
                            catFAQSchema = parent.FAQSchema;
                            catFaTitle = parent.FaTitle;
                            catEnTitle = parent.EnTitle;
                            catDescrption = parent.Descrption;
                            catMetaTitle = parent.MetaTitle;
                        }
                        else
                        {
                            return RedirectToAction("Index");

                        }

                        var subcategories = _categoryService.GetSubCategoryById(parent.Id);
                        var categories = CreateCategoryHtml.GetCategoryTreeDropDownItems(subcategories);
                        var active_item = catId;
                        var brands = _brandService.GetBrandForFilterItems(catId);
                        var series = _productSeriService.GetProductSeriesByCategoryIdAndBrandId(catId, res?.BrandId);
                        var selectedBrand = "";
                        if (!string.IsNullOrEmpty(seriName))
                        {
                            var isBrnad = _brandService.GetBrandByTitle(seriName);
                           
                             if (isBrnad != null)
                            {
                                if (filterDto.brand == null)
                                    filterDto.brand = new List<long?>();
                                filterDto.brand.Add(isBrnad.Id);
                                selectedBrand = seriName;

                        }
                    }


                    var result = _productService.SearchInProductsWithFilterInCategory(filterDto, catId);

                        result.FAQSchema = catFAQSchema;
                        result.SideBarData.Brands = brands.Select(b => new BrandFilterItem(b.Id, b.EnTitle, b.FaTitle))
                            .ToList();
                        result.SideBarData.Series =
                            series.Select(b => new SeriFilterItem(b.Id, b.EnTitle, b.Title)).ToList();
                        result.SideBarData.Categories = categories;
                        result.Category = new CategoryViewModel(catId,
                            catFaTitle, catEnTitle, new CategoryViewModel(parent.Id, parent.FaTitle, parent.EnTitle));

                        //ViewData["pageStructure"] = new PageStructureViewModel(catFaTitle,
                        //    catMetaTitle,
                        //  catDescrption,
                        //  catFAQSchema);
                        long? strucBrand = res?.BrandId;
                        if (strucBrand == null && filterDto.brand != null && filterDto.brand.Count == 1)
                            strucBrand = filterDto.brand.FirstOrDefault();
                        var pageStructure =
                            _pageSeoService.GetPageStructure(catId, strucBrand, null);
                        ViewData["pageStructure"] = pageStructure;
                        ViewData["Title"] = pageStructure?.PageTitle;
                    //
                    result.SelectedParentCategory = categoryName;
                    result.SelectedCategory = title;
                    if(!string.IsNullOrEmpty(selectedBrand))
                        result.SelectedBrand = selectedBrand;

                    return View(nameof(SubCategory), result);
                    }
               
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");

            }
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

        private List<SearchCategoryViewModel> GetCategories()
        {
            var subcategory = _categoryService.GetAllSubCategory();
            var category = _categoryService.GetAllCategory();

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

                        CategoryEnTitle = item.EnTitle
                    });
                }
            }
            return categories;
        }

    }
}