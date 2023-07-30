using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Property;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.Product
{
    public class PropertyService :  BaseService<ProductProperty>, IPropertyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PropertyService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }

        #region Groups
        public List<PropertyGroup> GetPropertyGroup(int pagenum)
        {
            int skip = (pagenum - 1) * 20;
            return _context.PropertyGroups.OrderBy(c=>c.Priority).Skip(skip).Take(20).ToList();
        }

        public int GetPropertyGroupCount()
        {
            return _context.PropertyGroups.Count();
        }

        public List<PropertyGroup> GetPropertyGroupForAddNames()
        {
            return _context.PropertyGroups.ToList();
        }

        public List<ComparePropertyGroupVieWModel> GetProductGroupForCompare(long id)
        {
            var query = _context.PropertyCategories.Where(c => c.CategoryId == id)
                .Select(r => new ComparePropertyGroupVieWModel
                {
                    GroupTitle = r.PropertyName.PropertyGroup.Title,
                    NameId = r.PropertyName.Id,
                    NameTitle = r.PropertyName.Title
                }).ToList();
            return query;
        }
        #endregion

        #region Names
        public int GetPropertyNameType(int id)
        {
            return _context.PropertyNames.Where(pn => pn.Id == id).Select(pn => pn.Type).SingleOrDefault();
        }

        public bool CheckValueForName(int nameid, int valueid)
        {
            return _context.PropertyValues.Any(p => p.PropertyNameId == nameid && p.Id == valueid);
        }

        public long GetValueIdByValue(long nameId, string value)
        {
            var res =  _context.PropertyValues.FirstOrDefault(p => p.PropertyNameId == nameId && p.Value.Equals(value));
            return res?.Id ?? 0;
        }
        public bool CheckNameForCategory(int nameid, long catid)
        {
            return _context.PropertyCategories.Any(pc => pc.PropertyNameId == nameid && pc.CategoryId == catid);
        }
        public List<PropertyName> GetPropertyName()
        {
            return _context.PropertyNames
                .Include(c=>c.PropertyGroup)
                .Include(c=>c.PropertyCategories)
                .ThenInclude(c=>c.Category).OrderBy(c => c.Priority).ToList();
        }
        public long AddPropertyName(PropertyName name)
        {
            try
            {
                name.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(name);
                _context.SaveChanges();
                return name.Id;
            }
            catch
            {

                return 0;
            }


        }

        public PropertyName GetPropertyNameForEdit(long id)
        {
            return _context.PropertyNames.Where(c=>c.Id == id).Include(c=>c.PropertyCategories).ThenInclude(c=>c.Category).FirstOrDefault();
        }

        public bool EditPropertyName(PropertyName name)
        {
            name = name.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(name);
            var res = _context.SaveChanges();
            if (res > 0)
                return true;
            return false;
        }

        public bool AddNameCategory(List<PropertyCategory> propertyCategories)
        {
            try
            {
                _context.PropertyCategories.AddRange(propertyCategories);
                _context.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public Tuple<List<ProductPropertyAddAdminViewModel>, List<PropertyValueForAddViewModel>>
            GetProductPropertyForAdd(long catid, long productid)
        {
            List<ProductPropertyAddAdminViewModel> propertyNames = (from pc in _context.PropertyCategories.Where(pc => pc.CategoryId == catid)
                                                                    join pn in _context.PropertyNames on pc.PropertyNameId equals pn.Id
                                                                    select new ProductPropertyAddAdminViewModel
                                                                    {
                                                                        NameId = pn.Id,
                                                                        NameTitle = pn.Title,
                                                                        Type = pn.Type
                                                                    }).ToList();
            for (int i = 0; i < propertyNames.Count; i++)
            {
                if (propertyNames[i].Type != 3 && propertyNames[i].Type != 4)
                {
                    propertyNames[i].Values = _context.PropertyValues.Where(pv => pv.PropertyNameId == propertyNames[i].NameId)
                        .Select(pv => new PropertyValueForAddViewModel
                        {
                            ValueId = pv.Id,
                            NameId = pv.PropertyNameId,
                            Value = pv.Value
                        }).ToList();
                }
            }

            List<PropertyValueForAddViewModel> saveproperty = (from pp in _context.ProductProperties.Where(pp => pp.ProductId == productid)
                                                               join pv in _context.PropertyValues on pp.PropertyValueId equals pv.Id
                                                               join pn in _context.PropertyNames on pv.PropertyNameId equals pn.Id
                                                               select new PropertyValueForAddViewModel
                                                               {
                                                                   ProductProertyId = pp.Id,
                                                                   Value = pv.Value,
                                                                   ValueId = pv.Id,
                                                                   NameId = pn.Id
                                                               }).ToList();
            return Tuple.Create(propertyNames, saveproperty);
        }
        #endregion

        #region Vlaue

        public List<PropertyValue> GetPropertyValues()
        {
            return _context.PropertyValues.ToList();
        }

        public long AddPropertyValue(PropertyValue value)
        {
            value.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(value);
            var res = _context.SaveChanges();
            return value.Id;
        }

        public bool EditPropertyValueList(List<PropertyValue> propertyValues)
        {
foreach (var propertyValue in propertyValues)
            {
                  propertyValue.LastUpdateDate = DateTime.Now;
                
             }
            _context.UpdateRange(propertyValues);
            _context.SaveChanges();
            return true;

        }
        public bool DeletePropertyValueList(List<PropertyValue> propertyValues)
        {
            try
            {
                foreach (var propertyValue in propertyValues)
            {
                  propertyValue.LastUpdateDate = DateTime.Now;
                  propertyValue.IsDelete = true;
            }
            _context.UpdateRange(propertyValues);
                 _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddPropertyValueList(List<PropertyValue> propertyValues)
        {
            try
            {foreach (var propertyValue in propertyValues)
            {
                propertyValue.IsDelete = false;
                propertyValue.CreateDate = DateTime.Now;
                propertyValue.LastUpdateDate = DateTime.Now;
                
             }
                _context.AddRange(propertyValues);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region ProductProperty
        public List<PropertyForSearchViewModel> GetPropertyForSearch(long catid)
        {
            return (from q in _context.PropertyNames.Where(c => c.UseSearch)
                    join c in _context.PropertyCategories on q.Id equals c.PropertyNameId
                    where c.CategoryId == catid
                    select new PropertyForSearchViewModel
                    {
                        PropertyName = q.Title,
                        PropertyValues = q.PropertyValues
                    }).ToList();
        }

        public long AddPropertyGroup(PropertyGroup propertyGroup)
        {
            try
            {
              

                propertyGroup = propertyGroup.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(propertyGroup);
                _context.SaveChanges();
                return propertyGroup.Id;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public void RemoveNameCategoryByPropertyNameId(long id)
        {
            var query = _context.PropertyCategories.Where(c => c.PropertyNameId == id).ToList();
            foreach (var propertyCategory in query)
            {
                propertyCategory.SetRemoveDefaultValue(_userService.GetUserId());
            }
            _context.UpdateRange(query);
            _context.SaveChanges();
        }


        public bool DeleteProductPropertyList(List<ProductProperty> productProperties)
        {
            try
            {
                var updateList =new List<ProductProperty>();
                foreach (var productProperty in productProperties)
                {
                    var updateEntity = _context.ProductProperties.Find(productProperty.Id);
                    if (updateEntity != null)
                    {
                        updateEntity.LastUpdateDate = DateTime.Now;
                        updateEntity.IsDelete = true;
                        updateList.Add(updateEntity);
                    }
                }
            _context.UpdateRange(updateList);
                 _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddProductPropertyList(List<ProductProperty> productProperties)
        {
            try
            {foreach (var productProperty in productProperties)
            {
                productProperty.IsDelete = false;
                productProperty.CreateDate = DateTime.Now;
                productProperty.LastUpdateDate = DateTime.Now;
                
             }
                _context.AddRange(productProperties);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

    }
}
