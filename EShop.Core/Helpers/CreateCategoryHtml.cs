using System.Collections.Generic;
using System.Linq;
using EShop.Core.ViewModels.Category;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Entities.Category;

namespace EShop.Core.Helpers
{
    public static class CreateCategoryHtml
    {

        public static List<MainCategoryFilterItem> GetCategoryTreeDropDownItems(
            List<Category> values)
        {

            List<MainCategoryFilterItem> result = new List<MainCategoryFilterItem>();
            foreach (var item in values)
            {
                var subs = new List<CategoryFilterItem>();
                if (item.SubCategory.Any(a => a.SubCat.FaTitle!=item.FaTitle))
                {
                  
                    subs = item.SubCategory
                        .Select(q => new CategoryFilterItem(q.SubCat.Id, q.SubCat.FaTitle, q.SubCat.EnTitle)).ToList();
                    
                }
                result.Add(new MainCategoryFilterItem(item.Id, item.EnTitle, item.FaTitle,subs ));
            }
            return result;
        }
       

        public static string GetHtmlCategory(List<SearchCategoryViewModel> values, long active_catid, long? parentid, string query)
        {
            string str = "";
            query = query.Length > 1 ? "?" + query : "";
            if (parentid == null)
            {
                foreach (var item in values.Distinct().Where(c => c.ParentId == null))
                {
                    str += "<li class='c-box_cat-item'>"
                        + "<span class='c-box_cat-span c-box_cat-span-arrow_down'>"
                       + "<a href='?catid=" + item.CategoryId + query + "' class='c-box_cat-link " + (item.CategoryId == active_catid ? "is-active" : "") + "'>" + item.Title + "</a>"
                        + "</span>";

                    str += GetHtmlCategory(values, active_catid, item.CategoryId, query);
                    str += "</li>";
                }
            }
            else
            {
                foreach (var item in values.Where(p => p.ParentId == parentid))
                {
                    str += "<ul class='c-box_cat-list'><li class='c-box_cat-item'>";

                    if (values.Any(c => c.ParentId == item.CategoryId))
                    {
                        //categories
                        str += "<span class='c-box_cat-span c-box_cat-span-arrow_left'>"
                       + "<a href='/category/" + item.CategoryEnTitle.ToUrlFormat() + query + "' class='c-box_cat-link " + (item.CategoryId == active_catid ? "is-active" : "") + "'>" + item.Title + "</a>"
                        + "</span>";
                        str += GetHtmlCategory(values, active_catid, item.CategoryId, query);
                    }
                    else
                    {
                        // subcategories
                        str += "<li class='c-box_cat-item'>"
                       + $"<a href='/category/{values.Distinct().Where(c => c.CategoryId == item.ParentId).FirstOrDefault().CategoryEnTitle.ToUrlFormat()}/{item.CategoryEnTitle.ToUrlFormat()}" + query + "' class='c-box_cat-link " + (item.CategoryId == active_catid ? "is-active" : "") + "'>" + item.Title + "</a>"
                       + "</li>";
                    }


                    str += "</li></ul>";
                }
            }
            return str;
        }
    }

}