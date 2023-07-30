using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.ViewComponents
{
    public class SearchSuggestionComponent : ViewComponent
    {
        private ISearchSuggestionService _searchSuggestionService;

        public SearchSuggestionComponent(ISearchSuggestionService searchSuggestionService)
        {
            _searchSuggestionService = searchSuggestionService;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool isColleauge = false)
        {
            List<SearchSuggestion> value = new List<SearchSuggestion>();
     
                   value = _searchSuggestionService.GetListForUser();

            return await Task.FromResult(View("SearchSuggestion", value));
        }
    }
}