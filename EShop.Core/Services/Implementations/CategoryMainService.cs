using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Seri;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Enum.Product;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class CategoryMainService : BaseService<CategotyMain>, ICategoryMainService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private ICategoryBrandPageService _categoryBrandPageService;
        public CategoryMainService(ApplicationDbContext context, IUserService userService, ICategoryBrandPageService categoryBrandPageService) :
            base(context)
        {
            _context = context;
            _userService = userService;
            _categoryBrandPageService = categoryBrandPageService;
        }


        public List<CategotyMain> GetListForAdmin()
        {
            return _context.CategotyMains.OrderByDescending(c => c.Id).ToList();
        }

        public List<CategotyMain> GetListForUser(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {

            return _context.CategotyMains.Where(c => !c.IsActive && c.TypeSystem == typeSystem).OrderBy(c => c.Order).ToList();

        }

        public CategotyMainViewModel GetCategotyMainDetail(long id)
        {
            var isUserColleague = _userService.IsUserColleague();


            var cat = _context.CategotyMains.Find(id);
            var listCategoryBrand = _categoryBrandPageService.GetListForUser();
            var result = new CategotyMainViewModel
            {
                Title = cat.Title,
                Text = cat.Text,
                Id = cat.Id,
                Brands = new List<Brand>(),
                CategotyMainProductsViewModels = new List<CategotyMainProductsViewModel>(),
                FAQSchema = cat.FAQSchema
            };


            var catProducts = new List<CategotyMainProductsViewModel>();
            var query = _context.Products
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.Variants)
                    .Where(c => c.IsPublished)
                    .OrderByDescending(c => c.Variants.Count).AsQueryable();




            if (cat.Type == EnumTypeCategotyMain.BySubCaegory)
            {
                query = (from q in query
                         join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                             on q.Id equals c.ProductId
                         select q);
                var quary2 = query.Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,
                    ImgName = s.ImgName,
                    MainPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count> 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                    DiscountPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                    ProductId = s.Id,
                    BrandId = s.BrandId,
                    CategoryId = s.CategoryId,
                    BrandTitle = s.Brand.FaTitle,
                    CategoryEnTitle = s.Category.EnTitle,
                    CategoryFaTitle = s.Category.FaTitle

                });
                var productsByCategory = quary2.ToList().GroupBy(c => c.CategoryId);
                foreach (var item in productsByCategory)
                {
                    var title = "";

                    long rowId = item.Key.Value; long? urlId = null;

                    CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                    {
                        Products = new List<ProductViewModel>()
                    };
                    foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                    {
                        title = t.CategoryEnTitle;
                        urlId = t.CategoryId;
                        productsViewModel.Products.Add(t);

                    }

                    productsViewModel.Title = title;
                    productsViewModel.CatId = urlId;
                    productsViewModel.CatEnTitle = cat.EnTitle;

                    if (productsViewModel.Products.Any())
                        catProducts.Add(productsViewModel);
                }

            }
            else
            {

                query = (from q in query
                         join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                             on q.Id equals c.ProductId
                         select q);
                var quary2 = query.Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,
                    ImgName = s.ImgName,
                    MainPrice =isUserColleague?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0 ,
                    DiscountPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                    ProductId = s.Id,
                    BrandId = s.BrandId,
                    CategoryId = s.CategoryId,
                    BrandTitle = s.Brand.FaTitle,
                    CategoryEnTitle = s.Category.EnTitle


                });
                var productsByCategory = quary2.ToList().GroupBy(c => c.BrandId);
                foreach (var item in productsByCategory)
                {
                    var title = "";
                    long rowId = item.Key.Value;
                    long? urlId = null;
                    CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                    {
                        Products = new List<ProductViewModel>()
                    };
                    foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                    {
                        title = t.BrandTitle;
                        urlId = t.BrandId;
                        productsViewModel.Products.Add(t);

                    }

                    productsViewModel.Title = title;
                    productsViewModel.BrandId = urlId;
                    productsViewModel.CatId = cat.CategoryId;

                    var existData = listCategoryBrand.FirstOrDefault(c =>
                        c.BrandId == productsViewModel.BrandId && c.CategoryId == productsViewModel.CatId);
                    if (existData != null)
                    {

                        productsViewModel.Url =
                            $"/main/{existData.CategoryId}-{existData.Brand.EnTitle}/{existData.EnTitle?.Replace(" ", "-")}";
                    }

                    if (productsViewModel.Products.Any())
                        catProducts.Add(productsViewModel);
                }


            }


            result.CategotyMainProductsViewModels = catProducts;

            return result;
        }

        public MainCategoryPageWithFilterViewModel MainCategoryWithFilter(long id, FilterDto dto,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {

            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();

                int skip = (dto.page - 1) * 32;

                if (dto.page == -1)
                    skip = 0;
                var cat = _context.CategotyMains.Find(id);
                var result = new MainCategoryPageWithFilterViewModel
                {
                    Title = cat.Title,
                    Text = cat.Text,
                    Id = cat.Id,
                    CategotyMainProductsViewModels = new List<CategotyMainProductsViewModel>(),
                    FAQSchema = cat.FAQSchema
                };


                var catProducts = new List<CategotyMainProductsViewModel>();
                var query = _context.Products
                    .Include(c => c.Category)
                    .Include(c => c.Brand)
                    .Include(c => c.Variants)
                        .Where(c => c.IsPublished)
                        .OrderByDescending(c => c.Variants.Count).AsQueryable();

                if (dto.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (dto.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPrice < c.Price && c.Count > 0));
                if (dto.brand != null && dto.brand.Count > 0)
                    query = query.Where(c => dto.brand.Contains(c.BrandId));



                switch (dto.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        query = query.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        query = query.OrderBy(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.ProductPublishDate:
                        query = query.OrderByDescending(c => c.Id);
                        break;
                    case EnumSortOnProducts.SellCount:
                        query = query.OrderByDescending(c => c.Sell);
                        break;
                }

                if (skip > 0)
                    query = query.Skip(skip);

                if (cat.Type == EnumTypeCategotyMain.BySubCaegory)
                {
                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                                 on q.Id equals c.ProductId
                             select q);
                    var quary2 = query.Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        ProductId = s.Id,
                        BrandId = s.BrandId,
                        CategoryId = s.CategoryId,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        PromotionPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Count > 0 && s.Variants.Any(v => v.Count > 0) ? s.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,

                    });

                    int? max_price = quary2.OrderByDescending(o => o.MainPrice).FirstOrDefault().MainPrice;
                    if (dto.max_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice <= dto.max_price);

                    if (dto.min_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice >= dto.min_price);

                    result.SideBarData = new SideBarViewModel(dto.min_price, max_price);
                    result.PaggingData = new PaggingViewModel() { Page = dto.page };
                    var productsByCategory = quary2.ToList().GroupBy(c => c.CategoryId);
                    foreach (var item in productsByCategory)
                    {
                        var title = "";
                        string enTitle = "";
                        long rowId = item.Key.Value; long? urlId = null;

                        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                        {
                            Products = new List<ProductViewModel>()
                        };
                        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                        {
                            title = t.CategoryFaTitle;
                            urlId = t.CategoryId;
                            productsViewModel.Products.Add(t);
                            enTitle = t.CategoryEnTitle;
                        }

                        productsViewModel.Title = title;
                        productsViewModel.CatId = urlId;
                        productsViewModel.CatEnTitle = enTitle;

                        if (productsViewModel.Products.Any())
                            catProducts.Add(productsViewModel);
                    }

                }
                else
                {

                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                                 on q.Id equals c.ProductId
                             select q);
                    var quary2 = query.Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        ProductId = s.Id,
                        BrandId = s.BrandId,
                        CategoryId = s.CategoryId,
                        BrandTitle = s.Brand.FaTitle,
                        BrandOrder = s.Brand.Order,
                        CategoryEnTitle = s.Category.EnTitle,
                        PromotionPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                            s.Variants.Count > 0 && s.Variants.Any(v => v.Count > 0) ? s.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,

                    });
                    int? max_price = quary2.OrderByDescending(o => o.MainPrice).FirstOrDefault().MainPrice;
                    if (dto.max_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice <= dto.max_price);

                    if (dto.min_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice >= dto.min_price);

                    result.SideBarData = new SideBarViewModel(dto.min_price, max_price);
                    result.PaggingData = new PaggingViewModel() { Page = dto.page };
                    var productsByCategory = quary2.AsEnumerable().GroupBy(c => c.BrandId);

                    var listCategoryBrand = _categoryBrandPageService.GetListForUser();

                    foreach (var item in productsByCategory)
                    {
                        var order = 1;
                        var title = "";
                        long rowId = item.Key.Value;
                        long? urlId = null;
                        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                        {
                            Products = new List<ProductViewModel>()
                        };
                        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                        {
                            title = t.BrandTitle;
                            urlId = t.BrandId;
                            order = t.BrandOrder;
                            productsViewModel.Products.Add(t);

                        }

                        productsViewModel.Title = title;
                        productsViewModel.BrandId = urlId;
                        productsViewModel.Order = order;
                        productsViewModel.CatId = cat.CategoryId;



                        var existData = listCategoryBrand.FirstOrDefault(c =>
                            c.BrandId == productsViewModel.BrandId && c.CategoryId == productsViewModel.CatId);
                        if (existData != null)
                        {
                            productsViewModel.Url =
                                $"/category/{cat.EnTitle.ToUrlFormat()}/{existData.Brand.EnTitle.ToUrlFormat()}";
                        }

                        if (productsViewModel.Products.Any())
                            catProducts.Add(productsViewModel);
                    }


                }


                result.CategotyMainProductsViewModels = catProducts.OrderBy(c => c.Order).ToList();
                return result;
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();

                int skip = (dto.page - 1) * 32;

                if (dto.page == -1)
                    skip = 0;
                var cat = _context.CategotyMains.Find(id);
                var result = new MainCategoryPageWithFilterViewModel
                {
                    Title = cat.Title,
                    Text = cat.Text,
                    Id = cat.Id,
                    CategotyMainProductsViewModels = new List<CategotyMainProductsViewModel>(),
                    FAQSchema = cat.FAQSchema
                };


                var catProducts = new List<CategotyMainProductsViewModel>();
                var query = _context.Products
                    .Include(c => c.Category)
                    .Include(c => c.Brand)
                    .Include(c => c.Variants)
                        .Where(c => c.IsPublished)
                        .OrderByDescending(c => c.Variants.Count).AsQueryable();

                if (dto.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (dto.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPlusPrice < c.PricePlus && c.Count > 0));
                if (dto.brand != null && dto.brand.Count > 0)
                    query = query.Where(c => dto.brand.Contains(c.BrandId));



                switch (dto.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        query = query.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        query = query.OrderBy(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.ProductPublishDate:
                        query = query.OrderByDescending(c => c.Id);
                        break;
                    case EnumSortOnProducts.SellCount:
                        query = query.OrderByDescending(c => c.Sell);
                        break;
                }

                if (skip > 0)
                    query = query.Skip(skip);

                if (cat.Type == EnumTypeCategotyMain.BySubCaegory)
                {
                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                                 on q.Id equals c.ProductId
                             select q);
                    var quary2 = query.Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        ProductId = s.Id,
                        BrandId = s.BrandId,
                        CategoryId = s.CategoryId,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        PromotionPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Count > 0 && s.Variants.Any(v => v.Count > 0) ? s.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,

                    });

                    int? max_price = quary2.OrderByDescending(o => o.MainPrice).FirstOrDefault().MainPrice;
                    if (dto.max_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice <= dto.max_price);

                    if (dto.min_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice >= dto.min_price);

                    result.SideBarData = new SideBarViewModel(dto.min_price, max_price);
                    result.PaggingData = new PaggingViewModel() { Page = dto.page };
                    var productsByCategory = quary2.ToList().GroupBy(c => c.CategoryId);
                    foreach (var item in productsByCategory)
                    {
                        var title = "";
                        string enTitle = "";
                        long rowId = item.Key.Value; long? urlId = null;

                        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                        {
                            Products = new List<ProductViewModel>()
                        };
                        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                        {
                            title = t.CategoryFaTitle;
                            urlId = t.CategoryId;
                            productsViewModel.Products.Add(t);
                            enTitle = t.CategoryEnTitle;
                        }

                        productsViewModel.Title = title;
                        productsViewModel.CatId = urlId;
                        productsViewModel.CatEnTitle = enTitle;

                        if (productsViewModel.Products.Any())
                            catProducts.Add(productsViewModel);
                    }

                }
                else
                {

                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
                                 on q.Id equals c.ProductId
                             select q);
                    var quary2 = query.Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        ProductId = s.Id,
                        BrandId = s.BrandId,
                        CategoryId = s.CategoryId,
                        BrandTitle = s.Brand.FaTitle,
                        BrandOrder = s.Brand.Order,
                        CategoryEnTitle = s.Category.EnTitle,
                        PromotionPrice = isUserColleague ?
                            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                            s.Variants.Count > 0 && s.Variants.Any(v => v.Count > 0) ? s.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,

                    });
                    int? max_price = quary2.OrderByDescending(o => o.MainPrice).FirstOrDefault().MainPrice;
                    if (dto.max_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice <= dto.max_price);

                    if (dto.min_price > 0)
                        quary2 = quary2.Where(c => c.PromotionPrice >= dto.min_price);

                    result.SideBarData = new SideBarViewModel(dto.min_price, max_price);
                    result.PaggingData = new PaggingViewModel() { Page = dto.page };
                    var productsByCategory = quary2.AsEnumerable().GroupBy(c => c.BrandId);

                    var listCategoryBrand = _categoryBrandPageService.GetListForUser();

                    foreach (var item in productsByCategory)
                    {
                        var order = 1;
                        var title = "";
                        long rowId = item.Key.Value;
                        long? urlId = null;
                        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
                        {
                            Products = new List<ProductViewModel>()
                        };
                        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
                        {
                            title = t.BrandTitle;
                            urlId = t.BrandId;
                            order = t.BrandOrder;
                            productsViewModel.Products.Add(t);

                        }

                        productsViewModel.Title = title;
                        productsViewModel.BrandId = urlId;
                        productsViewModel.Order = order;
                        productsViewModel.CatId = cat.CategoryId;



                        var existData = listCategoryBrand.FirstOrDefault(c =>
                            c.BrandId == productsViewModel.BrandId && c.CategoryId == productsViewModel.CatId);
                        if (existData != null)
                        {
                            productsViewModel.Url =
                                $"/category/{cat.EnTitle.ToUrlFormat()}/{existData.Brand.EnTitle.ToUrlFormat()}";
                        }

                        if (productsViewModel.Products.Any())
                            catProducts.Add(productsViewModel);
                    }


                }


                result.CategotyMainProductsViewModels = catProducts.OrderBy(c => c.Order).ToList();
                return result;
            }



        }

        public bool Add(CategotyMain categotyMain)
        {
            try
            {
                categotyMain.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(categotyMain);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(CategotyMain categotyMain)
        {
            try
            {
                categotyMain.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(categotyMain);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(CategotyMain categotyMain)
        {
            try
            {
                categotyMain.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(categotyMain);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CategotyMain FindSliderById(long id)
        {
            return _context.CategotyMains.Find(id);
        }


        public CategotyMainViewModel GetCategotyMainDetailGetByTitle(string _title, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            //var isUserColleague = _userService.IsUserColleague();

            var cat = _context.CategotyMains/*.Include(c => c.Category)*/.FirstOrDefault(t =>t.TypeSystem == typeSystem && t.EnTitle.Replace(" ", "-").ToLower() == _title.ToLower());
            if (cat == null) return null;
            //Alz Comments
            //var listCategoryBrand = _categoryBrandPageService.GetListForUser();
            var result = new CategotyMainViewModel
            {
                Title = cat.Title,
                Text = cat.Text,
                Id = cat.Id,
                Brands = new List<Brand>(),
                CategotyMainProductsViewModels = new List<CategotyMainProductsViewModel>(),
                EnTitle = _title,
                FAQSchema = cat.FAQSchema,
                //MetaTitle = !string.IsNullOrEmpty(cat.Category.MetaTitle)?cat.Category.MetaTitle:cat.Title,
               // Description = cat.Category.Descrption,
            };

            //Alz Comments
            //var catProducts = new List<CategotyMainProductsViewModel>();
            //var query = _context.Products
            //    .Include(c => c.Category)
            //    .Include(c => c.Brand)
            //    .Include(c => c.Variants)
            //        .Where(c => c.IsPublished)
            //        .OrderByDescending(c => c.Variants.Count).AsQueryable();


            //if (cat.Type == EnumTypeCategotyMain.BySubCaegory)
            //{
            //    query = (from q in query
            //             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId)
            //                 on q.Id equals c.ProductId
            //             select q);
            //    var quary2 = query.Select(s => new ProductViewModel
            //    {
            //        FaTitle = s.FaTitle,
            //        EnTitle = s.EnTitle ?? s.FaTitle,
            //        ImgName = s.ImgName,
            //        MainPrice = isUserColleague ?
            //            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
            //            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
            //        DiscountPrice = isUserColleague ?
            //            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
            //            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
            //        ProductId = s.Id,
            //        BrandId = s.BrandId,
            //        CategoryId = s.CategoryId,
            //        BrandTitle = s.Brand.FaTitle,
            //        CategoryEnTitle = s.Category.EnTitle,
            //        CategoryFaTitle = s.Category.FaTitle



            //    });
            //    var productsByCategory = quary2.ToList().GroupBy(c => c.CategoryId);
            //    foreach (var item in productsByCategory)
            //    {
            //        var title = "";
            //        string catEnTitle = "";
            //        long rowId = item.Key.Value; long? urlId = null;

            //        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
            //        {
            //            Products = new List<ProductViewModel>()
            //        };
            //        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
            //        {
            //            title = t.CategoryFaTitle;
            //            catEnTitle = t.CategoryEnTitle;
            //            urlId = t.CategoryId;
            //            productsViewModel.Products.Add(t);

            //        }

            //        productsViewModel.Title = title;
            //        productsViewModel.CatId = urlId;
            //        productsViewModel.CatEnTitle = catEnTitle.ToUrlFormat();
            //        if (productsViewModel.Products.Any())
            //            catProducts.Add(productsViewModel);
            //    }

            //}
            //else
            //{

            //    query = (from q in query
            //             join c in _context.ProductCategories.Where(c => c.CategoryId == cat.CategoryId).Include(c => c.Category)
            //                 on q.Id equals c.ProductId
            //             select q);
            //    var quary2 = query.Select(s => new ProductViewModel
            //    {
            //        FaTitle = s.FaTitle,
            //        EnTitle = s.EnTitle ?? s.FaTitle,
            //        ImgName = s.ImgName,
            //        MainPrice = isUserColleague ?
            //            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
            //            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
            //        DiscountPrice = isUserColleague ?
            //            s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
            //            s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
            //        ProductId = s.Id,
            //        BrandId = s.BrandId,
            //        CategoryId = s.CategoryId,
            //        BrandTitle = s.Brand.FaTitle,
            //        CategoryFaTitle = s.Category.FaTitle,
            //        CategoryEnTitle = s.Category.EnTitle,
            //        BrandEnTitle = s.Brand.EnTitle,
            //        BrandOrder = s.Brand.Order


            //    });
            //    var productsByCategory = quary2.ToList().GroupBy(c => c.BrandId);
            //    foreach (var item in productsByCategory)
            //    {
            //        string brandEnTitle = "";
            //        int order = 0;
            //        var title = "";
            //        long rowId = item.Key.Value;
            //        long? urlId = null;
            //        CategotyMainProductsViewModel productsViewModel = new CategotyMainProductsViewModel
            //        {
            //            Products = new List<ProductViewModel>()
            //        };
            //        foreach (var t in item.OrderByDescending(c => c.MainPrice).Take(16).ToList())
            //        {
            //            title = t.BrandTitle;
            //            urlId = t.BrandId;
            //            order = t.BrandOrder;
            //            productsViewModel.Products.Add(t);
            //            brandEnTitle = t.BrandEnTitle;

            //        }

            //        productsViewModel.Title = title;
            //        productsViewModel.BrandId = urlId;
            //        productsViewModel.CatId = cat.CategoryId;
            //        productsViewModel.BrandEnTitle = brandEnTitle;
            //        productsViewModel.Order = order;
            //        var existData = listCategoryBrand.FirstOrDefault(c =>
            //            c.BrandId == productsViewModel.BrandId && c.CategoryId == productsViewModel.CatId);
            //        productsViewModel.Url =
            //              $"/category/{cat.EnTitle.ToUrlFormat()}/{productsViewModel.BrandEnTitle.ToUrlFormat()}";
            //        if (existData != null)
            //        {


            //        }

            //        if (productsViewModel.Products.Any())
            //            catProducts.Add(productsViewModel);
            //    }


            //}


            //result.CategotyMainProductsViewModels = catProducts.OrderBy(c=>c.Order).ToList();

            return result;
        }

        public CategotyMain GetByTitle(string categoryName)
        {
            return _context.CategotyMains.Where(c => c.EnTitle.Replace(" ", "-").Equals(categoryName)).Include(c => c.Category)
                .ThenInclude(c => c.SubCategory).FirstOrDefault();
        }
    }
}
