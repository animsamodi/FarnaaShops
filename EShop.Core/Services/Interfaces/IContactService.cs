using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;

namespace EShop.Core.Services.Interfaces
{
    public interface IContactService : IBaseService<Contact>
    {
        List<Contact> GetListForAdmin();
        List<Contact> GetListForUser();
        bool Add(Contact Contact);
        bool Update(Contact Contact);
        bool Delete(Contact Contact);
        Contact FindById(long id);
        Contact GetActiveRow();
    }
}
