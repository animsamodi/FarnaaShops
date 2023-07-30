using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels.Category;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Brand;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Product;

namespace EShop.Core.Services.Implementations
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        #region constructor

        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CategoryService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }
        #endregion

        #region Category Section



        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();

        }

        public List<CategoryForBrandViewModel> GetCategoryByBrandId(long id)
        {
            var res = _context.Categories.AsQueryable().Include(c => c.BrandCategories)
                .Where(
                    c => c.BrandCategories.Any(brand => brand.BrandId == id))
                .Select(s => new CategoryForBrandViewModel
                {
                    Id = s.Id,
                    FaTitle = s.FaTitle
                });

            return res.ToList();
        }



        public Category GetCategoryByName(string catname)
        {
            return _context.Categories.AsQueryable().FirstOrDefault(c => c.EnTitle == catname);
        }

        public List<Category> GetSubCategoryById(long parentid)
        {
            var res = _context.Categories.AsQueryable()
                .Include(c => c.SubCategory)
                .Where(c => c.SubCategory.Any(sub => sub.ParentId == parentid));

            return res.ToList();
        }


        public ShowCategoriesForUserViewModel GetSubCategoryByName(string catName)
        {
            Category category = GetCategoryByName(catName);
            if (category == null)
                return null;

            ShowCategoriesForUserViewModel showCategoriesvm = new ShowCategoriesForUserViewModel
            {
                FaTitle = category.FaTitle,
                categories = GetSubCategoryById(category.Id)
            };
            return showCategoriesvm;
        }




        public List<Category> GetCategoriesForAdmin()
        {
            return _context.Categories.AsQueryable().ToList();
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                _context.Categories.Attach(category);
                _context.Entry(category).State = EntityState.Modified;
                category = category.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(category);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;

            }

        }

        public Category FindCategoryById(long id)
        {
            return _context.Categories.Find(id);
        }

        public List<GetCategoryForAddViewModel> GetCategoriesForAdd()
        {
            return _context.Categories.AsQueryable()
                .Where(c => c.IsMain == true)
                .Select(c => new GetCategoryForAddViewModel
                {
                    Id = c.Id,
                    Title = c.FaTitle
                }).ToList();
        }

        public List<GetCategoryForAddViewModel> GetCategoriesForAddCategory()
        {
            return _context.Categories.AsQueryable()
                .Select(c => new GetCategoryForAddViewModel
                {
                    Id = c.Id,
                    Title = c.FaTitle
                }).ToList();
        }

        public bool AddCategory(Category category, List<long> parentList)
        {
            try
            {
                category = category.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(category);
                _context.SaveChanges();

                if (parentList != null)
                {
                    AddOrUpdateParentCategory(category.Id, parentList);

                }

                return true;

            }
            catch (Exception e)
            {
                return false;

            }

        }

        public void AddOrUpdateParentCategory(long subcatid, List<long> newparentList)
        {
            List<SubCategory> addlist = new List<SubCategory>();
            List<SubCategory> parentlist = _context.SubCategories.AsQueryable()
                .AsNoTracking().Where(s => s.SubId == subcatid).ToList();
            if (parentlist.Count > 0)
            {
                List<SubCategory> removelist = new List<SubCategory>();
                foreach (var item in parentlist)
                {
                    if (newparentList != null)
                    {
                        if (newparentList.Contains(item.ParentId))
                        {
                            newparentList.Remove(item.ParentId);
                        }
                        else
                        {
                            removelist.Add(new SubCategory
                            {
                                Id = item.Id,
                                SubId = item.SubId,
                                ParentId = item.ParentId,
                                IsDelete = true,
                                LastUpdateDate = DateTime.Now
                            });
                        }
                    }
                    else
                    {
                        removelist.Add(new SubCategory
                        {
                            Id = item.Id,
                            SubId = item.SubId,
                            ParentId = item.ParentId,
                            IsDelete = true,
                            LastUpdateDate = DateTime.Now
                        });
                    }
                }

                if (newparentList != null && newparentList.Count > 0)
                {
                    foreach (var item in newparentList)
                    {
                        addlist.Add(new SubCategory
                        {
                            SubId = subcatid,
                            ParentId = item,
                            IsDelete = false,
                            CreateDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now
                        });
                    }
                }
                _context.UpdateRange(removelist);

            }
            else
            {
                if (newparentList != null)
                {
                    foreach (var item in newparentList)
                    {
                        addlist.Add(new SubCategory
                        {
                            SubId = subcatid,
                            ParentId = item,
                            IsDelete = false,
                            CreateDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now
                        });
                    }

                }
            }
            _context.AddRange(addlist);
            _context.SaveChanges();

        }

        public List<long> GetSubCategory(long id)
        {
            return _context.SubCategories.AsQueryable().Where(s => s.SubId == id).Select(s => s.ParentId).ToList();
        }




        public bool IsExistCategoryTitle(long catId, string enTitle)
        {
            return _context.Categories.AsQueryable().Any(c => c.Id != catId && c.EnTitle == enTitle);
        }

        public bool UpdateCategory(Category category, List<long> parentList)
        {
            try
            {
                _context.Categories.Attach(category);
                _context.Entry(category).State = EntityState.Modified;
                category = category.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(category);
                _context.SaveChanges();

                AddOrUpdateParentCategory(category.Id, parentList);
                return true;
            }
            catch (Exception e)
            {
                return false;

            }


        }

        public List<long> GetParentCategory(long id)
        {
            List<long> parentList = new List<long>();
            List<long> temp = new List<long>();

            List<long> temp2 = _context.SubCategories.AsQueryable().AsNoTracking().Where(s => s.SubId == id).Select(s => s.ParentId).ToList();
            label1: parentList.AddRange(temp2);
            temp.Clear();
            temp.AddRange(temp2);
            temp2.Clear();
            foreach (var item in temp)
            {
                temp2.AddRange(_context.SubCategories.AsQueryable().AsNoTracking().Where(s => s.SubId == item).Select(s => s.ParentId).ToList());
                if (item.Equals(temp.Last()))
                    goto label1;
            }

            return parentList.Distinct().ToList();
        }

        public bool AddProductCategories(List<ProductCategory> productCategory)
        {
            try
            {
                foreach (var category in productCategory)
                {

                    category.IsDelete = false;

                    category.CreateDate = DateTime.Now;
                    category.LastUpdateDate = DateTime.Now;
                }
                _context.AddRange(productCategory);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;

            }



        }




        public List<SubCategory> GetAllSubCategory()
        {
            return _context.SubCategories.AsQueryable().ToList();
        }

        public List<GetCategoryForAddViewModel> GetCategoriesForAddProduct()
        {
            return _context.Categories.AsQueryable()
                .Where(c => c.IsMain == true)
                .Select(c => new GetCategoryForAddViewModel
                {
                    Id = c.Id,
                    Title = c.FaTitle
                }).ToList();
        }

        public List<GetCategoryForTree> GetCategoriesForTree()
        {
            var query = _context.Categories.Include(c=>c.SubCategory)
                .Select(c=>new GetCategoryForTree
            {
                Id = c.Id,
                ParentId = c.SubCategory.FirstOrDefault().ParentId,
                FaTitle = c.FaTitle,
                Descrption = c.Descrption,
                EnTitle = c.EnTitle ?? c.FaTitle,
                KeyWord = c.KeyWord,
                FAQSchema = c.FAQSchema,
                MetaTitle=c.MetaTitle
            });

            return query.ToList();
        }

        public GetCategoryForTree GetCategoryByIdForTree(long id)
        {
            var query = _context.Categories.Include(c => c.SubCategory)
                .Where(c=>c.Id == id)
                .Select(c => new GetCategoryForTree
                {
                    Id = c.Id,
                    ParentId = c.SubCategory.FirstOrDefault().ParentId,
                    FaTitle = c.FaTitle,
                    Descrption = c.Descrption,
                    EnTitle = c.EnTitle ?? c.FaTitle,
                    KeyWord = c.KeyWord,
                    FAQSchema = c.FAQSchema
                });

            return query.FirstOrDefault();
        }

        public bool AddBrandCategories(List<BrandCategory> brandCategories)
        {
            try
            {
                foreach (var brandCategory in brandCategories)
                {
                    var isExist = _context.BrandCategories.Count(c =>
                                      c.BrandId == brandCategory.BrandId && c.CategoryId == brandCategory.CategoryId) >
                                  0;
                    if (!isExist)
                    {
                        _context.Add(brandCategory);
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

        public bool IsExistProductCategory(long catId, long productId)
        {
            return _context.ProductCategories.Any(c => c.CategoryId == catId && c.ProductId == productId) ;
        }

        public bool RemoveProductCategories(long pId)
        {
            try
            {
                var res = _context.ProductCategories.Where(c => c.ProductId == pId).ToList();
                foreach (var productCategory in res)
                {
                    productCategory.SetRemoveDefaultValue(_userService.GetUserId());
                }
                _context.UpdateRange(res);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RemoveAllProductCategories()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [ProductCategories]");
        }

        private string GetCategorytitle(Category cat, ref long id, ref bool isLast)
        {
            var text = cat.FaTitle;
            var parents = _context.SubCategories.Where(c => c.SubId == cat.Id).Include(c => c.ParentCategory).Include(c => c.SubCat).ToList();

            if (parents.Any())
            {
                foreach (var child in parents)
                {
                    id = child.Id;
                    var text2 = GetCategorytitle(child.ParentCategory, ref id, ref isLast);

                    text =text2 + ">" +text  ;
                }
            }
            else
            {
                id = cat.Id;
            }

            return text;
        }
        public Category GetCategoryByTitle(string title)
        {
            return _context.Categories.Where(s => s.EnTitle.ToLower().Replace(" ","-").Equals(title.ToUrlFormat()))/*.Include(p=>p.Products)*/.FirstOrDefault();
        }
        public Category GetCategoryByFaTitle(string title)
        {
            return _context.Categories.Where(s => s.FaTitle.ToLower().Replace(" ", "-").Equals(title.ToUrlFormat())).FirstOrDefault();
        }
        public Category GetCategoryAndSubsByTitle(string title)
        {
            return _context.Categories.Where(s => s.EnTitle.ToLower().Replace(" ","-").Equals(title.ToUrlFormat()))
                .Include(p => p.SubCategory).Include(p=>p.ParentCategory).FirstOrDefault();
        }
        public Category GetCategoryAndParentByTitle(string title)
        {
            return _context.Categories.Where(s => s.EnTitle.Replace(" ", "-").Equals(title))
                .Include(p=>p.ParentCategory).Include(p => p.Products).FirstOrDefault();
        }
        public long GetCategoryIdByTitle(string title)
        {
       return     _context.Categories.Where(q => q.EnTitle.ToLower().Replace(" ", "") == title  || q.EnTitle.ToLower().Replace("-", "") == title).Select(q=>q.Id).FirstOrDefault();
        }

        #endregion


    }
}
