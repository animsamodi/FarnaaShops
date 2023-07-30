using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Brand;

namespace EShop.Core.Services.Interfaces
{
    public interface IBrandService : IBaseService<Brand>
    {
        Brand GetBrandByName(string brandTitle);
        List<BrandForAddProductViewModel> GetBrandsForAddProduct();
        List<BranListdAdminViewModel> GetBrandListForAdmin();
        bool AddBrand(Brand brand);
        Brand GetBrandById(long id);
        bool EditBrand(Brand brand);
        List<Brand> GetBrandByCategoryId(long id);
        List<Brand> GetBrandByCategoryTitle(string title);

        List<Brand> GetListBrand();

        bool DeleteBrand(Brand brand);
        List<Brand> GetAllBrands();
        void RemoveAllBrandCategories();
        List<Brand> GetBrandForFilterItems(long categoryId);
        List<Brand> GetListUserShowBrand();
        Brand GetBrandByTitle(string title);
    }
}
