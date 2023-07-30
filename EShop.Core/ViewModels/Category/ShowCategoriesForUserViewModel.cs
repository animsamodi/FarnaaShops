using System.Collections.Generic;

namespace EShop.Core.ViewModels.Category
{
   public class ShowCategoriesForUserViewModel
    {
        public string FaTitle { get; set; }
        public List<DataLayer.Entities.Category.Category> categories { get; set; } 
    }
}
