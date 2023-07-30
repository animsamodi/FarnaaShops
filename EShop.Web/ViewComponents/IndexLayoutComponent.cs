using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using Infrastructure.CacheHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Nest;

namespace EShop.Web.ViewComponents
{
    public class IndexLayoutComponent : ViewComponent
    {
        private readonly IIndexLayoutService _indexLayoutService;
        private IUserService _userService;
   
        public IndexLayoutComponent(IIndexLayoutService indexLayoutService, IUserService userService)
        {
            _indexLayoutService = indexLayoutService;
            _userService = userService;
        
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<IndexLayoutViewModel> value = new List<IndexLayoutViewModel>();

                     
                value = _indexLayoutService.GetListForUser();

           


            return await Task.FromResult(View("IndexLayout", value));
        }
    }
}