using EShop.Core.Cache;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces.Components;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Core.Services.Implementations.Components
{
    public class UiCircleComponentService : BaseService<UiCircleComponent>, IUiCircleComponentService
    {
        private readonly ApplicationDbContext _context;
        //private readonly ICacheService _cacheService;

        public UiCircleComponentService(ApplicationDbContext context
            /*,ICacheService cacheService*/)
            : base(context)
        {
            _context = context;
            //_cacheService = cacheService;
        }

        public async Task<List<UiCircleComponent>> GetAll()
        {
            //var components = await  _cacheService.GetAsync<IEnumerable<UiCircleComponent>>(KeysConstants.COMPONENTKEY);

            //if (components == null || (components != null && components.Count() < 1))
            //{
            //    components = await _context.UiCiricleComponents.Where(i => i.IsActive && !i.IsDelete).OrderBy(c => c.Id).ToListAsync();
            //    await _cacheService.SetAsync(Cache.KeysConstants.COMPONENTKEY, components);
            //}
            var components = await _context.UiCiricleComponents.Where(i => i.IsActive && !i.IsDelete).OrderBy(c => c.Id).ToListAsync();

            return components.ToList();
        }
    }

}
