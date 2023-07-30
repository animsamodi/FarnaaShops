using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EShop.Core.Services.Implementations
{
    public class IndexLayoutService : BaseService<IndexLayout>, IIndexLayoutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public IndexLayoutService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<IndexLayout> GetListForAdmin()
        {
            return _context.IndexLayouts.Include(c=>c.Category)
                .Include(c=>c.Category)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<IndexLayoutViewModel> GetListForUser(EnumTypeSystem typeSystem =EnumTypeSystem.Farnaa)
        {

            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();
                var result = new List<IndexLayoutViewModel>();
                var list = _context.IndexLayouts.Where(c => c.IsActive).ToList();
                IQueryable<DataLayer.Entities.Product.Product> lstProduct = null;
                if (isUserColleague)
                    lstProduct = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                        .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge && v.PriceColleague > 0)).AsQueryable();
                else
                    lstProduct = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                        .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0)).AsQueryable();

                foreach (var indexLayout in list)
                {
                    int count = indexLayout.Count;
                    IndexLayoutViewModel layoutViewModel = new IndexLayoutViewModel
                    {
                        BrandId = indexLayout.BrandId,
                        CategoryId = indexLayout.CategoryId,
                        Count = indexLayout.Count,
                        Id = indexLayout.Id,
                        Image1 = indexLayout.Image1,
                        Image2 = indexLayout.Image2,
                        Image3 = indexLayout.Image3,
                        Image4 = indexLayout.Image4,
                        ImageUrl1 = indexLayout.ImageUrl1,
                        ImageUrl2 = indexLayout.ImageUrl2,
                        ImageUrl3 = indexLayout.ImageUrl3,
                        ImageUrl4 = indexLayout.ImageUrl4,
                        Order = indexLayout.Order,
                        Sort = indexLayout.Sort,
                        Title = indexLayout.Title,
                        Type = indexLayout.Type,
                        Url = indexLayout.Url,
                        SideImage = indexLayout.SideImage
                    };
                    if (indexLayout.Type == EnumTypeLayotIndex.Product)
                    {
                        var products = lstProduct;
                        if (indexLayout.BrandId != null)
                        {
                            var brand = _context.Brands.Where(b => b.IsDelete == false && b.Id == indexLayout.BrandId).Select(b => b.EnTitle).SingleOrDefault();
                            products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Where(c => c.BrandId == indexLayout.BrandId.Value);
                            layoutViewModel.BrandTitle = brand;
                        }
                        if (indexLayout.CategoryId != null)
                        {
                            var cat = _context.Categories.Where(c => c.IsDelete == false && indexLayout.CategoryId == c.Id).Select(cat => new Category() { Id = cat.Id, EnTitle = cat.EnTitle, IsMain = cat.IsMain }).SingleOrDefault();
                            var subCat = _context.SubCategories.Include(c => c.ParentCategory).Where(c => c.IsDelete == false && c.SubId == cat.Id).FirstOrDefault();
                            if (subCat.ParentCategory != null)
                            {
                                if (subCat.ParentCategory.EnTitle != "All Products")
                                    layoutViewModel.ParentCategoryEnName = subCat.ParentCategory.EnTitle;
                            }
                            products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Where(c => c.CategoryId == indexLayout.CategoryId.Value);
                            layoutViewModel.CategoryTitle = cat.EnTitle;

                        }
                        switch (indexLayout.Sort)
                        {
                            case EnumSortLayoutIndex.New:
                                products = products.OrderByDescending(c => c.Id);
                                break;
                            case EnumSortLayoutIndex.Sell:
                                products = products.OrderByDescending(c => c.Sell);

                                break;
                            case EnumSortLayoutIndex.View:
                                products = products.OrderByDescending(c => c.View);

                                break;

                        }

                        products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Take(count);

                        if (products.Any())
                        {
                            layoutViewModel.Products = products.Select(s => new ProductViewModel
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
                                CategoryEnTitle = s.Category.EnTitle,
                                VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                                {
                                    Color = v.productOption.Value,
                                    Text = v.productOption.Name
                                }).ToList()

                            }).ToList();
                            result.Add(layoutViewModel);
                        }

                    }
                    else
                    {
                        result.Add(layoutViewModel);

                    }


                }
                return result.OrderBy(c => c.Order).ToList();
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();
                var result = new List<IndexLayoutViewModel>();
                var list = _context.IndexLayouts.Where(c => c.IsActive && c.TypeSystem == typeSystem).ToList();
                IQueryable<DataLayer.Entities.Product.Product> lstProduct = null;
                if (isUserColleague)
                    lstProduct = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                        .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge && v.PriceColleaguePlus > 0)).AsQueryable();
                else
                    lstProduct = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                        .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPlusPrice > 0)).AsQueryable();

                foreach (var indexLayout in list)
                {
                    int count = indexLayout.Count;
                    IndexLayoutViewModel layoutViewModel = new IndexLayoutViewModel
                    {
                        BrandId = indexLayout.BrandId,
                        CategoryId = indexLayout.CategoryId,
                        Count = indexLayout.Count,
                        Id = indexLayout.Id,
                        Image1 = indexLayout.Image1,
                        Image2 = indexLayout.Image2,
                        Image3 = indexLayout.Image3,
                        Image4 = indexLayout.Image4,
                        ImageUrl1 = indexLayout.ImageUrl1,
                        ImageUrl2 = indexLayout.ImageUrl2,
                        ImageUrl3 = indexLayout.ImageUrl3,
                        ImageUrl4 = indexLayout.ImageUrl4,
                        Order = indexLayout.Order,
                        Sort = indexLayout.Sort,
                        Title = indexLayout.Title,
                        Type = indexLayout.Type,
                        Url = indexLayout.Url,
                        SideImage = indexLayout.SideImage
                    };
                    if (indexLayout.Type == EnumTypeLayotIndex.Product)
                    {
                        var products = lstProduct;
                        if (indexLayout.BrandId != null)
                        {
                            var brand = _context.Brands.Where(b => b.IsDelete == false && b.Id == indexLayout.BrandId).Select(b => b.EnTitle).SingleOrDefault();
                            products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Where(c => c.BrandId == indexLayout.BrandId.Value);
                            layoutViewModel.BrandTitle = brand;
                        }
                        if (indexLayout.CategoryId != null)
                        {
                            var cat = _context.Categories.Where(c => c.IsDelete == false && indexLayout.CategoryId == c.Id).Select(cat => new Category() { Id = cat.Id, EnTitle = cat.EnTitle, IsMain = cat.IsMain }).SingleOrDefault();
                            var subCat = _context.SubCategories.Include(c => c.ParentCategory).Where(c => c.IsDelete == false && c.SubId == cat.Id).FirstOrDefault();
                            if (subCat.ParentCategory != null)
                            {
                                if (subCat.ParentCategory.EnTitle != "All Products")
                                    layoutViewModel.ParentCategoryEnName = subCat.ParentCategory.EnTitle;
                            }
                            products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Where(c => c.CategoryId == indexLayout.CategoryId.Value);
                            layoutViewModel.CategoryTitle = cat.EnTitle;

                        }
                        switch (indexLayout.Sort)
                        {
                            case EnumSortLayoutIndex.New:
                                products = products.OrderByDescending(c => c.Id);
                                break;
                            case EnumSortLayoutIndex.Sell:
                                products = products.OrderByDescending(c => c.Sell);

                                break;
                            case EnumSortLayoutIndex.View:
                                products = products.OrderByDescending(c => c.View);

                                break;

                        }

                        products = (IOrderedQueryable<DataLayer.Entities.Product.Product>)products.Take(count);

                        if (products.Any())
                        {
                            layoutViewModel.Products = products.Select(s => new ProductViewModel
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
                                CategoryEnTitle = s.Category.EnTitle,
                                VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                                {
                                    Color = v.productOption.Value,
                                    Text = v.productOption.Name
                                }).ToList()

                            }).ToList();
                            result.Add(layoutViewModel);
                        }

                    }
                    else
                    {
                        result.Add(layoutViewModel);

                    }


                }
                return result.OrderBy(c => c.Order).ToList();
            }

        }

        public bool Add(IndexLayout indexLayout)
        {
            try
            {
                indexLayout.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(indexLayout);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(IndexLayout indexLayout)
        {
            try
            {
                indexLayout.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(indexLayout);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(IndexLayout indexLayout)
        {
            try
            {
                indexLayout.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(indexLayout);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IndexLayout FindById(long id)
        {
            return _context.IndexLayouts.Find(id);
        }
    }
}
