using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Ofogh;

namespace EShop.Core.Services.Interfaces
{
    public interface IOfoghService : IBaseService<OfoghHistory>
    {
        List<OfoghHistory> GetListForAdminKamkar();
        List<OfoghHistory> GetListForAdminKhord();
         bool Add(OfoghHistory OfoghHistory);
        bool Update(OfoghHistory OfoghHistory);
        bool Delete(OfoghHistory OfoghHistory);
        OfoghHistory FindById(long id);
        bool AddRange(List<OfoghHistory> ofoghHistories);
    }
}
