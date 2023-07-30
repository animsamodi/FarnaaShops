using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Entities.Category;
using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Product;

namespace EShop.Core.Services.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        List<CategoryForBrandViewModel> GetCategoryByBrandId(long brandId);
        List<Category> GetSubCategoryById(long parentid);
        long GetCategoryIdByTitle(string title);
        ShowCategoriesForUserViewModel GetSubCategoryByName(string catName);
         List<Category> GetCategoriesForAdmin();
        Category FindCategoryById(long id);
        Category GetCategoryByTitle(string title);
        bool DeleteCategory(Category category);
        List<GetCategoryForAddViewModel> GetCategoriesForAdd();
        bool AddCategory(Category category, List<long> parentList);
        void AddOrUpdateParentCategory(long subCatId, List<long> parentList);
        List<long> GetSubCategory(long id);
        bool IsExistCategoryTitle(long catId, string enTitle);
        bool UpdateCategory(Category category, List<long> parentList);
        List<long> GetParentCategory(long id);
        bool AddProductCategories(List<ProductCategory> productCategory);
        List<GetCategoryForAddViewModel> GetCategoriesForAddCategory();
        List<Category> GetAllCategory();
        List<SubCategory> GetAllSubCategory();
        List<GetCategoryForAddViewModel> GetCategoriesForAddProduct();
        List<GetCategoryForTree> GetCategoriesForTree();
        GetCategoryForTree GetCategoryByIdForTree(long id);
        bool AddBrandCategories(List<BrandCategory> brandCategories);
        bool IsExistProductCategory(long catId, long productId);
        bool RemoveProductCategories(long pId);
        void RemoveAllProductCategories();
        Category GetCategoryAndSubsByTitle(string title);
        Category GetCategoryByFaTitle(string title);
    }
}