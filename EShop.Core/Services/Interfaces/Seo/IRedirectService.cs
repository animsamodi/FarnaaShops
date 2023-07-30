using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Seo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Services.Interfaces.Seo
{
    public interface IRedirectService : IBaseService<Redirect>
    {
        Tuple<int, List<Redirect>> GetListRedirect(string searchText, int pageNumber);
        List<Redirect> GetListRedirectForAdmin();
        Redirect FindRedirectById(long id);
        bool AddRedirect(Redirect redirect);
        bool EditRedirect(Redirect redirect);
        bool DeleteRedirect(Redirect redirect);
        bool RedirectIsExistOrNot(string oldUrl);
        public Task<IEnumerable<Redirect>>  GetAllRedirects();
    }
}
