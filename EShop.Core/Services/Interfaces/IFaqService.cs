using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
    public interface IFaqService : IBaseService<Faq>
    {
        List<Faq> GetListForAdmin();
        List<Faq> GetListForUser();
        bool Add(Faq Faq);
        bool Update(Faq Faq);
        bool Delete(Faq Faq);
        Faq FindById(long id);
    }
}
