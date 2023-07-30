using System;
using System.Collections.Generic;
using System.Linq;
using EndPoint.Web.Utilities;
using EShop.Core.Helpers;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Variety;
using EShop.Logging.AuditLog.Models;
using Infrastructure.CacheHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace EShop.Admin.Controllers
{
    //[Area("Admin")]
    public class VariantController : BaseAdminController
    {
        readonly ICategoryService _categoryService;
        readonly IProductService _productService;
        readonly IBrandService _brandService;
        readonly IPropertyService _propertyService;
        readonly IGalleryService _galleryService;
        readonly IReviewService _reviewService;
        readonly IAttributeRatingService _attributeRatingService;
        readonly IVariantService _variantService;
        private IColleaguePriceRangeService _colleaguePriceRangeService; 
         private IColleaguePlusPriceRangeService _colleaguePlusPriceRangeService;
        private IPlusPriceRangeService _plusPriceRangeService;

        public VariantController(ICategoryService categoryService,
            IProductService productService, IBrandService brandService,
            IGalleryService galleryService, IReviewService reviewService, 
            IAttributeRatingService attributeRatingService, 
            IPropertyService propertyService, IVariantService variantService,
            Logging.AuditLog.IAuditService logger,IHttpContextAccessor contextAccessor, IColleaguePriceRangeService colleaguePriceRangeService, IPlusPriceRangeService plusPriceRangeService, IColleaguePlusPriceRangeService colleaguePlusPriceRangeService) : base(logger,contextAccessor)
        {

            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _galleryService = galleryService;
            _reviewService = reviewService;
            _attributeRatingService = attributeRatingService;
            _propertyService = propertyService;
            _variantService = variantService;
            _colleaguePriceRangeService = colleaguePriceRangeService;
             _plusPriceRangeService = plusPriceRangeService;
            _colleaguePlusPriceRangeService = colleaguePlusPriceRangeService;
        }
        [IgnoreAntiforgeryToken]

        public IActionResult IndexPlus(string search = "")
        {


            var res = _variantService.GetListVariants(search);

            TempData["Search"] = search;

            return View(res);
        }
        [IgnoreAntiforgeryToken]

        public IActionResult IndexColleague(string search = "")
        {


            var res = _variantService.GetListVariants(search);

            TempData["Search"] = search;

            return View(res);
        }
        public IActionResult IndexPlusColleague(string search = "")
        {


            var res = _variantService.GetListVariants(search);

            TempData["Search"] = search;

            return View(res);
        }
        [IgnoreAntiforgeryToken]

        public IActionResult Index(string search = "")
        {
            var res = _variantService.GetListVariants(search);
            TempData["Search"] = search;
            return View(res);
        }
        [IgnoreAntiforgeryToken]

        public IActionResult IndexPreSell(string search = "")
        {
            var res = _variantService.GetListVariants(search);
            TempData["Search"] = search;
            return View(res);
        }
        [IgnoreAntiforgeryToken]

        public IActionResult UpdateProductsVariantsColleague(long[] VariantIds, int[] MainPrices, int[] Counts, int[] IncreaseCounts, int[] DecreaseCounts, int[] MaxOrderCounts,int[] GetColleaguePriceFromOrginals, string search = "")
        {
            var userId = ClaimUtility.GetUserId(User);
            var lstChaneColleaugePrice = _colleaguePriceRangeService.GetListActiveRange();

            List<VariantUpdateViewModel> variants = new List<VariantUpdateViewModel>();
            for (int i = 0; i < VariantIds.Length; i++)
            {
                var item = new VariantUpdateViewModel();
                item.Id = VariantIds[i];
                item.PriceColleague = MainPrices[i];
                item.ChangeUserId = userId; 
                //IncreaseCount = Math.Abs(IncreaseCounts[i]),
                //DecreaseCount = Math.Abs(DecreaseCounts[i])
                item.IncreaseCount = 0;
                item.DecreaseCount = 0;
                // CountColleague = Counts[i],
                item.MaxOrderCountColleague = MaxOrderCounts[i] > 0 ? MaxOrderCounts[i] : 1;
                item.GetColleaguePriceFromOrginal = GetColleaguePriceFromOrginals[i] == 1;
                //var changePrice = lstChaneColleaugePrice.FirstOrDefault(c => item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice);
                //if (changePrice != null)
                //  item.ChangePrice = changePrice.ChangePrice;
                variants.Add(item);
            }

          var res =  _variantService.UpdateProductsVariantsColleague(variants, lstChaneColleaugePrice);
        
            // TempData.Put("UpdateResult", res); ;
            return RedirectToAction("IndexColleague", new { search });
        }
        [IgnoreAntiforgeryToken]

        public IActionResult UpdateProductsVariantsPlusColleague(long[] VariantIds, int[] MainPrices, int[] Counts, int[] IncreaseCounts, int[] DecreaseCounts, int[] MaxOrderCounts,int[] GetPlusColleaguePriceFromOrginals, string search = "")
        {
            var userId = ClaimUtility.GetUserId(User);
            var lstChangePlusColleaugePrice = _colleaguePlusPriceRangeService.GetListActiveRange();

            List<VariantUpdateViewModel> variants = new List<VariantUpdateViewModel>();
            for (int i = 0; i < VariantIds.Length; i++)
            {
                var item = new VariantUpdateViewModel();
                item.Id = VariantIds[i];
                item.PriceColleaguePlus = MainPrices[i];
                item.ChangeUserId = userId; 
                //IncreaseCount = Math.Abs(IncreaseCounts[i]),
                //DecreaseCount = Math.Abs(DecreaseCounts[i])
                item.IncreaseCount = 0;
                item.DecreaseCount = 0;
                // CountColleague = Counts[i],
                item.MaxOrderCountColleaguePlus = MaxOrderCounts[i] > 0 ? MaxOrderCounts[i] : 1;
                item.GetColleaguePricePlusFromOrginal = GetPlusColleaguePriceFromOrginals[i] == 1;
                //var changePrice = lstChaneColleaugePrice.FirstOrDefault(c => item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice);
                //if (changePrice != null)
                //  item.ChangePrice = changePrice.ChangePrice;
                variants.Add(item);
            }

          var res =  _variantService.UpdateProductsVariantsColleaguePlus(variants, lstChangePlusColleaugePrice);
        
            // TempData.Put("UpdateResult", res); ;
            return RedirectToAction("IndexPlusColleague", new { search });
        }
        [IgnoreAntiforgeryToken]

        public IActionResult UpdateProductsVariantsPlus(long[] VariantIds, int[] MainPrices,int[] SepcialPrices, int[] Counts, int[] IncreaseCounts, int[] DecreaseCounts, int[] MaxOrderCounts,int[] GetPlusPriceFromOrginals, string search = "")
        {
            var userId = ClaimUtility.GetUserId(User);
            var lstChanePlusPrice = _plusPriceRangeService.GetListActiveRange();

            List<VariantUpdateViewModel> variants = new List<VariantUpdateViewModel>();
            for (int i = 0; i < VariantIds.Length; i++)
            {
                var item = new VariantUpdateViewModel();
                item.Id = VariantIds[i];
                item.PricePlus= MainPrices[i];
                item.SepcialPlusPrice= SepcialPrices[i];
                item.ChangeUserId = userId; 
                //IncreaseCount = Math.Abs(IncreaseCounts[i]),
                //DecreaseCount = Math.Abs(DecreaseCounts[i])
                item.IncreaseCount = 0;
                item.DecreaseCount = 0;
                // CountPlus= Counts[i],
                item.MaxOrderCountPlus= MaxOrderCounts[i] > 0 ? MaxOrderCounts[i] : 1;
                item.GetPlusPriceFromOrginal = GetPlusPriceFromOrginals[i] == 1;
                //var changePrice = lstChanePlusPrice.FirstOrDefault(c => item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice);
                //if (changePrice != null)
                //  item.ChangePrice = changePrice.ChangePrice;
                variants.Add(item);
            }

          var res =  _variantService.UpdateProductsVariantsPlus(variants, lstChanePlusPrice);
        
            // TempData.Put("UpdateResult", res); ;
            return RedirectToAction("IndexPlus", new { search });
        }
        [IgnoreAntiforgeryToken]

        public IActionResult UpdateProductsVariants(long[] VariantIds, int[] MainPrices, int[] SellerPrices, int[] Counts, int[] IncreaseCounts, int[] DecreaseCounts, int[] MaxOrderCounts, string search = "")
        {
            var userId = ClaimUtility.GetUserId(User);
            var lstChangeColleaugePrice = _colleaguePriceRangeService.GetListActiveRange();
            var lstChangeColleaugePlusPrice = _colleaguePlusPriceRangeService.GetListActiveRange();
            var lstChangePlusPrice = _plusPriceRangeService.GetListActiveRange();
            List<VariantUpdateViewModel> variants = new List<VariantUpdateViewModel>();
            for (int i = 0; i < VariantIds.Length; i++)
            {
                if (SellerPrices[i] == 0)
                    SellerPrices[i] = MainPrices[i];

                if (SellerPrices[i] > MainPrices[i])
                    SellerPrices[i] = MainPrices[i];

                var item = new VariantUpdateViewModel();
                item.Id = VariantIds[i];
                item.Price = MainPrices[i];
                item.SepcialPrice = SellerPrices[i]; //Count = Counts[i],
                item.MaxOrderCount = MaxOrderCounts[i] > 0 ? MaxOrderCounts[i] : 1;
                item.ChangeUserId = userId;
                item.IncreaseCount = Math.Abs(IncreaseCounts[i]);
                item.DecreaseCount = Math.Abs(DecreaseCounts[i]);
                //Colleague
                var changePrice = lstChangeColleaugePrice.FirstOrDefault(c=> item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice  );
                if (changePrice != null)
                {
                    item.PriceColleague = item.SepcialPrice - changePrice.ChangePrice;
                    item.MaxOrderCountColleague = 1;
                }
                //Colleague Plus
                var changePriceColleaguePlus = lstChangeColleaugePlusPrice.FirstOrDefault(c=> item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice  );
                if (changePriceColleaguePlus != null)
                {
                    item.PriceColleaguePlus = (item.SepcialPrice + (item.SepcialPrice * changePriceColleaguePlus.ChangePricePercent / 100)).RoundPrice();
                    item.MaxOrderCountColleaguePlus = 1;
                }
                //Plus
                var changePricePlus = lstChangePlusPrice.FirstOrDefault(c => item.SepcialPrice > c.MinPrice && item.SepcialPrice < c.MaxPrice);
                if (changePricePlus != null)
                {
                    item.PricePlus= (item.Price + (item.Price * changePricePlus.ChangePricePercent / 100)).RoundPrice();
                    item.SepcialPlusPrice=(item.SepcialPrice + (item.SepcialPrice * changePricePlus.ChangePricePercent / 100)).RoundPrice();
                    item.MaxOrderCountPlus = 1;

                }
                variants.Add(item);
                //

            }

            var res = _variantService.UpdateProductsVariants(variants);
           
            // TempData.Put("UpdateResult", res); ;
            return RedirectToAction("Index", new { search });
        }
        [IgnoreAntiforgeryToken]

        public IActionResult UpdateProductsVariantsPreSell(long[] VariantIds, string[] DateSells, string search = "")
        {
            var userId = ClaimUtility.GetUserId(User);

            List<Variant> variants = new List<Variant>();
            for (int i = 0; i < VariantIds.Length; i++)
            {


                if (DateSells[i] != null && DateSells[i].PersianToEnglish().ConvertShamsiToMiladi().ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    var variant = new Variant();
                    variant.Id = VariantIds[i];
                    variant.ChangeUserId = userId;
                    variant.DateSell = DateSells[i].PersianToEnglish().ConvertShamsiToMiladi();
                    variants.Add(variant);
                }

            }

            _variantService.UpdateProductsVariantsPreSell(variants);
            _logger.CreateAuditScope(new AuditLog<List<Variant>>()
            {
                Modifier = _userId,
                Action = Command.Update,
                Entite = variants,
            });
            
            return RedirectToAction("IndexPreSell", new { search });
        }


        #region Color
        public IActionResult ColorList()
        {
            var res = _variantService.GetListProductOption();
            return View(res);
        }
        public IActionResult CreateColor() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateColor(ProductOption productOption)
        {
            if (!ModelState.IsValid)
                return View(productOption);

            TempData["res"] = "faild";
            if (_variantService.CreateColor(productOption))
            {
                _logger.CreateAuditScope(new AuditLog<ProductOption>()
                {
                    Modifier = _userId,
                    Action = Command.Create,
                    Entite = productOption,
                });
                TempData["res"] = "success";
            }

            return RedirectToAction(nameof(ColorList));
        }

        public IActionResult EditColor(int id)
        {
            ProductOption productOption = _variantService.FindColorById(id);
            if (productOption == null)
            {
                TempData["res"] = "faild";
                return RedirectToAction(nameof(ColorList));
            }

            return View(productOption);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditColor(ProductOption productOption)
        {
            if (!ModelState.IsValid)
                return View(productOption);



            TempData["res"] = "faild";
            if (_variantService.UpdateColor(productOption))
            {
                _logger.CreateAuditScope(new AuditLog<ProductOption>()
                {
                    Modifier = _userId,
                    Action = Command.Update,
                    Entite = productOption,
                });
                TempData["res"] = "success";
            }
            
            return RedirectToAction(nameof(ColorList));
        }

        #endregion




    }
}