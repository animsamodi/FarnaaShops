using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
    public interface ISearchSuggestionService : IBaseService<SearchSuggestion>
    {
        List<SearchSuggestion> GetListForAdmin();
        List<SearchSuggestion> GetListForUser();
        bool Add(SearchSuggestion SearchSuggestion);
        bool Update(SearchSuggestion SearchSuggestion);
        bool Delete(SearchSuggestion SearchSuggestion);
        SearchSuggestion FindById(long id);
    }
}
