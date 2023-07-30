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
using EShop.DataLayer.Entities.Promotion;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Variant
{
    public class VariantService : BaseService<DataLayer.Entities.Variety.Variant>, IVariantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public VariantService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<VariantsForProductDetailViewModel> GetVariantsByProductId(long productId, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            if (typeSystem == EnumTypeSystem.Farnaa)
            {
                bool isUserColleague = _userService.IsUserColleague();
                var query = _context.Variants.Where(v => (v.ProductId == productId) && (/*v.ShopCount > 0 ||*/((!isUserColleague && v.Count > 0) || (isUserColleague && v.Count > 0 && v.SellingColleauge))));
                var variants = (from q in query
                                join g in _context.Guarantees on q.GuaranteeId equals g.Id
                                join p in _context.ProductOptions on q.ProductOptionId equals p.Id
                                join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                                on q.Id equals vp.VariantId
                                into varaint
                                from v in varaint.DefaultIfEmpty()
                                select new VariantsForProductDetailViewModel
                                {
                                    VariantId = q.Id,
                                    Count = isUserColleague ? q.Count : q.Count,
                                    DisSatisfied = q.DisSatisfied,
                                    TotallyDisSatisfied = q.TotallyDisSatisfied,
                                    Satisfied = q.Satisfied,
                                    Neutral = q.Neutral,
                                    TotallySatisfied = q.TotallySatisfied,
                                    VoteCount = q.VoteCount,
                                    PurchaseConsentPercent = q.PurchaseConsentPercent,
                                    Guarantee = g.Title,
                                    ProductOptionCodeTwo = p.SecondValue,
                                    ProductOption = p.Name,
                                    ProductOptionId = p.Id,
                                    ProductOptionCode = p.Value,
                                    DiscountType = v.Type,
                                    DiscountEndDate = v.EndDate,
                                    SellerId = q.SellerId,
                                    MainPrice = isUserColleague ? q.PriceColleague : q.Price,
                                    MaxOrderCount = isUserColleague ? q.MaxOrderCountColleague : q.MaxOrderCount,
                                    SellerPrice = isUserColleague ?
                                        q.PriceColleague :
                                        v.Price > 0 ? v.Price : q.SepcialPrice > 0 ? q.SepcialPrice : q.Price
                                }
                                ).OrderByDescending(c => c.VariantId).ToList();

                return variants;
            }
            else
            {
                bool isUserColleague = _userService.IsUserColleague();
                var query = _context.Variants.Where(v => (v.ProductId == productId) && (/*v.ShopCount > 0 ||*/((!isUserColleague && v.Count > 0) || (isUserColleague && v.Count > 0 && v.SellingColleauge))));
                var variants = (from q in query
                                join g in _context.Guarantees on q.GuaranteeId equals g.Id
                                join p in _context.ProductOptions on q.ProductOptionId equals p.Id
                                join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                                on q.Id equals vp.VariantId
                                into varaint
                                from v in varaint.DefaultIfEmpty()
                                select new VariantsForProductDetailViewModel
                                {
                                    VariantId = q.Id,
                                    Count = isUserColleague ? q.Count : q.Count,
                                    DisSatisfied = q.DisSatisfied,
                                    TotallyDisSatisfied = q.TotallyDisSatisfied,
                                    Satisfied = q.Satisfied,
                                    Neutral = q.Neutral,
                                    TotallySatisfied = q.TotallySatisfied,
                                    VoteCount = q.VoteCount,
                                    PurchaseConsentPercent = q.PurchaseConsentPercent,
                                    Guarantee = g.Title,
                                    ProductOptionCodeTwo = p.SecondValue,
                                    ProductOption = p.Name,
                                    ProductOptionId = p.Id,
                                    ProductOptionCode = p.Value,
                                    DiscountType = v.Type,
                                    DiscountEndDate = v.EndDate,
                                    SellerId = q.SellerId,
                                    MainPrice = isUserColleague ? q.PriceColleaguePlus : q.PricePlus,
                                    MaxOrderCount = isUserColleague ? q.MaxOrderCountColleaguePlus : q.MaxOrderCountPlus,
                                    SellerPrice = isUserColleague ?
                                        q.PriceColleaguePlus :
                                        /*v.Price > 0 ? v.Price :*/ q.SepcialPlusPrice > 0 ? q.SepcialPlusPrice : q.PricePlus
                                }
                                ).OrderByDescending(c => c.VariantId).ToList();

                return variants;
            }
          
        }
        public List<VariantsForProductDetailViewModel> GetAllVariantsByProductId(long productId)
        {
            var query = _context.Variants.Where(v => (v.ProductId == productId));
            var variants = (from q in query
                            join g in _context.Guarantees on q.GuaranteeId equals g.Id
                            join p in _context.ProductOptions on q.ProductOptionId equals p.Id
                            join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                            on q.Id equals vp.VariantId
                            into varaint
                            from v in varaint.DefaultIfEmpty()
                            select new VariantsForProductDetailViewModel
                            {
                                VariantId = q.Id,
                                Count = q.Count,
                                DisSatisfied = q.DisSatisfied,
                                TotallyDisSatisfied = q.TotallyDisSatisfied,
                                Satisfied = q.Satisfied,
                                Neutral = q.Neutral,
                                TotallySatisfied = q.TotallySatisfied,
                                VoteCount = q.VoteCount,
                                PurchaseConsentPercent = q.PurchaseConsentPercent,
                                Guarantee = g.Title,
                                ProductOption = p.Name,
                                ProductOptionCode = p.Value,
                                DiscountType = v.Type,
                                DiscountEndDate = v.EndDate,
                                SellerId = q.SellerId,
                                MainPrice = q.Price,
                                SellerPrice = v.Price > 0 ? v.Price : q.SepcialPrice > 0 ? q.SepcialPrice : q.Price
                            }
                            ).OrderByDescending(c => c.VariantId).ToList();

            return variants;
        }


        public Tuple<int, int, int?> GetMinPriceAndCountByVariantId(int variantid)
        {
            bool isUserColleague = _userService.IsUserColleague();
            var query = (from v in _context.Variants.Where(v => v.Id == variantid && (/*v.ShopCount > 0 ||*/ ((!isUserColleague && v.Count > 0) || (isUserColleague && v.Count > 0 && v.SellingColleauge))))
                         join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                         on v.Id equals vp.VariantId into variantpromotion
                         from vp in variantpromotion.DefaultIfEmpty()
                         select new
                         {
                             price = isUserColleague ? v.PriceColleague : vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                             count = isUserColleague ? v.Count : vp != null ? vp.ReminaingCount : v.Count,
                             maxordercoun = isUserColleague ? v.MaxOrderCountColleague : vp != null ? vp.MaxOrderCount : v.MaxOrderCount
                         }
                       ).FirstOrDefault();
            return query != null ? Tuple.Create(query.price, query.count, query.maxordercoun) : null;
        }

        public bool UpdateVariantAndVariantPromotion(List<DataLayer.Entities.Variety.Variant> variants, List<VariantPromotion> variantPromotions)
        {
            try
            {
                foreach (var variant in variants)
                {
                    variant.LastUpdateDate = DateTime.Now;

                }
                foreach (var variantPromotion in variantPromotions)
                {
                    variantPromotion.LastUpdateDate = DateTime.Now;

                }
                _context.UpdateRange(variants);
                _context.UpdateRange(variantPromotions);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<DataLayer.Entities.Variety.Variant> GetVariants(List<long> id)
        {
            return _context.Variants.Where(c => id.Contains(c.Id)).ToList();
        }

        public void UpdateRangeVariant(List<DataLayer.Entities.Variety.Variant> variants)
        {
            foreach (var variant in variants)
            {
                variant.LastUpdateDate = DateTime.Now;

            }
            _context.UpdateRange(variants);
            _context.SaveChanges();
        }

        public List<Guarantee> GetListGuarantee()
        {
            return _context.Guarantees.ToList();
        }

        public List<ProductOption> GetListProductOption()
        {
            return _context.ProductOptions.ToList();
        }

        public long GetSellerId()
        {
            return _context.Sellers.FirstOrDefault()?.Id ?? 0;
        }

        public bool AddVariant(DataLayer.Entities.Variety.Variant variant)
        {
            try
            {

                variant = variant.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(variant);
                var res = _context.SaveChanges();
                if (res > 0)
                    return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return false;
        }

        public List<VariantsProductDetailViewModel> GetListVariants(string search = "")
        {
            var res = _context.Variants
                .Include(c => c.Product)
                .Include(c => c.Guarantee)
                .Include(c => c.productOption)
                .Select(c => new VariantsProductDetailViewModel
                {
                    VariantId = (int)c.Id,
                    Count = c.Count,
                    ProductId = (int)c.ProductId,
                    Guarantee = c.Guarantee.Title,
                    MainPrice = c.Price,
                    ProductEnTitle = c.Product.EnTitle ?? c.Product.FaTitle,
                    ProductFaTitle = c.Product.FaTitle,
                    ProductOption = c.productOption.Name,
                    ProductOptionCode = c.productOption.Value,
                    SellCount = c.ReserveCount,
                    SellerId = (int)c.SellerId,
                    SellerPrice = c.SepcialPrice,
                    Image = c.Product.ImgName,
                    MaxOrderCount = c.MaxOrderCount,
                    DateSell = c.DateSell,
                    DateSellString = c.DateSell != null ? c.DateSell.Value.ConvertMiladiToShamsi().EnglishToPersian() : "",
                    //
                    CountColleague = c.Count,
                    MaxOrderCountColleague = c.MaxOrderCountColleague,
                    PriceColleague = c.PriceColleague,
                    GetColleaguePriceFromOrginal = c.GetColleaguePriceFromOrginal,
                    //
                    SepcialPlusPrice = c.SepcialPlusPrice,
                    CountPlus = c.CountPlus,
                    GetPlusPriceFromOrginal = c.GetPlusPriceFromOrginal,
                    MaxOrderCountPlus =     c.MaxOrderCountPlus,
                    PricePlus = c.PricePlus,
                    //
                    CountColleaguePlus = c.CountColleaguePlus,
                    GetColleaguePricePlusFromOrginal = c.GetColleaguePricePlusFromOrginal,
                    MaxOrderCountColleaguePlus = c.MaxOrderCountColleaguePlus,
                    PriceColleaguePlus = c.PriceColleaguePlus

                });

            if (!string.IsNullOrEmpty(search) && search != "-1")
                res = res.Where(c =>
                    c.Guarantee.Contains(search) || c.ProductEnTitle.Contains(search) ||
                    c.ProductFaTitle.Contains(search) || c.ProductOption.Contains(search));
            else if (search != "-1")
                res = res.Take(0);
            return res.OrderByDescending(c => c.VariantId).ToList();
        }

        public List<VariantPromotionsViewModel> GetListVariantPromotions(long id)
        {
            var res = _context.VariantPromotions.Where(c => c.VariantId == id)
                .Include(c => c.Variant)
                .Select(c => new VariantPromotionsViewModel
                {
                    Count = c.Count,
                    Price = c.Price,
                    VariantId = c.VariantId,
                    EndDate = c.EndDate,
                    MaxOrderCount = c.MaxOrderCount,
                    Percent = c.Percent,
                    ReminaingCount = c.ReminaingCount,
                    StartDate = c.StartDate,
                    VariantPromotionId = c.Id
                });

            return res.ToList();
        }

        public long AddVariantPromotion(VariantPromotionsViewModel model)
        {
            try
            {
                var promotion = _context.Promotion.FirstOrDefault();
                VariantPromotion variantPromotion = new VariantPromotion
                {
                    Count = model.Count,
                    EndDate = model.EndDate,
                    MaxOrderCount = model.MaxOrderCount,
                    Percent = model.Percent,
                    Price = model.Price,
                    PromotionId = promotion.Id,
                    ReminaingCount = model.Count,
                    StartDate = model.StartDate,
                    Type = 1,
                    VariantId = model.VariantId,

                };

                variantPromotion = variantPromotion.SetCreateDefaultValue(_userService.GetUserId());

                _context.Add(variantPromotion);
                _context.SaveChanges();


                return model.VariantId;
            }
            catch (Exception e)
            {
                return 0;
            }




        }

        public DataLayer.Entities.Variety.Variant GetVariantsId(long id)
        {
            return _context.Variants.Find(id);
        }

        public bool EditVariant(DataLayer.Entities.Variety.Variant variant)
        {
            try
            {
                var v = _context.Variants.Find(variant.Id);
                v.Price = variant.Price;
                v.SepcialPrice = variant.SepcialPrice;
                v.Count = variant.Count;
                v.MaxOrderCount = variant.MaxOrderCount;
                v.ProductOptionId = variant.ProductOptionId;
                v.GuaranteeId = variant.GuaranteeId;
                v.LastUpdateDate = DateTime.Now;
                _context.Update(v);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;


            }
        }

        public bool DeleteVariantPromotion(long id)
        {
            try
            {

                var variantPromotion = _context.VariantPromotions.Find(id);
                variantPromotion = variantPromotion.SetRemoveDefaultValue(_userService.GetUserId());
                _context.UpdateRange(variantPromotion);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool CheckVariantExist(long variantProductId, long variantGuaranteeId, long variantProductOptionId)
        {
            return _context.Variants.Any(c =>
                c.ProductId == variantProductId && c.GuaranteeId == variantGuaranteeId &&
                c.ProductOptionId == variantProductOptionId && c.IsDelete == false);
        }

        public bool EditVariantGrid(VariantsProductDetailViewModel variant)
        {
            try
            {
                var v = _context.Variants.Find(variant.VariantId);
                v.Price = variant.MainPrice;
                v.SepcialPrice = variant.SellerPrice;
                v.Count = variant.Count;
                v.LastUpdateDate = DateTime.Now;
                _context.Update(v);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;


            }
        }


        public bool CreateColor(ProductOption productOption)
        {
            try
            {
                productOption.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(productOption);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductOption FindColorById(long id)
        {
            return _context.ProductOptions.Find(id);
        }

        public bool UpdateColor(ProductOption productOption)
        {
            try
            {
                productOption.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(productOption);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<long> GetListProductProductOption(long productId)
        {
            return _context.Variants.Where(c => c.ProductId == productId).Select(c => c.ProductOptionId).ToList();
        }
        public List<ProductOption> GetProductProductOptions(long productId)
        {
            return _context.Variants.Include(c => c.productOption).Where(c => c.ProductId == productId).Select(c => c.productOption).ToList();
        }

        public List<long> GetListProductGuarantees(long productId)
        {
            return _context.Variants.Where(c => c.ProductId == productId).Select(c => c.GuaranteeId).Distinct().ToList();
        }

        public DataLayer.Entities.Variety.Variant GetVariantExist(long productId, long guarantee, long color)
        {
            return _context.Variants.FirstOrDefault(c =>
                c.ProductId == productId && c.GuaranteeId == guarantee &&
                c.ProductOptionId == color && c.IsDelete == false);
        }

        public void DeleteLastVarients(List<DataLayer.Entities.Variety.Variant> lastGuarantessColors)
        {
            foreach (var model in lastGuarantessColors)
            {
                var varient = _context.Variants.Find(model.Id);
                varient.SetRemoveDefaultValue(_userService.GetUserId());
                _context.SaveChanges();
            }
        }

        public List<DataLayer.Entities.Variety.Variant> GetLastVariantsByProductId(long productId)
        {
            return _context.Variants.Where(c => c.ProductId == productId).ToList();
        }

        public bool DeleteVariant(long id)
        {

            try
            {
                var variant = _context.Variants.Find(id);
                variant.SetRemoveDefaultValue(_userService.GetUserId());
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddGuarantee(Guarantee guarantee)
        {
            try
            {
                guarantee.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(guarantee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Guarantee GetGuaranteeById(long id)
        {
            var res = _context.Guarantees.Find(id);
            return res;
        }

        public bool EditGuarantee(Guarantee guarantee)
        {
            try
            {
                guarantee.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(guarantee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteGuarantee(Guarantee guarantee)
        {
            try
            {
                guarantee.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(guarantee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void UpdateProductsVariantsPreSell(List<DataLayer.Entities.Variety.Variant> variants)
        {
            foreach (var variant in variants)
            {
                try
                {
                    var v = _context.Variants.Find(variant.Id);
                    v.ChangeUserId = variant.ChangeUserId;
                    v.DateSell = variant.DateSell;
                    _context.Update(v);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                }
            }
        }
        public List<VariantUpdateMessageViewModel> UpdateProductsVariants(List<VariantUpdateViewModel> variants)
        {
            List<VariantUpdateMessageViewModel> res = new List<VariantUpdateMessageViewModel>();
            foreach (var variant in variants)
            {
                try
                {
                    var v = _context.Variants.Include(c => c.Product).FirstOrDefault(c => c.Id == variant.Id);
                    if (variant.DecreaseCount > v.Count && variant.DecreaseCount != 0)
                    {
                        res.Add(new VariantUpdateMessageViewModel
                        {
                            Id = variant.Id,
                            IsOk = false,
                            Message = $"مقدار درخواستی {variant.DecreaseCount} برای کاهش از موجودی محصول شما بیشتر است ",
                            ProductId = v.ProductId
                        });
                    }
                    else
                    {
                        if (variant.DecreaseCount != 0)
                            v.Count -= variant.DecreaseCount;
                        if (variant.IncreaseCount != 0)
                            v.Count += variant.IncreaseCount;


                        v.Price = variant.Price;
                        v.SepcialPrice = variant.SepcialPrice;
                        //
                        if ((v.Product.CategoryId != 37 &&
                            v.Product.CategoryId != 20039 &&
                            v.Product.CategoryId != 20040 &&
                            v.Product.CategoryId != 20045 &&
                            v.Product.CategoryId != 20048 &&
                            v.Product.CategoryId != 20052) && v.GetColleaguePriceFromOrginal && v.SepcialPrice > 0)
                        {
                            v.PriceColleague = variant.PriceColleague;
                            v.MaxOrderCountColleague = v.MaxOrderCountColleague > 0 ? v.MaxOrderCountColleague : variant.MaxOrderCountColleague;

                        }
                        //Plus
                        v.SepcialPlusPrice = variant.SepcialPlusPrice;
                        v.PricePlus = variant.PricePlus;
                        v.MaxOrderCountPlus = v.MaxOrderCountPlus > 0 ? v.MaxOrderCountPlus : variant.MaxOrderCountPlus;
                        //
                        //Colleague Plus
                        v.PriceColleaguePlus = variant.PriceColleaguePlus;
                        v.MaxOrderCountColleaguePlus = v.MaxOrderCountColleaguePlus > 0 ? v.MaxOrderCountColleaguePlus : variant.MaxOrderCountColleaguePlus;

                        //
                        //v.Count = variant.Count;
                        v.MaxOrderCount = variant.MaxOrderCount;
                        v.LastUpdateDate = DateTime.Now;
                        v.ChangeUserId = variant.ChangeUserId;
                        _context.Update(v);
                        _context.SaveChanges();
                        res.Add(new VariantUpdateMessageViewModel
                        {
                            Id = variant.Id,
                            IsOk = true,
                            Message = "عملیات با موفقیت انجام شد",
                            ProductId = v.ProductId
                        });
                    }

                }
                catch (Exception e)
                {
                }
            }

            return res;
        }

        public List<VariantUpdateMessageViewModel> UpdateProductsVariantsColleague(List<VariantUpdateViewModel> variants, List<ColleaguePriceRange> lstChaneColleaugePrice)
        {
            List<VariantUpdateMessageViewModel> res = new List<VariantUpdateMessageViewModel>();
            foreach (var variant in variants)
            {
                try
                {
                    var v = _context.Variants.Find(variant.Id);
                    //if (variant.DecreaseCount > v.CountColleague && variant.DecreaseCount != 0)
                    //{
                    //    res.Add(new VariantUpdateMessageViewModel
                    //    {
                    //        Id = variant.Id,
                    //        IsOk = false,
                    //        Message = $"مقدار درخواستی {variant.DecreaseCount} برای کاهش از موجودی محصول شما بیشتر است "
                    //    });
                    //}
                    //else
                    //{
                    //if (variant.DecreaseCount != 0)
                    //    v.CountColleague -= variant.DecreaseCount;
                    //if (variant.IncreaseCount != 0)
                    //    v.CountColleague += variant.IncreaseCount;


                    if (variant.GetColleaguePriceFromOrginal)
                    {
                        var changePrice = lstChaneColleaugePrice.FirstOrDefault(c => v.SepcialPrice > c.MinPrice && v.SepcialPrice < c.MaxPrice);
                        if (changePrice != null)
                            v.PriceColleague = v.SepcialPrice - changePrice.ChangePrice;
                    }
                    else
                    {
                        v.PriceColleague = variant.PriceColleague;

                    }

                    v.GetColleaguePriceFromOrginal = variant.GetColleaguePriceFromOrginal;
                    v.SellingColleauge = true;

                    v.MaxOrderCountColleague = variant.MaxOrderCountColleague;
                    v.LastUpdateDate = DateTime.Now;
                    v.ChangeUserId = variant.ChangeUserId;
                    _context.Update(v);
                    _context.SaveChanges();
                    res.Add(new VariantUpdateMessageViewModel
                    {
                        Id = variant.Id,
                        IsOk = true,
                        Message = "عملیات با موفقیت انجام شد",
                        ProductId = v.ProductId

                    });
                    //}

                }
                catch (Exception e)
                {
                }
            }

            return res;

        }

        public List<VariantUpdateMessageViewModel> UpdateProductsVariantsPlus(List<VariantUpdateViewModel> variants, List<PlusPriceRange> lstChangePlusPrice)
        {
            List<VariantUpdateMessageViewModel> res = new List<VariantUpdateMessageViewModel>();
            foreach (var variant in variants)
            {
                try
                {
                    var v = _context.Variants.Find(variant.Id);
                    //if (variant.DecreaseCount > v.CountPlus&& variant.DecreaseCount != 0)
                    //{
                    //    res.Add(new VariantUpdateMessageViewModel
                    //    {
                    //        Id = variant.Id,
                    //        IsOk = false,
                    //        Message = $"مقدار درخواستی {variant.DecreaseCount} برای کاهش از موجودی محصول شما بیشتر است "
                    //    });
                    //}
                    //else
                    //{
                    //if (variant.DecreaseCount != 0)
                    //    v.CountPlus-= variant.DecreaseCount;
                    //if (variant.IncreaseCount != 0)
                    //    v.CountPlus+= variant.IncreaseCount;


                    if (variant.GetPlusPriceFromOrginal)
                    {
                        var changePrice = lstChangePlusPrice.FirstOrDefault(c => v.SepcialPrice > c.MinPrice && v.SepcialPrice < c.MaxPrice);
                        if (changePrice != null)
                        {
                            v.PricePlus= (v.Price + (v.Price * changePrice.ChangePricePercent / 100)).RoundPrice();
                            v.SepcialPlusPrice= (v.Price + (v.SepcialPrice * changePrice.ChangePricePercent / 100)).RoundPrice();
                        }
                    }
                    else
                    {
                        v.PricePlus = variant.PricePlus;
                        v.SepcialPlusPrice = variant.SepcialPlusPrice;

                    }

                    v.GetPlusPriceFromOrginal = variant.GetPlusPriceFromOrginal;
                    v.SellingColleauge = true;

                    v.MaxOrderCountPlus= variant.MaxOrderCountPlus;
                    v.LastUpdateDate = DateTime.Now;
                    v.ChangeUserId = variant.ChangeUserId;
                    _context.Update(v);
                    _context.SaveChanges();
                    res.Add(new VariantUpdateMessageViewModel
                    {
                        Id = variant.Id,
                        IsOk = true,
                        Message = "عملیات با موفقیت انجام شد",
                        ProductId = v.ProductId

                    });
                    //}

                }
                catch (Exception e)
                {
                }
            }

            return res;
        }

        public Tuple<int, List<ProductListDiscountViewModel>> GetProductsVariantDiscountForAdmin(string searchtext, int pagenumber, int brnad, int category, int state,
            int take)
        {
            int skip = (pagenumber - 1) * take;
            IQueryable<ProductListDiscountViewModel> query = _context.Variants
                .Include(c => c.Product)
                .Include("Product.Brand")
                .Include("Product.Category")
                .Include(c => c.productOption)
                .Include(c => c.Guarantee)
                .AsQueryable()
                .OrderByDescending(c => c.Id)
                .Where(p =>
                    (EF.Functions.Like(p.Product.EnTitle, "%" + searchtext + "%") ||
                     EF.Functions.Like(p.Product.FaTitle, "%" + searchtext + "%"))
                    && p.Count > 0 && p.SepcialPrice < p.Price && !p.IsDelete)
                .Select(p => new ProductListDiscountViewModel
                {
                    Id = p.Product.Id,
                    FaTitle = p.Product.FaTitle,
                    Image = p.Product.ImgName,
                    CategoryTitle = p.Product.Category.FaTitle,
                    BrnadTitle = p.Product.Brand.FaTitle,
                    BrnadId = p.Product.Brand.Id,
                    CategoryId = p.Product.Category.Id,
                    DefaultPrice = p.Price.ToString("N0"),
                    DiscountPrice = p.SepcialPrice.ToString("N0"),
                    ColorTitle = p.productOption.Name,
                    Count = p.Count,
                    GarantyTitle = p.Guarantee.Title,

                });
            if (brnad != 0)
                query = query.Where(c => c.BrnadId == brnad);
            if (category != 0)
                query = query.Where(c => c.CategoryId == category);
            //if (state != 0)
            //{
            //    var isExist = state == 1;

            //    query = query.Where(c => c.IsExist == isExist);
            //}
            return Tuple.Create(query.Count(), query.Skip(skip).Take(take).ToList());
        }

        public bool UpdateColleaugePriceByRenge(int rengMinPrice, int rengMaxPrice, int rengChangePrice)
        {
            try
            {
                //var t = _context.Variants.Include(c => c.Product)
                //    .Where(c => c.GetColleaguePriceFromOrginal && c.PriceColleague > 0 &&
                //                (
                //                    c.Product.CategoryId == 37 ||
                //                    c.Product.CategoryId == 20039 ||
                //                    c.Product.CategoryId == 20040 ||
                //                    c.Product.CategoryId == 20045 ||
                //                    c.Product.CategoryId == 20048 ||
                //                    c.Product.CategoryId == 20052
                //                )).ToList();
                //foreach (var variant in t)
                //{
                //    variant.PriceColleague = 0;
                //    variant.SellingColleauge = false;
                //    variant.SetEditDefaultValue(_userService.GetUserId());
                //    _context.SaveChanges();
                //}

                var data = _context.Variants
                    .Include(c => c.Product)
                    .Where(c =>
                    c.SepcialPrice > rengMinPrice && c.SepcialPrice <= rengMaxPrice && c.GetColleaguePriceFromOrginal
                    &&
                    (
                        c.Product.CategoryId != 37 ||
                        c.Product.CategoryId != 20039 ||
                        c.Product.CategoryId != 20040 ||
                        c.Product.CategoryId != 20045 ||
                        c.Product.CategoryId != 20048 ||
                        c.Product.CategoryId != 20052
                    )).ToList();
                foreach (var variant in data)
                {
                    var price = variant.SepcialPrice - rengChangePrice;
                    variant.PriceColleague = price > 0 ? price : 0;
                    variant.SellingColleauge = variant.PriceColleague > 0;
                    variant.SetEditDefaultValue(_userService.GetUserId());
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ResetCustomColleaugePrice(List<ColleaguePriceRange> lstRange)
        {
            try
            {

                foreach (var item in lstRange)
                {
                    var data = _context.Variants
                        .Include(c => c.Product)
                        .Where(c =>
                            c.SepcialPrice > item.MinPrice && c.SepcialPrice <= item.MaxPrice && !c.GetColleaguePriceFromOrginal
                            &&
                            (
                                c.Product.CategoryId != 37 ||
                                c.Product.CategoryId != 20039 ||
                                c.Product.CategoryId != 20040 ||
                                c.Product.CategoryId != 20045 ||
                                c.Product.CategoryId != 20048 ||
                                c.Product.CategoryId != 20052
                            )).ToList();
                    foreach (var variant in data)
                    {
                        var price = variant.SepcialPrice - item.ChangePrice;
                        variant.PriceColleague = price > 0 ? price : 0;
                        variant.SellingColleauge = true;
                        variant.GetColleaguePriceFromOrginal = true;
                        variant.SetEditDefaultValue(_userService.GetUserId());
                        _context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ResetCustomPlusPrice(List<PlusPriceRange> lstRange)
        {
            //Todo
            throw new NotImplementedException();
        }

        public DataLayer.Entities.Product.Product GetProductByVariantId(int id)
        {
            var res = _context.Variants.Include(c => c.Product).ThenInclude(c=>c.Category).Where(c => c.Id == id)
                .Select(c => c.Product).FirstOrDefault();
            return res;
        }

        public VariantsProductDetailViewModel GetVariantsDetailById(int id)
        {
            var res = _context.Variants
                .Include(c => c.Product)
                .Include(c => c.Guarantee)
                .Include(c => c.productOption)
                .Select(c => new VariantsProductDetailViewModel
                {
                    VariantId = (int)c.Id,
                    Count = c.Count,
                    ProductId = (int)c.ProductId,
                    Guarantee = c.Guarantee.Title,
                    MainPrice = c.Price,
                    ProductEnTitle = c.Product.EnTitle ?? c.Product.FaTitle,
                    ProductFaTitle = c.Product.FaTitle,
                    ProductOption = c.productOption.Name,
                    ProductOptionCode = c.productOption.Value,
                    SellCount = c.ReserveCount,
                    SellerId = (int)c.SellerId,
                    SellerPrice = c.SepcialPrice,
                    Image = c.Product.ImgName,
                    MaxOrderCount = c.MaxOrderCount,
                    DateSell = c.DateSell,
                    DateSellString = c.DateSell != null ? c.DateSell.Value.ConvertMiladiToShamsi().EnglishToPersian() : "",
                    //
                    CountColleague = c.Count,
                    MaxOrderCountColleague = c.MaxOrderCountColleague,
                    PriceColleague = c.PriceColleague,
                    GetColleaguePriceFromOrginal = c.GetColleaguePriceFromOrginal

                }).FirstOrDefault(c=>c.VariantId == id);
            return res;


        }

        public Tuple<int, int, int?> GetMinPriceAndCountByVariantIdAndUserType(int variantId, int userId)
        {
            bool isUserColleague = _userService.IsUserColleagueByUserId(userId);
            var query = (from v in _context.Variants.Where(v => v.Id == variantId && (/*v.ShopCount > 0 ||*/ ((!isUserColleague && v.Count > 0) || (isUserColleague && v.Count > 0 && v.SellingColleauge))))
                         join vp in _context.VariantPromotions.Where(vp => (vp.StartDate <= DateTime.Now && vp.EndDate >= DateTime.Now) && vp.ReminaingCount > 0)
                         on v.Id equals vp.VariantId into variantpromotion
                         from vp in variantpromotion.DefaultIfEmpty()
                         select new
                         {
                             price = isUserColleague ? v.PriceColleague : vp != null ? vp.Price : v.SepcialPrice > 0 ? v.SepcialPrice : v.Price,
                             count = isUserColleague ? v.Count : vp != null ? vp.ReminaingCount : v.Count,
                             maxordercoun = isUserColleague ? v.MaxOrderCountColleague : vp != null ? vp.MaxOrderCount : v.MaxOrderCount
                         }
                       ).FirstOrDefault();
            return query != null ? Tuple.Create(query.price, query.count, query.maxordercoun) : null;
        }

        public bool UpdatePlusPriceByRenge(int rengMinPrice, int rengMaxPrice, int rengChangePricePercent)
        {
            //Todo
            throw new NotImplementedException();
        }

        public bool ResetCustomPlusPriceColleague(List<ColleaguePlusPriceRange> lstRange)
        {
            //Todo
            throw new NotImplementedException();
        }

        public List<VariantUpdateMessageViewModel> UpdateProductsVariantsColleaguePlus(List<VariantUpdateViewModel> variants, List<ColleaguePlusPriceRange> lstChangePlusColleaugePrice)
        {
            List<VariantUpdateMessageViewModel> res = new List<VariantUpdateMessageViewModel>();
            foreach (var variant in variants)
            {
                try
                {
                    var v = _context.Variants.Find(variant.Id);
                   


                    if (variant.GetColleaguePricePlusFromOrginal)
                    {
                        var changePrice = lstChangePlusColleaugePrice.FirstOrDefault(c => v.SepcialPrice > c.MinPrice && v.SepcialPrice < c.MaxPrice);
                        if (changePrice != null)
                            v.PriceColleaguePlus = (v.SepcialPrice +(v.SepcialPrice * changePrice.ChangePricePercent / 100)).RoundPrice();
                    }
                    else
                    {
                        v.PriceColleaguePlus = variant.PriceColleaguePlus;

                    }

                    v.GetColleaguePricePlusFromOrginal = variant.GetColleaguePricePlusFromOrginal;
                    v.SellingColleauge = true;

                    v.MaxOrderCountColleaguePlus = variant.MaxOrderCountColleaguePlus;
                    v.LastUpdateDate = DateTime.Now;
                    v.ChangeUserId = variant.ChangeUserId;
                    _context.Update(v);
                    _context.SaveChanges();
                    res.Add(new VariantUpdateMessageViewModel
                    {
                        Id = variant.Id,
                        IsOk = true,
                        Message = "عملیات با موفقیت انجام شد",
                        ProductId = v.ProductId

                    });
                    //}

                }
                catch (Exception e)
                {
                }
            }

            return res;
        }
    }

}
