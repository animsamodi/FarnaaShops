using System.Collections.Generic;
using EShop.Core.ViewModels.Product;

namespace EShop.Core.ViewModels.Category
{
    public class MainCategoryPageWithFilterViewModel:BasePageWithFilterViewModel
    {
        
        public long Id { get; set; }
        public string EnTitle { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string FAQSchema { get; set; }

        public List<CategotyMainProductsViewModel> CategotyMainProductsViewModels { get; set; }
    }
}