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
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.QueryModel;
using Microsoft.EntityFrameworkCore;
using EShop.DataLayer.Entities.Category;
using System.Threading.Tasks;
using AngleSharp.Common;
using EShop.DataLayer.Enum.Product;
using EShop.DataLayer.Enum;
using EShop.Core.ViewModels.Api;
using System.Security.Cryptography;

namespace EShop.Core.Services.Implementations.Product
{
    public class ProductService : BaseService<EShop.DataLayer.Entities.Product.Product>, IProductService
    {
        #region constructor

        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public ProductService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }

        #endregion


        public ProductDetailUserViewModel GetProductDetailUser(long id, long userId)
        {
            var t = _context.Products.AsQueryable()
                .Where(c => c.Id == id).ToList();

            var res = _context.Products.AsQueryable()
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.ProductProperties)
                .Include(c => c.ProductImages).ThenInclude(i => i.ProductOption)
                .Where(c => c.Id == id)
                .Select(s => new ProductDetailUserViewModel
                {
                    ProductId = s.Id,
                    BrandId = s.BrandId,
                    BrandEnTitle = s.Brand.EnTitle,
                    BrandImage = s.Brand.ImgName,
                    EnTitle = s.EnTitle ?? s.FaTitle,
                    FaTitle = s.FaTitle,
                    ImgName = s.ImgName,
                    KeyWord = s.KeyWord + "," + s.Brand.KeyWord + "," + s.Category.KeyWord,
                    CategoryName = s.Category.FaTitle,
                    Catid = s.CategoryId,
                    BrandName = s.Brand.FaTitle,
                    Gallery = s.ProductImages.Select(c => new ProductGallleyViewModel() { ImgUrl = c.ImgName, ProductOptionId = c.ProductOptionId }).ToList(),
                    MetaTitle = s.MetaTitle,
                    MetaDescription = s.MetaDescription,
                    MetaKeywords = s.MetaKeywords,
                    Canonical = s.Canonical,
                    HeaderTag = s.HeaderTag,
                    IsShowPopUp = s.IsShowPopUp,
                    Have3dFile = s.Have3dFile,
                    PopUpContent = s.PopUpContent,
                    Schema = s.Schema,
                    BaseSchema = s.BaseSchema,
                    FAQSchema = s.FAQSchema
                }).SingleOrDefault();
            if (res != null)
            {
                //if (userId != 0)
                //{

                //    res.IsFovorite =
                //        _context.UserProductFovoriteses.Any(c => c.ProductId == res.ProductId && c.UserId == userId);
                //}

                res.ProductProperty = GetProperty(id);
            }



            return res;
        }
        public Tuple<int, List<ProductListViewModel>> GetProductsForAdminColleague(string searchtext, int pagenumber, int brnad, int category, int state, int take)
        {
            int skip = (pagenumber - 1) * take;
            IQueryable<ProductListViewModel> query = _context.Products
                .Include(c => c.Variants)
                .Include(c => c.Brand)
                .Include(c => c.Category).AsQueryable()
                .OrderByDescending(c => c.Id)
                .Where(p => EF.Functions.Like(p.EnTitle, "%" + searchtext + "%") || EF.Functions.Like(p.FaTitle, "%" + searchtext + "%"))
                .Select(p => new ProductListViewModel
                {
                    Id = p.Id,
                    FaTitle = p.FaTitle,
                    Image = p.ImgName,
                    SpecCode = p.SpecCode,
                    CommodityId = p.CommodityId,
                    TolatCount = p.Variants.Sum(c => c.Count),
                    CategoryTitle = p.Category.FaTitle,
                    BrnadTitle = p.Brand.FaTitle,
                    BrnadId = p.Brand.Id,
                    CategoryId = p.Category.Id,
                    DefaultPrice = p.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderByDescending(c => c.PriceColleague).FirstOrDefault() != null ? p.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderByDescending(c => c.PriceColleague).FirstOrDefault().PriceColleague.ToString("N0") : "0",
                    IsExist = p.Variants.Sum(c => c.Count) > 0,
                    IsAvailablesoon = p.IsAvailablesoon
                }).OrderByDescending(c => c.TolatCount);
            if (brnad != 0)
                query = query.Where(c => c.BrnadId == brnad);
            if (category != 0)
                query = query.Where(c => c.CategoryId == category);
            if (state != 0)
            {
                var isExist = state == 1;

                query = query.Where(c => c.IsExist == isExist);
            }
            return Tuple.Create(query.Count(), query.Skip(skip).Take(take).ToList());
        }
        public Tuple<int, List<ProductListViewModel>> GetProductsForAdminPlus(string searchtext, int pagenumber, int brnad, int category, int state, int take)
        {
            int skip = (pagenumber - 1) * take;
            IQueryable<ProductListViewModel> query = _context.Products
                .Include(c => c.Variants)
                .Include(c => c.Brand)
                .Include(c => c.Category).AsQueryable()
                .OrderByDescending(c => c.Id)
                .Where(p => EF.Functions.Like(p.EnTitle, "%" + searchtext + "%") || EF.Functions.Like(p.FaTitle, "%" + searchtext + "%"))
                .Select(p => new ProductListViewModel
                {
                    Id = p.Id,
                    FaTitle = p.FaTitle,
                    Image = p.ImgName,
                    SpecCode = p.SpecCode,
                    CommodityId = p.CommodityId,
                    TolatCount = p.Variants.Sum(c => c.Count),
                    CategoryTitle = p.Category.FaTitle,
                    BrnadTitle = p.Brand.FaTitle,
                    BrnadId = p.Brand.Id,
                    CategoryId = p.Category.Id,
                    DefaultPrice = p.Variants.Where(v => v.Count > 0 && v.PricePlus > 0).OrderByDescending(c => c.PricePlus).FirstOrDefault() != null ? p.Variants.Where(v => v.Count > 0 && v.PricePlus > 0).OrderByDescending(c => c.PricePlus).FirstOrDefault().PricePlus.ToString("N0") : "0",
                    IsExist = p.Variants.Sum(c => c.Count) > 0,
                    IsAvailablesoon = p.IsAvailablesoon
                }).OrderByDescending(c => c.TolatCount);
            if (brnad != 0)
                query = query.Where(c => c.BrnadId == brnad);
            if (category != 0)
                query = query.Where(c => c.CategoryId == category);
            if (state != 0)
            {
                var isExist = state == 1;

                query = query.Where(c => c.IsExist == isExist);
            }
            return Tuple.Create(query.Count(), query.Skip(skip).Take(take).ToList());
        }

        public ProductReviewViewModel GetProductReview(long productId, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            var review = _context.ProductReviews.AsQueryable()
                .Where(c => c.ProductId == productId && c.TypeSystem == typeSystem).Select(
                    c => new ProductReviewViewModel
                    {
                        Review = c.Review.ToLazyLoadImage(),
                        ShortReview = c.ShortReview.ToLazyLoadImage(),
                        Suammry = c.Summary.ToLazyLoadImage(),
                        Psitive = c.Positive,
                        Negative = c.Negative,

                    }).FirstOrDefault() ?? new ProductReviewViewModel();

            var rating = _context.ProductReviewRatings.AsQueryable()
                .Include(c => c.RatingAttribute)
                .Where(c => c.ProductId == productId)
                .Select(c => new ReviewRatingViewModel
                {
                    Title = c.RatingAttribute.Title,
                    Value = c.Value
                }).ToList();


            review.ReviewRatingViewModels = rating;
            return review;

        }

        public List<ProductPropertyUserViewModel> GetProperty(long productId)
        {
            var res = _context.ProductProperties.AsQueryable()
                .Include(c => c.PropertyValue)
                .ThenInclude(c => c.PropertyName)

                .ThenInclude(c => c.PropertyGroup)
                .Where(c => c.ProductId == productId)
                 .Select(c => new ProductPropertyUserViewModel
                 {
                     GroupName = c.PropertyValue.PropertyName.PropertyGroup.Title,
                     PropertyName = c.PropertyValue.PropertyName.Title,
                     PropertValue = c.PropertyValue.Value,
                     OrderGroup = 1,
                     OrderPropertyName = c.PropertyValue.PropertyName.Priority,
                     UseSummary = c.PropertyValue.PropertyName.UseSummary,
                 }).ToList();


            return res;
        }

        public Tuple<int, List<ProductListViewModel>> GetProductsForAdmin(string searchtext, int pagenumber, int brnad, int category, int state, int take)
        {
            int skip = (pagenumber - 1) * take;
            IQueryable<ProductListViewModel> query = _context.Products
                .Include(c => c.Variants)
                .Include(c => c.Brand)
                .Include(c => c.Category).AsQueryable()
                .OrderByDescending(c => c.Id)
                .Where(p => EF.Functions.Like(p.EnTitle, "%" + searchtext + "%") || EF.Functions.Like(p.FaTitle, "%" + searchtext + "%"))
                .Select(p => new ProductListViewModel
                {
                    Id = p.Id,
                    FaTitle = p.FaTitle,
                    Image = p.ImgName,
                    SpecCode = p.SpecCode,
                    CommodityId = p.CommodityId,
                    TolatCount = p.Variants.Sum(c => c.Count),
                    CategoryTitle = p.Category.FaTitle,
                    BrnadTitle = p.Brand.FaTitle,
                    BrnadId = p.Brand.Id,
                    CategoryId = p.Category.Id,
                    DefaultPrice = p.Variants.Where(v => v.Count > 0).OrderByDescending(c => c.SepcialPrice).FirstOrDefault() != null ? p.Variants.Where(v => v.Count > 0).OrderByDescending(c => c.SepcialPrice).FirstOrDefault().SepcialPrice.ToString("N0") : "0",
                    IsExist = p.Variants.Sum(c => c.Count) > 0,
                    IsAvailablesoon = p.IsAvailablesoon
                }).OrderByDescending(c => c.TolatCount);
            if (brnad != 0)
                query = query.Where(c => c.BrnadId == brnad);
            if (category != 0)
                query = query.Where(c => c.CategoryId == category);
            if (state != 0)
            {
                var isExist = state == 1;

                query = query.Where(c => c.IsExist == isExist);
            }
            return Tuple.Create(query.Count(), query.Skip(skip).Take(take).ToList());
        }

        public Tuple<int, List<SearchPageViewModel>> GetProductsForCategory(int pagenumber, int brnad, int category, int take)
        {
            throw new NotImplementedException();
        }


        public long AddProduct(DataLayer.Entities.Product.Product product)
        {
            product = product.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(product);
            _context.SaveChanges();
            return product.Id;
        }
        public long GetProductCategoryId(long id)
        {
            return _context.Products.Find(id).CategoryId;
        }

        public List<CompareViewModel> GetProductForCompare(List<long?> idlist)
        {
            var query = (from p in _context.Products.Where(c => c.IsPublished && idlist.Contains(c.Id))
                         join cat in _context.Categories on p.CategoryId equals cat.Id
                         join vr in _context.Variants.Where(c => c.Count > 0 || c.ShopCount > 0)

                         on p.Id equals vr.ProductId into variant
                         from v in variant.DefaultIfEmpty()
                         join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                         on v.Id equals vp.VariantId into temp
                         from vp in temp.DefaultIfEmpty()

                         select new CompareViewModel
                         {
                             CategoryId = p.CategoryId,
                             ProductId = p.Id,
                             FaTitle = p.FaTitle,
                             CategoryTitle = p.Category.EnTitle,
                             EnTitle = p.EnTitle ?? p.FaTitle,
                             Price = vp != null ? vp.Price : v != null ? v.SepcialPrice > 0 ? v.SepcialPrice : v.Price : 0,
                             Gallery = (from g in _context.ProductImages.Where(c => c.ProductId == p.Id)
                                        select g.ImgName).ToList(),
                             Properties = (from property in _context.ProductProperties.Where(c => c.ProductId == p.Id)
                                           join value in _context.PropertyValues on property.PropertyValueId equals value.Id
                                           select new ComparePropertyViewModel { NameId = value.PropertyNameId, Value = value.Value }).ToList()
                         }).ToList();

            return query;
        }
        public List<CompareProductViewModel> GetProductForCompareByBrandId(long brandid, string Name, long catid)
        {
            return _context.Products.Where(c => c.IsPublished && c.Id == brandid && c.FaTitle.Contains(Name) && c.CategoryId == catid)
                .Select(r => new CompareProductViewModel
                {
                    FaTitle = r.FaTitle,
                    EnTitle = r.EnTitle ?? r.FaTitle,

                    ImgName = r.ImgName,
                    ProductId = r.Id
                }).ToList();
        }

        public List<CompareProductViewModel> GetProductForCompareByName(string Name, long catid)
        {
            return _context.Products.Where(c => c.IsPublished && c.FaTitle.Contains(Name) && c.CategoryId == catid)
                .Select(r => new CompareProductViewModel
                {
                    FaTitle = r.FaTitle,
                    EnTitle = r.EnTitle ?? r.FaTitle,

                    ImgName = r.ImgName,
                    ProductId = r.Id
                }).ToList();
        }

        public List<CompareProductViewModel> GetProductForCompare(long catid)
        {
            return _context.Products.Where(c => c.CategoryId == catid)
              .Select(r => new CompareProductViewModel
              {
                  FaTitle = r.FaTitle,
                  EnTitle = r.EnTitle ?? r.FaTitle,

                  ImgName = r.ImgName,
                  ProductId = r.Id
              }).Take(39).ToList();
        }

        public List<ProductPromotionIndexViewModel> GetProductPromotionForIndex()
        {
            var query1 = (from p in _context.Products.Where(c => c.IsPublished)
                          join v in _context.Variants.Where(c => c.Count > 0 || c.ShopCount > 0)
                          on p.Id equals v.ProductId
                          join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                          on v.Id equals vp.VariantId
                          group vp by new { p.Id, p.FaTitle, p.EnTitle, p.ImgName, p.CategoryId } into g
                          select new
                          {
                              product = g.Key,
                              price = g.Min(c => c.Price),
                          }).Take(10).ToList();

            var query2 = (from p in query1
                          join c in _context.Categories on p.product.CategoryId equals c.Id
                          let v = (from variant in _context.Variants.Where(v => v.ProductId == p.product.Id && (v.Count > 0 || v.ShopCount > 0))
                                   join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                              on variant.Id equals vp.VariantId
                                   orderby vp.Price
                                   select new
                                   {
                                       vp.EndDate,
                                       vp.Price,
                                       mainprice = variant.Price,
                                       vp.Percent
                                   }).First()

                          select new ProductPromotionIndexViewModel
                          {
                              CategoryTitle = c.FaTitle,
                              FaTitle = p.product.FaTitle,
                              EnTitle = p.product.EnTitle ?? p.product.FaTitle,
                              date = v.EndDate,
                              ImgName = p.product.ImgName,
                              MainPrice = v.mainprice,
                              DiscountPrice = v.Price,
                              Percent = v.Percent,
                              ProductId = p.product.Id,
                              Property = (from prop in _context.ProductProperties.Where(pp => pp.ProductId == p.product.Id)
                                          join value in _context.PropertyValues on prop.PropertyValueId equals value.Id
                                          join name in _context.PropertyNames.Where(pn => pn.UseSummary) on value.PropertyNameId equals name.Id
                                          select new ProductPromotionIndexPropertyViewModel
                                          {
                                              Name = name.Title,
                                              Value = value.Value
                                          }).Take(4).ToList()
                          }).ToList();


            return query2;
        }
        public List<ProductViewModel> GetProductBestSelling(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {

            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var res = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0))
                    .OrderByDescending(c => c.Variants.Sum(v => v.ReserveCount)).OrderByDescending(c => c.View).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        ProductId = s.Id,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        BrandEnTitle = s.Brand.EnTitle,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryId = s.CategoryId,
                        BrandId = s.BrandId,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    });


                return res.ToList();
            }
            else
            {
                var res = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPlusPrice > 0))
                    .OrderByDescending(c => c.Variants.Sum(v => v.ReserveCount)).OrderByDescending(c => c.View).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        ProductId = s.Id,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        BrandEnTitle = s.Brand.EnTitle,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryId = s.CategoryId,
                        BrandId = s.BrandId,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    }).ToList();


                return res;
            }


          
        }

        public List<ProductViewModel> GetListRelatedProduct(long id, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var product = _context.Products.Find(id);
                var res = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Include(c => c.ProductCategories)
                    .Include(c => c.RelatedProducts1)
                    .Include(c => c.RelatedProducts2)
                    .Where(c => c.IsPublished && c.Id != id && (c.RelatedProducts1.Any(c => c.ProductId1 == id)
                                                                || c.RelatedProducts1.Any(c => c.ProductId2 == id)
                                                                || c.RelatedProducts2.Any(c => c.ProductId1 == id)
                                                                || c.RelatedProducts2.Any(c => c.ProductId2 == id)))
                    .Where(c => c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0))
                    .OrderByDescending(c => c.CreateDate).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        CategoryEnTitle = s.Category.EnTitle,
                        ProductId = s.Id,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()
                    });


                return res.ToList();
            }
            else
            {
                var product = _context.Products.Find(id);
                var res = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Include(c => c.ProductCategories)
                    .Include(c => c.RelatedProducts1)
                    .Include(c => c.RelatedProducts2)
                    .Where(c => c.IsPublished && c.Id != id && (c.RelatedProducts1.Any(c => c.ProductId1 == id)
                                                                || c.RelatedProducts1.Any(c => c.ProductId2 == id)
                                                                || c.RelatedProducts2.Any(c => c.ProductId1 == id)
                                                                || c.RelatedProducts2.Any(c => c.ProductId2 == id)))
                    .Where(c => c.Variants.Any(v => v.Count > 0 && v.SepcialPlusPrice > 0))
                    .OrderByDescending(c => c.CreateDate).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        CategoryEnTitle = s.Category.EnTitle,
                        ProductId = s.Id,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()
                    });


                return res.ToList();
            }

        }

        public DataLayer.Entities.Product.Product FindProductById(long id)
        {
            return _context.Products.Find(id);
        }

        public bool UpdateProduct(DataLayer.Entities.Product.Product product)
        {
            try
            {
                product = product.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(product);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;


            }
        }

        public List<DataLayer.Entities.Product.Product> GetAllProduct()
        {
            return _context.Products.Where(c => c.IsPublished).ToList();
        }

        public List<RelatedProductForAdmin> GetRelatedProductForAdmin(long productId)
        {
            var product = _context.Products.Find(productId);
            var quary = _context.Products.Where(c => c.CategoryId != product.CategoryId)
                .Include(c => c.RelatedProducts1)
                .Include(c => c.RelatedProducts2)
                .Select(c => new RelatedProductForAdmin
                {
                    ProductTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    ProductImage = c.ImgName,
                    ProductId = c.Id,
                    IsRelated = c.RelatedProducts1.Any(c => c.ProductId1 == productId)
                                || c.RelatedProducts1.Any(c => c.ProductId2 == productId)
                                || c.RelatedProducts2.Any(c => c.ProductId1 == productId)
                                || c.RelatedProducts2.Any(c => c.ProductId2 == productId)
                });




            return quary.ToList();
        }
        public List<RelatedProductForAdmin> GetAccessoriesProductForAdmin(long productId)
        {
            var product = _context.Products.Find(productId);
            var quary = _context.Products.Where(c => c.CategoryId != product.CategoryId)
                .Include(c => c.ProductAccessorieses1)
                .Include(c => c.ProductAccessorieses2)
                .Select(c => new RelatedProductForAdmin
                {
                    ProductTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    ProductImage = c.ImgName,
                    ProductId = c.Id,
                    IsRelated = c.ProductAccessorieses1.Any(c => c.ProductId1 == productId)
                                || c.ProductAccessorieses1.Any(c => c.ProductId2 == productId)
                                || c.ProductAccessorieses2.Any(c => c.ProductId1 == productId)
                                || c.ProductAccessorieses2.Any(c => c.ProductId2 == productId)
                });




            return quary.ToList();
        }

        public bool ChangeRelatedProduct(long productId, List<long> relatedProducts)
        {
            try
            {
                var oldRelated = _context.RelatedProducts.Where(c => c.ProductId1 == productId || c.ProductId2 == productId)
                    .ToList();

                _context.RemoveRange(oldRelated);
                _context.SaveChanges();

                List<RelatedProduct> newRelatedProducts = new List<RelatedProduct>();
                foreach (var relatedProduct in relatedProducts.Select(product => new RelatedProduct
                {
                    ProductId1 = productId,
                    ProductId2 = product,
                }))
                {
                    relatedProduct.SetCreateDefaultValue(_userService.GetUserId());

                    newRelatedProducts.Add(relatedProduct);
                }

                _context.AddRange(newRelatedProducts);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool ChangeAccessoriesProduct(long productId, List<long> relatedProducts)
        {
            try
            {
                var oldRelated = _context.ProductAccessorieses.Where(c => c.ProductId1 == productId || c.ProductId2 == productId)
                    .ToList();

                _context.RemoveRange(oldRelated);
                _context.SaveChanges();

                List<ProductAccessories> newRelatedProducts = new List<ProductAccessories>();
                foreach (var relatedProduct in relatedProducts.Select(product => new ProductAccessories
                {
                    ProductId1 = productId,
                    ProductId2 = product,
                }))
                {
                    relatedProduct.SetCreateDefaultValue(_userService.GetUserId());

                    newRelatedProducts.Add(relatedProduct);
                }

                _context.AddRange(newRelatedProducts);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<ProductViewModel> GetListSimilarProduct(long id, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();

                var product = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption).FirstOrDefault(c => c.Id == id);
                int price = product.Variants.OrderByDescending(c => c.Price).FirstOrDefault() != null ? product.Variants.OrderByDescending(c => c.Price).FirstOrDefault().SepcialPrice : 0;

                int minPrice = price - (price * 20 / 100);
                int maxPrice = price + (price * 20 / 100);
                if (minPrice < 0)
                    minPrice = 0;
                var res = _context.Products.Include(c => c.Variants)
                    .Include(c => c.ProductCategories)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0))
                    .Where(c => c.CategoryId == product.CategoryId && c.Id != id)
                    .OrderByDescending(c => c.CreateDate)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,

                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0 && c.SellingColleauge).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0 && c.SellingColleauge).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        CategoryEnTitle = s.Category.EnTitle,
                        ProductId = s.Id,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    })
                    .Where(c => c.MainPrice >= minPrice && c.MainPrice <= maxPrice).Take(10);


                return res.ToList();
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();

                var product = _context.Products.Include(c => c.Variants).ThenInclude(c => c.productOption).FirstOrDefault(c => c.Id == id);
                int price = product.Variants.OrderByDescending(c => c.PricePlus).FirstOrDefault() != null ? product.Variants.OrderByDescending(c => c.PricePlus).FirstOrDefault().SepcialPlusPrice : 0;

                int minPrice = price - (price * 20 / 100);
                int maxPrice = price + (price * 20 / 100);
                if (minPrice < 0)
                    minPrice = 0;
                var res = _context.Products.Include(c => c.Variants)
                    .Include(c => c.ProductCategories)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPlusPrice > 0))
                    .Where(c => c.CategoryId == product.CategoryId && c.Id != id)
                    .OrderByDescending(c => c.CreateDate)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,

                        ImgName = s.ImgName,
                        MainPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0 && c.SellingColleauge).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0 && c.SellingColleauge).OrderBy(c => c.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                        s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        CategoryEnTitle = s.Category.EnTitle,
                        ProductId = s.Id,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    })
                    .Where(c => c.MainPrice >= minPrice && c.MainPrice <= maxPrice).Take(10);


                return res.ToList();
            }

        }

        public bool DeleteProduct(long id)
        {

            try
            {
                var product = _context.Products.Find(id);
                product.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int CountUseCategoryInProducts(int catId)
        {
            var res = _context.Products.Count(c => c.CategoryId == catId);
            return res;
        }

        public void AddProductView(long id)
        {

            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.View++;
                product.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(product);
            }

            _context.SaveChanges();
        }

        public void AddProductSell(long id)
        {
            var product = _context.Products.Find(id);
            product.Sell++;
            product.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(product);
            _context.SaveChanges();
        }

        public List<ProductViewModel> GetProductNew()
        {


            var res = _context.Products.Include(c => c.Variants)
                .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0)).OrderByDescending(c => c.CreateDate).Take(10)
                .Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,

                    ImgName = s.ImgName,
                    MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                    DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,

                    ProductId = s.Id,

                });


            return res.ToList();
        }

        public List<ProductViewModel> GetProductBestView()
        {
            var res = _context.Products.Include(c => c.Variants)
                .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0)).OrderBy(c => Guid.NewGuid()).OrderByDescending(c => c.View).Take(10)
                .Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,

                    ImgName = s.ImgName,
                    MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                    DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,

                    ProductId = s.Id,

                });


            return res.ToList();
        }

        public List<ProductPricesChartViewModel> GetProductPricesForChart(long productid)
        {
            var query = (from prices in _context.ProductPrices.Where(c => c.SubmitDate < DateTime.Now.AddDays(-1) && c.SubmitDate > DateTime.Now.AddDays(-90))
                         join v in _context.Variants on prices.VariantId equals v.Id
                         join s in _context.Sellers on v.SellerId equals s.Id
                         join g in _context.Guarantees on v.GuaranteeId equals g.Id
                         where v.ProductId == productid
                         orderby prices.SubmitDate
                         select new ProductPricesChartViewModel
                         {
                             Color = prices.ProductOption.Name,
                             Date = prices.SubmitDate.GetDateForChart(),
                             DiscountPercent = prices.DiscountPercent,
                             IsAvailable = prices.IsAvailable,
                             Seller = s.Name,
                             Guarantee = g.Title,
                             ProductOptionId = prices.ProductOptionId,
                             Price = prices.Price,
                             DiscountPrice = prices.DiscountPrice > 0 ? prices.DiscountPrice : prices.Price
                         }).ToList();
            return query;
        }

        public ProductPricesChartViewModel GetProductPriceForChartByProductOptionId(long productid, long id)
        {
            var query = (from prices in _context.ProductPrices.Where(c => c.ProductOptionId == id && c.SubmitDate < DateTime.Now.AddDays(-90))
                         join v in _context.Variants on prices.VariantId equals v.Id
                         join s in _context.Sellers on v.SellerId equals s.Id
                         join g in _context.Guarantees on v.GuaranteeId equals g.Id
                         where v.ProductId == productid
                         orderby prices.SubmitDate
                         select new ProductPricesChartViewModel
                         {
                             Color = prices.ProductOption.Name,
                             Date = prices.SubmitDate.GetDateForChart(),
                             DiscountPercent = prices.DiscountPercent,
                             IsAvailable = prices.IsAvailable,
                             Seller = s.Name,
                             Guarantee = g.Title,
                             ProductOptionId = prices.ProductOptionId,
                             Price = prices.Price,
                             DiscountPrice = prices.DiscountPrice > 0 ? prices.DiscountPrice : prices.Price
                         }).FirstOrDefault();


            return query;
        }



        public List<HeaderSearchModel> HeaderSearch(string text)
        {


            var query = _context.HeaderSearches.FromSqlRaw("EXECUTE Proc_HeaderSearch {0}", text).ToList();
            return query;

        }


        public List<string> HeaderSearch2(string text)
        {
            var res = _context.Products.Where(c => c.FaTitle.Contains(text) || c.EnTitle.Contains(text))
                .Select(c => c.FaTitle).Take(5).ToList();
            return res;
        }

        public List<ProductViewModel> GetListProductAccessories(long id)
        {
            var isUserColleague = _userService.IsUserColleague();

            var res = _context.Products.Include(c => c.Variants)
                .Include(c => c.ProductCategories)
                .Include(c => c.ProductAccessorieses1)
                .Include(c => c.ProductAccessorieses2)
                .Where(c => c.IsPublished && c.Id != id && (c.ProductAccessorieses1.Any(c => c.ProductId1 == id)
                                                            || c.ProductAccessorieses2.Any(c => c.ProductId1 == id)
                                                            || c.ProductAccessorieses2.Any(c => c.ProductId2 == id)
                                                            ))
                .Where(c => c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0))
                .OrderByDescending(c => c.CreateDate).Take(10)
                .Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,
                    ImgName = s.ImgName,
                    MainPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                    s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                    VariantId = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                    s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Id : 0,
                    DiscountPrice = isUserColleague ?
                        s.Variants.Any(c => c.Count > 0 && c.SellingColleauge) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                    s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,

                    ProductId = s.Id,

                });


            return res.ToList();
        }


        public ProductSearchWithFilterViewModel SearchPage(SearchWithFilterDto filter, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();

                int skip = (filter.page - 1) * 32;

                if (filter.page == -1)
                    skip = 0;

                IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished).Include(c => c.Variants).ThenInclude(c => c.Guarantee).Include(c => c.Brand)
                    .Include(c => c.Category)
                    .AsQueryable();

                if (filter.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (filter.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPrice < c.Price && c.Count > 0));

                if (!String.IsNullOrEmpty(filter.q))
                {
                    query = query.Where(c => c.FaTitle.Contains(filter.q)
                                             || c.EnTitle.Contains(filter.q)
                                             || c.KeyWord.Contains(filter.q)
                                             || c.Brand.EnTitle.Contains(filter.q)
                                             || c.Brand.FaTitle.Contains(filter.q)
                                             || c.Brand.KeyWord.Contains(filter.q)
                                             || c.Category.FaTitle.Contains(filter.q)
                                             || c.Category.EnTitle.Contains(filter.q)
                                             || c.Category.KeyWord.Contains(filter.q)
                        );
                }
                if (filter.brand != null && filter.brand.Count > 0)
                    query = query.Where(c => filter.brand.Contains(c.BrandId));
                //
                if (filter.JustColleauge)
                    query = query.Where(c => c.Variants.Any(v => v.Count > 0 && v.SellingColleauge));

                // if (propvalue != null && propvalue.Count > 0)
                // {
                //     query = query.Include(c => c.ProductProperties)
                //         .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));
                //
                // }

                List<long> categoris = null;
                if (filter.catId != null && filter.catId.Count > 0)
                {
                    foreach (var item in filter.catId)
                    {
                        query = (from q in query
                                 join c in _context.ProductCategories.Where(c => filter.catId.Contains(c.CategoryId))
                                     on q.Id equals c.ProductId
                                 select q);

                    }
                }
                else
                {
                    //var t = query.Include(c => c.ProductCategories).ToList();
                    //categoris = (from q in query
                    //             join c in _context.ProductCategories on q.Id equals c.ProductId
                    //             join cat in _context.Categories on c.CategoryId equals cat.Id
                    //             select c.CategoryId).ToList();
                }

                IQueryable<ProductSearchWithFilterItem> res = query.Select(c => new ProductSearchWithFilterItem
                {
                    FaTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    CategoryProduct = c.Category.EnTitle,
                    Img = c.ImgName,
                    ProductId = c.Id,
                    CategoryId = c.CategoryId,
                    SellCount = c.Sell,
                    ViewCount = c.View,
                    GuranateeTitle = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Guarantee.Title : "" :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Guarantee.Title : "",
                    MainPrice = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Price : 0,
                    PromotionPrice = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                });

                int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
                if (filter.max_price > 0)
                    res = res.Where(c => c.PromotionPrice <= filter.max_price);

                if (filter.min_price > 0)
                    res = res.Where(c => c.PromotionPrice >= filter.min_price);
                int count = (res.Count() + 32 - 1) / 32;

                switch (filter.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        res = res.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        res = res.OrderBy(c => c.MainPrice == 0).ThenBy(m => m.MainPrice);
                        break;
                    case EnumSortOnProducts.ProductPublishDate:
                        res = res.OrderByDescending(c => c.ProductId);
                        break;
                    case EnumSortOnProducts.SellCount:
                        res = res.OrderByDescending(c => c.SellCount);
                        break;
                }

                if (skip > 0)
                    res = res.Skip(skip);

                if (filter.page != -1)
                    return new ProductSearchWithFilterViewModel(filter.page, filter.min_price, max_price, count, res.Take(32).ToList());

                return new ProductSearchWithFilterViewModel(filter.page, filter.min_price, max_price, count, res.ToList());

            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();

                int skip = (filter.page - 1) * 32;

                if (filter.page == -1)
                    skip = 0;

                IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished).Include(c => c.Variants).ThenInclude(c => c.Guarantee).Include(c => c.Brand)
                    .Include(c => c.Category)
                    .AsQueryable();

                if (filter.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (filter.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPlusPrice < c.PriceColleaguePlus && c.Count > 0));

                if (!String.IsNullOrEmpty(filter.q))
                {
                    query = query.Where(c => c.FaTitle.Contains(filter.q)
                                             || c.EnTitle.Contains(filter.q)
                                             || c.KeyWord.Contains(filter.q)
                                             || c.Brand.EnTitle.Contains(filter.q)
                                             || c.Brand.FaTitle.Contains(filter.q)
                                             || c.Brand.KeyWord.Contains(filter.q)
                                             || c.Category.FaTitle.Contains(filter.q)
                                             || c.Category.EnTitle.Contains(filter.q)
                                             || c.Category.KeyWord.Contains(filter.q)
                        );
                }
                if (filter.brand != null && filter.brand.Count > 0)
                    query = query.Where(c => filter.brand.Contains(c.BrandId));
                //
                if (filter.JustColleauge)
                    query = query.Where(c => c.Variants.Any(v => v.Count > 0 && v.SellingColleauge));

                // if (propvalue != null && propvalue.Count > 0)
                // {
                //     query = query.Include(c => c.ProductProperties)
                //         .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));
                //
                // }

                List<long> categoris = null;
                if (filter.catId != null && filter.catId.Count > 0)
                {
                    foreach (var item in filter.catId)
                    {
                        query = (from q in query
                                 join c in _context.ProductCategories.Where(c => filter.catId.Contains(c.CategoryId))
                                     on q.Id equals c.ProductId
                                 select q);

                    }
                }
                else
                {
                    //var t = query.Include(c => c.ProductCategories).ToList();
                    //categoris = (from q in query
                    //             join c in _context.ProductCategories on q.Id equals c.ProductId
                    //             join cat in _context.Categories on c.CategoryId equals cat.Id
                    //             select c.CategoryId).ToList();
                }

                IQueryable<ProductSearchWithFilterItem> res = query.Select(c => new ProductSearchWithFilterItem
                {
                    FaTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    CategoryProduct = c.Category.EnTitle,
                    Img = c.ImgName,
                    ProductId = c.Id,
                    CategoryId = c.CategoryId,
                    SellCount = c.Sell,
                    ViewCount = c.View,
                    GuranateeTitle = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().Guarantee.Title : "" :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().Guarantee.Title : "",
                    MainPrice = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                    PromotionPrice = isUserColleague ?
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                    c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                });

                int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
                if (filter.max_price > 0)
                    res = res.Where(c => c.PromotionPrice <= filter.max_price);

                if (filter.min_price > 0)
                    res = res.Where(c => c.PromotionPrice >= filter.min_price);
                int count = (res.Count() + 32 - 1) / 32;

                switch (filter.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        res = res.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        res = res.OrderBy(c => c.MainPrice == 0).ThenBy(m => m.MainPrice);
                        break;
                    case EnumSortOnProducts.ProductPublishDate:
                        res = res.OrderByDescending(c => c.ProductId);
                        break;
                    case EnumSortOnProducts.SellCount:
                        res = res.OrderByDescending(c => c.SellCount);
                        break;
                }

                if (skip > 0)
                    res = res.Skip(skip);

                if (filter.page != -1)
                    return new ProductSearchWithFilterViewModel(filter.page, filter.min_price, max_price, count, res.Take(32).ToList());

                return new ProductSearchWithFilterViewModel(filter.page, filter.min_price, max_price, count, res.ToList());

            }


        }
        public Tuple<List<SearchPageViewModel>, Category, int?, int, int> SearchPage(
             List<long> propvalue = null,
             int? minprice = 0, int? maxprice = 0, int sort = 0, long catid = 0, List<long?> brandId = null,
             int skip = 0, int page = 1)
        {
            var isUserColleague = _userService.IsUserColleague();

            IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished).Include(c => c.Variants).ThenInclude(c => c.Guarantee).Include(c => c.Brand)
                .Include(c => c.Category).ThenInclude(p => p.SubCategory).ThenInclude(p => p.ParentCategory)
                .AsQueryable();

            if (brandId != null && brandId.Count > 0)
                query = query.Where(c => brandId.Contains(c.BrandId));

            if (propvalue != null && propvalue.Count > 0)
            {
                query = query.Include(c => c.ProductProperties)
                    .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));

            }

            List<long> categoris = null;

            query = (from q in query
                     join c in _context.ProductCategories.Where(c => c.CategoryId == catid)
                         on q.Id equals c.ProductId
                     select q);


            var cat = query.FirstOrDefault().Category;

            IQueryable<SearchPageViewModel> res = query.Select(c => new SearchPageViewModel
            {
                FaTitle = c.FaTitle,
                EnTitle = c.EnTitle ?? c.FaTitle,
                CategoryProduct = c.Category.EnTitle,
                Img = c.ImgName,
                ProductId = c.Id,
                CategoryId = c.CategoryId,

                Sell = c.Sell,
                View = c.View,
                GuranateeTitle = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Guarantee.Title : "" :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Guarantee.Title : "",
                MainPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Price : 0,
                PromotionPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
            });

            int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
            if (maxprice > 0)
                res = res.Where(c => c.PromotionPrice <= maxprice);

            if (minprice > 0)
                res = res.Where(c => c.PromotionPrice >= minprice);
            int count = (res.Count() + 32 - 1) / 32;

            switch (sort)
            {

                case 1:
                    res = res.OrderByDescending(c => c.MainPrice);
                    break;
                case 2:
                    res = res.OrderBy(c => c.MainPrice);
                    break;
                case 3:
                    res = res.OrderByDescending(c => c.ProductId);
                    break;
                case 4:
                    res = res.OrderByDescending(c => c.Sell);
                    break;
            }

            if (skip > 0)
                res = res.Skip(skip);

            if (page != -1)
                return Tuple.Create(res.Take(32).ToList(), cat, max_price, count, page);

            return Tuple.Create(res.ToList(), cat, max_price, count, page);

        }

        public ProductSearchWithFilterViewModel SearchInProductsWithFilterInBrand(FilterDto dto, long brandId)
        {
            var isUserColleague = _userService.IsUserColleague();

            int skip = (dto.page - 1) * 32;

            if (dto.page == -1)
                skip = 0;

            IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished)
                 .Include(c => c.Variants).ThenInclude(c => c.Guarantee)
                 .Include(c => c.Brand)
                .Include(c => c.Category).AsQueryable();

            if (dto.brand != null && dto.brand.Count > 0)
                query = query.Where(c => dto.brand.Contains(c.BrandId));
            if (brandId == brandId)
                query = query.Where(c => brandId == c.BrandId);

            // if (propvalue != null && propvalue.Count > 0)
            // {
            //     query = query.Include(c => c.ProductProperties)
            //         .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));
            //
            // }

            List<long> categoris = null;

            IQueryable<ProductSearchWithFilterItem> res = query.Select(c => new ProductSearchWithFilterItem
            {
                FaTitle = c.FaTitle,
                EnTitle = c.EnTitle ?? c.FaTitle,
                CategoryProduct = c.Category.EnTitle,
                Img = c.ImgName,
                ProductId = c.Id,
                CategoryId = c.CategoryId,
                SellCount = c.Sell,
                ViewCount = c.View,
                GuranateeTitle = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Guarantee.Title : "" :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Guarantee.Title : "",
                MainPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Price : 0,
                PromotionPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
            });

            int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
            if (dto.max_price > 0)
                res = res.Where(c => c.PromotionPrice <= dto.max_price);

            if (dto.min_price > 0)
                res = res.Where(c => c.PromotionPrice >= dto.min_price);
            int count = (res.Count() + 32 - 1) / 32;

            switch (dto.sort)
            {

                case EnumSortOnProducts.MainPriceDesc:
                    res = res.OrderByDescending(c => c.MainPrice);
                    break;
                case EnumSortOnProducts.MainPrice:
                    res = res.OrderBy(c => c.MainPrice == 0).ThenBy(m => m.MainPrice);
                    break;
                case EnumSortOnProducts.ProductPublishDate:
                    res = res.OrderByDescending(c => c.ProductId);
                    break;
                case EnumSortOnProducts.SellCount:
                    res = res.OrderByDescending(c => c.SellCount);
                    break;
            }

            if (skip > 0)
                res = res.Skip(skip);

            if (dto.page != -1)
                return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.Take(32).ToList());

            return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.ToList());
        }

        public ProductSearchWithFilterViewModel SearchInProductsWithFilterInCategory(FilterDto dto, long categoryId,
            EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var isUserColleague = _userService.IsUserColleague();
                //
                int skip = (dto.page - 1) * 32;

                if (dto.page == -1)
                    skip = 0;

                IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished)
                     .Include(c => c.Variants).ThenInclude(c => c.Guarantee)
                     .Include(c => c.Brand)
                    .Include(c => c.Category).AsQueryable();

                if (dto.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (dto.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPrice < c.Price && c.Count > 0));

                if (dto.brand != null && dto.brand.Count > 0)
                    query = query.Where(c => dto.brand.Contains(c.BrandId));
                if (dto.Seri != null && dto.Seri.Count > 0)
                    query = query.Where(c => dto.Seri.Contains(c.SeriId));

                // if (propvalue != null && propvalue.Count > 0)
                // {
                //     query = query.Include(c => c.ProductProperties)
                //         .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));
                //
                // }

                List<long> categoris = null;
                if (categoryId > 0)
                {
                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == categoryId)
                                 on q.Id equals c.ProductId
                             select q);

                }


                IQueryable<ProductSearchWithFilterItem> res = query.Select(c => new ProductSearchWithFilterItem
                {
                    FaTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    CategoryProduct = c.Category.EnTitle,
                    Img = c.ImgName,
                    ProductId = c.Id,
                    CategoryId = c.CategoryId,
                    SellCount = c.Sell,
                    ViewCount = c.View,
                    GuranateeTitle = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Guarantee.Title : "" :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Guarantee.Title : "",
                    MainPrice = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Price : 0,
                    PromotionPrice = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().PriceColleague : 0 :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                });

                int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
                if (dto.max_price > 0)
                    res = res.Where(c => c.PromotionPrice <= dto.max_price);

                if (dto.min_price > 0)
                    res = res.Where(c => c.PromotionPrice >= dto.min_price);
                int count = (res.Count() + 32 - 1) / 32;

                switch (dto.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        res = res.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        {
                            res = res.OrderBy(c => c.MainPrice == 0).ThenBy(m => m.MainPrice);
                            break;
                        }
                    case EnumSortOnProducts.ProductPublishDate:
                        res = res.OrderByDescending(c => c.ProductId);
                        break;
                    case EnumSortOnProducts.SellCount:
                        res = res.OrderByDescending(c => c.SellCount);
                        break;
                }

                if (skip > 0)
                    res = res.Skip(skip);

                if (dto.page != -1)
                    return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.Take(32).ToList());

                return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.ToList());
            }
            else
            {
                var isUserColleague = _userService.IsUserColleague();
                //
                int skip = (dto.page - 1) * 32;

                if (dto.page == -1)
                    skip = 0;

                IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished)
                     .Include(c => c.Variants).ThenInclude(c => c.Guarantee)
                     .Include(c => c.Brand)
                    .Include(c => c.Category).AsQueryable();

                if (dto.availablestock)
                    query = query.Where(p => p.Variants.Sum(c => c.Count) > 0);
                if (dto.discounted)
                    query = query.Where(p => p.Variants.Any(c => c.SepcialPlusPrice < c.PricePlus && c.Count > 0));

                if (dto.brand != null && dto.brand.Count > 0)
                    query = query.Where(c => dto.brand.Contains(c.BrandId));
                if (dto.Seri != null && dto.Seri.Count > 0)
                    query = query.Where(c => dto.Seri.Contains(c.SeriId));

                // if (propvalue != null && propvalue.Count > 0)
                // {
                //     query = query.Include(c => c.ProductProperties)
                //         .Where(c => c.ProductProperties.Any(prop => propvalue.Contains(prop.PropertyValueId)));
                //
                // }

                List<long> categoris = null;
                if (categoryId > 0)
                {
                    query = (from q in query
                             join c in _context.ProductCategories.Where(c => c.CategoryId == categoryId)
                                 on q.Id equals c.ProductId
                             select q);

                }


                IQueryable<ProductSearchWithFilterItem> res = query.Select(c => new ProductSearchWithFilterItem
                {
                    FaTitle = c.FaTitle,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    CategoryProduct = c.Category.EnTitle,
                    Img = c.ImgName,
                    ProductId = c.Id,
                    CategoryId = c.CategoryId,
                    SellCount = c.Sell,
                    ViewCount = c.View,
                    GuranateeTitle = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().Guarantee.Title : "" :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().Guarantee.Title : "",
                    MainPrice = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                    PromotionPrice = isUserColleague ?
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleaguePlus).FirstOrDefault().PriceColleaguePlus : 0 :
                        c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                });

                int? max_price = res.OrderByDescending(c => c.MainPrice).FirstOrDefault()?.MainPrice;
                if (dto.max_price > 0)
                    res = res.Where(c => c.PromotionPrice <= dto.max_price);

                if (dto.min_price > 0)
                    res = res.Where(c => c.PromotionPrice >= dto.min_price);
                int count = (res.Count() + 32 - 1) / 32;

                switch (dto.sort)
                {

                    case EnumSortOnProducts.MainPriceDesc:
                        res = res.OrderByDescending(c => c.MainPrice);
                        break;
                    case EnumSortOnProducts.MainPrice:
                        {
                            res = res.OrderBy(c => c.MainPrice == 0).ThenBy(m => m.MainPrice);
                            break;
                        }
                    case EnumSortOnProducts.ProductPublishDate:
                        res = res.OrderByDescending(c => c.ProductId);
                        break;
                    case EnumSortOnProducts.SellCount:
                        res = res.OrderByDescending(c => c.SellCount);
                        break;
                }

                if (skip > 0)
                    res = res.Skip(skip);

                if (dto.page != -1)
                    return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.Take(32).ToList());

                return new ProductSearchWithFilterViewModel(dto.page, dto.min_price, max_price, count, res.ToList());
            }

        }

        public List<SearchPageViewModel> GetProductSuggest()
        {
            var isUserColleague = _userService.IsUserColleague();

            IQueryable<DataLayer.Entities.Product.Product> query = _context.Products.Where(c => c.IsPublished).AsQueryable().Include(c => c.Variants);

            IQueryable<SearchPageViewModel> res = query.Select(c => new SearchPageViewModel
            {
                FaTitle = c.FaTitle,
                Img = c.ImgName,
                MainPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Price : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().Price : 0,
                PromotionPrice = isUserColleague ?
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0 && v.SellingColleauge) ? c.Variants.Where(v => v.Count > 0 && v.SellingColleauge).OrderBy(v => v.PriceColleague).FirstOrDefault().Price : 0 :
                c.Variants.Count > 0 && c.Variants.Any(v => v.Count > 0) ? c.Variants.Where(v => v.Count > 0).OrderBy(v => v.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                ProductId = c.Id,
            }).Where(c => c.MainPrice > 0).OrderBy(c => Guid.NewGuid()).Take(5);

            return res.ToList();


        }

        public DataLayer.Entities.Product.Product GetProductNameWithId(long id)
        {
            var product = _context.Products.Where(i => i.Id.Equals(id)).Include(q => q.Category).Select(e => new DataLayer.Entities.Product.Product() { EnTitle = e.EnTitle.ToUrlFormat(), Category = new DataLayer.Entities.Category.Category() { EnTitle = e.Category.EnTitle.ToUrlFormat() } }).SingleOrDefault();
            if (product == null) return null;
            return product;
        }

        public async Task<List<DataLayer.Entities.Product.Product>> GetByCategoryIdAndBrandId(long categoryId, long brandId)
        {
            var query = _context.Products.Include(s => s.Variants).Include(s => s.Category).AsQueryable();

            if (brandId != 0)
                query = query.Where(p => p.BrandId == brandId);
            if (categoryId != 0)
                query = query.Where(p => p.CategoryId == categoryId);

            return await query.ToListAsync();
        }

        public List<ProductViewModel> GetProductListPrice(long? categoryId, long? brandId, long? seriId, EnumProductPricePageOrder order)
        {
            var quary = _context.Products
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .Include(c => c.Variants)
                .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0)).AsQueryable();


            if (categoryId != null)
                quary = quary.Where(c => c.CategoryId == categoryId);

            if (brandId != null)
                quary = quary.Where(c => c.BrandId == brandId);

            if (seriId != null)
                quary = quary.Where(c => c.SeriId == seriId);



            var res = quary.Select(s => new ProductViewModel
            {
                FaTitle = s.FaTitle,
                EnTitle = s.EnTitle ?? s.FaTitle,
                ImgName = s.ImgName,
                MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                ProductId = s.Id,
                CategoryId = s.CategoryId,
                BrandId = s.BrandId,
                BrandEnTitle = s.Brand.EnTitle,
                BrandTitle = s.Brand.FaTitle,
                CategoryEnTitle = s.Category.EnTitle,
                CategoryFaTitle = s.Category.FaTitle,


            });

            switch (order)
            {
                case EnumProductPricePageOrder.Alphabet:
                    res = res.OrderBy(c => c.EnTitle);
                    break;
                case EnumProductPricePageOrder.Price:
                    res = res.OrderByDescending(c => c.MainPrice);
                    break;
            }

            return res.ToList();
        }

        public List<ProductViewModel> GetListAvailableSoonProducts()
        {
            var quary = _context.Products
                .Include(c => c.Brand)
                .Include(c => c.Category)
                 .Where(c => c.IsAvailablesoon).OrderByDescending(c => c.Id).AsQueryable();





            var res = quary.Select(s => new ProductViewModel
            {
                FaTitle = s.FaTitle,
                EnTitle = s.EnTitle ?? s.FaTitle,
                ImgName = s.ImgName,
                MainPrice = 0,
                DiscountPrice = 0,
                ProductId = s.Id,
                CategoryId = s.CategoryId,
                BrandId = s.BrandId,
                BrandEnTitle = s.Brand.EnTitle,
                BrandTitle = s.Brand.FaTitle,
                CategoryEnTitle = s.Category.EnTitle,
                CategoryFaTitle = s.Category.FaTitle,


            });



            return res.ToList();
        }

        public List<ProductViewModel> GetListDiscountProducts(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                var res = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0 && v.SepcialPrice != v.Price))
                    .OrderByDescending(c => c.Variants.Sum(v => v.ReserveCount)).OrderByDescending(c => c.View).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                        ProductId = s.Id,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        BrandEnTitle = s.Brand.EnTitle,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryId = s.CategoryId,
                        BrandId = s.BrandId,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    });


                return res.ToList();
            }
            else
            {
                var res = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                    .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPlusPrice > 0 && v.SepcialPlusPrice != v.PricePlus))
                    .OrderByDescending(c => c.Variants.Sum(v => v.ReserveCount)).OrderByDescending(c => c.View).Take(10)
                    .Select(s => new ProductViewModel
                    {
                        FaTitle = s.FaTitle,
                        EnTitle = s.EnTitle ?? s.FaTitle,
                        ImgName = s.ImgName,
                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().PricePlus : 0,
                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPlusPrice).FirstOrDefault().SepcialPlusPrice : 0,
                        ProductId = s.Id,
                        CategoryEnTitle = s.Category.EnTitle,
                        CategoryFaTitle = s.Category.FaTitle,
                        BrandEnTitle = s.Brand.EnTitle,
                        BrandTitle = s.Brand.FaTitle,
                        CategoryId = s.CategoryId,
                        BrandId = s.BrandId,
                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                        {
                            Color = v.productOption.Value,
                            Text = v.productOption.Name
                        }).ToList()

                    });


                return res.ToList();
            }
     
        }

        public List<DataLayer.Entities.Product.Product> GetAllProductByCategory()
        {
            return _context.Products.Include(c => c.Category).Where(c => c.IsPublished).ToList();

        }

        public List<ProductApiViewModel> GetListProductForApi(FilterProductApiViewModel filter)
        {
            var query = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                  .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.SepcialPrice > 0))
                                    .Select(s => new ProductApiViewModel
                                    {
                                        FaTitle = s.FaTitle,
                                        EnTitle = s.EnTitle ?? s.FaTitle,
                                        ImgName = "https://farnaa.com/uploads/" + s.ImgName,
                                        MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().Price : 0,
                                        DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.SepcialPrice).FirstOrDefault().SepcialPrice : 0,
                                        ProductId = s.Id,
                                        CategoryEnTitle = s.Category.EnTitle,
                                        CategoryFaTitle = s.Category.FaTitle,
                                        BrandEnTitle = s.Brand.EnTitle,
                                        BrandTitle = s.Brand.FaTitle,
                                        CategoryId = s.CategoryId,
                                        BrandId = s.BrandId,
                                        Url = $"https://farnaa.com/product/{s.Id}/{s.Category.EnTitle.ToUrlFormat()}/{s.EnTitle.ToUrlFormat()}",
                                        VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new ProductColorApiViewModel
                                        {
                                            Color = v.productOption.Value,
                                            Text = v.productOption.Name
                                        }).ToList()

                                    }).AsQueryable();

            if (filter.ProductId != null && filter.ProductId != 0)
                query = query.Where(c => c.ProductId == filter.ProductId);

            if (filter.CategoryId != null && filter.CategoryId != 0)
                query = query.Where(c => c.CategoryId == filter.CategoryId);

            if (filter.BrandId != null && filter.BrandId != 0)
                query = query.Where(c => c.BrandId == filter.BrandId);

            if (!string.IsNullOrEmpty(filter.ProductName))
                query = query.Where(c => c.FaTitle.Contains(filter.ProductName) || c.EnTitle.Contains(filter.ProductName));
            
            if (!string.IsNullOrEmpty(filter.BrandName))
                query = query.Where(c => c.BrandTitle.Contains(filter.BrandName) || c.BrandEnTitle.Contains(filter.BrandName));

            if (!string.IsNullOrEmpty(filter.CategoryName))
                query = query.Where(c => c.CategoryFaTitle.Contains(filter.CategoryName) || c.CategoryEnTitle.Contains(filter.CategoryName));


            var res = query.OrderByDescending(c => c.DiscountPrice).Take(4).ToList();
            return res;
        }

        public List<ProductViewModel> GetColleaugeSpecialSaleProduct()
        {
            var res = _context.Products.Include(c => c.Category).Include(c => c.Brand).Include(c => c.Variants).ThenInclude(c => c.productOption)
                .Where(c => c.IsPublished && c.Variants.Any(v => v.Count > 0 && v.PriceColleague > 0 && !v.GetColleaguePriceFromOrginal ))
                .OrderByDescending(c => c.Variants.Sum(v => v.ReserveCount)).OrderByDescending(c => c.View)/*.Take(10)*/
                .Select(s => new ProductViewModel
                {
                    FaTitle = s.FaTitle,
                    EnTitle = s.EnTitle ?? s.FaTitle,
                    ImgName = s.ImgName,
                    MainPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0,
                    DiscountPrice = s.Variants.Any(c => c.Count > 0) ? s.Variants.Where(c => c.Count > 0).OrderBy(c => c.PriceColleague).FirstOrDefault().PriceColleague : 0,
                    ProductId = s.Id,
                    CategoryEnTitle = s.Category.EnTitle,
                    CategoryFaTitle = s.Category.FaTitle,
                    BrandEnTitle = s.Brand.EnTitle,
                    BrandTitle = s.Brand.FaTitle,
                    CategoryId = s.CategoryId,
                    BrandId = s.BrandId,
                    VariantColor = s.Variants.Where(c => c.Count > 0).Select(v => new VariantColorViewModel
                    {
                        Color = v.productOption.Value,
                        Text = v.productOption.Name
                    }).ToList()

                });


            return res.ToList();
        }
    }

}
