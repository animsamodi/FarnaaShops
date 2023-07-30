using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Promotion;
using EShop.DataLayer.Entities.Variety;
using EShop.DataLayer.Enum;
using EShop.DataLayer.Migrations;

namespace EShop.Core.Services.Interfaces
{
   public interface IVariantService : IBaseService<Variant>
    {
        List<ProductOption> GetProductProductOptions(long productId);
        List<VariantsForProductDetailViewModel> GetVariantsByProductId(long productId, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        List<VariantsForProductDetailViewModel> GetAllVariantsByProductId(long productId);
        Tuple<int, int, int?> GetMinPriceAndCountByVariantId(int variantid);

        bool UpdateVariantAndVariantPromotion(List<DataLayer.Entities.Variety.Variant> variants, List<VariantPromotion> variantPromotions);
        List<DataLayer.Entities.Variety.Variant> GetVariants(List<long> id);
        void UpdateRangeVariant(List<DataLayer.Entities.Variety.Variant> variants);
        List<Guarantee> GetListGuarantee();
        List<ProductOption> GetListProductOption();
        long GetSellerId();
        bool AddVariant(Variant variant);
        List<VariantsProductDetailViewModel> GetListVariants(string search="");
        List<VariantPromotionsViewModel> GetListVariantPromotions( long id);
        long  AddVariantPromotion(VariantPromotionsViewModel model);
        Variant GetVariantsId(long id);
        bool EditVariant(Variant variant);
        bool DeleteVariantPromotion(long id);
        bool CheckVariantExist( long variantProductId,  long variantGuaranteeId,  long variantProductOptionId);
        bool EditVariantGrid(VariantsProductDetailViewModel variant);
        List<VariantUpdateMessageViewModel> UpdateProductsVariants(List<VariantUpdateViewModel> variants);
        bool CreateColor(ProductOption productOption);
        ProductOption FindColorById(long id);
        bool UpdateColor(ProductOption productOption);
        List<long> GetListProductProductOption( long productId);
        List<long> GetListProductGuarantees(long productId);
        Variant GetVariantExist(long productId,  long guarantee,  long color);
        void DeleteLastVarients(List<Variant> lastGuarantessColors);
        List<Variant> GetLastVariantsByProductId(long productId);
        bool DeleteVariant(long id);
        bool AddGuarantee(Guarantee guarantee);
        Guarantee GetGuaranteeById(long id);
        bool EditGuarantee(Guarantee guarantee);
        bool DeleteGuarantee(Guarantee guarantee);
        void UpdateProductsVariantsPreSell(List<Variant> variants);
        List<VariantUpdateMessageViewModel>  UpdateProductsVariantsColleague(List<VariantUpdateViewModel> variants,List<ColleaguePriceRange> lstChaneColleaugePrice);
        List<VariantUpdateMessageViewModel> UpdateProductsVariantsPlus(List<VariantUpdateViewModel> variants,List<PlusPriceRange> lstChangePlusPrice);
         Tuple<int, List<ProductListDiscountViewModel>> GetProductsVariantDiscountForAdmin(string searchtext, int pagenumber, int brnad, int category, int state, int take);

         bool UpdateColleaugePriceByRenge(int rengMinPrice, int rengMaxPrice, int rengChangePrice);
         bool ResetCustomColleaugePrice(List<ColleaguePriceRange> lstRange);
         bool ResetCustomPlusPrice(List<PlusPriceRange> lstRange);
         Product GetProductByVariantId(int id);
         VariantsProductDetailViewModel GetVariantsDetailById(int id);
        Tuple<int, int, int?> GetMinPriceAndCountByVariantIdAndUserType(int variantId, int userId);
        bool UpdatePlusPriceByRenge(int rengMinPrice, int rengMaxPrice, int rengChangePricePercent);
        bool ResetCustomPlusPriceColleague(List<ColleaguePlusPriceRange> lstRange);
        List<VariantUpdateMessageViewModel> UpdateProductsVariantsColleaguePlus(List<VariantUpdateViewModel> variants, List<ColleaguePlusPriceRange> lstChangePlusColleaugePrice);
    }
}
