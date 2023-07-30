using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface IColleaugeSettingService : IBaseService<ColleaugeSetting>
    {
        List<ColleaugeSetting> GetListForAdmin();
        List<ColleaugeSetting> GetListForUser();
 
        bool Add(ColleaugeSetting entity);
        bool Update(ColleaugeSetting entity);
        bool Delete(ColleaugeSetting entity);
        ColleaugeSetting FindById(long id);
        ColleaugeSetting FindFirst();
        TopHeaderViewModel GetTopHeader(EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        ColleaugeSetting GetEntityByType(EnumTypeSystem typeSystem);
    }
}