using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Property;

namespace EShop.Core.Services.Interfaces
{
    public interface IPropertyService : IBaseService<ProductProperty>
    {
        #region group
        int GetPropertyGroupCount();
        List<PropertyGroup> GetPropertyGroup(int pagenum);
        List<PropertyGroup> GetPropertyGroupForAddNames();
        List<ComparePropertyGroupVieWModel> GetProductGroupForCompare(long id);
        #endregion

        #region name
        long AddPropertyName(PropertyName name);
        List<PropertyName> GetPropertyName();
        bool CheckNameForCategory(int nameid, long catid);
        bool CheckValueForName(int nameid, int valueid);
        long GetValueIdByValue(long nameId, string value);
        PropertyName GetPropertyNameForEdit(long id);
        bool EditPropertyName(PropertyName name);
        bool AddNameCategory(List<PropertyCategory> propertyCategories);
        int GetPropertyNameType(int id);
        Tuple<List<ProductPropertyAddAdminViewModel>, List<PropertyValueForAddViewModel>> GetProductPropertyForAdd(
            long catid, long productid);
        #endregion

        #region value
        List<PropertyValue> GetPropertyValues();
        long AddPropertyValue(PropertyValue vlaue);
        bool EditPropertyValueList(List<PropertyValue> propertyValues);
        bool DeletePropertyValueList(List<PropertyValue> propertyValues);
        bool AddPropertyValueList(List<PropertyValue> propertyValues);

        #endregion
        #region ProductProperty
        bool DeleteProductPropertyList(List<ProductProperty> productProperties);
        bool AddProductPropertyList(List<ProductProperty> productProperties);
        #endregion
        List<PropertyForSearchViewModel> GetPropertyForSearch(long catid);

        long AddPropertyGroup(PropertyGroup propertyGroup);
        void RemoveNameCategoryByPropertyNameId(long id);
    }
}
