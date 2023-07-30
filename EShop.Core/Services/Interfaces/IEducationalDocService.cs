using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IEducationalDocService : IBaseService<EducationalDoc>
    {
        List<EducationalDoc> GetListForAdmin();
        List<EducationalDoc> GetListForUser();
        bool Add(EducationalDoc indexLayout);
        bool Update(EducationalDoc indexLayout);
        bool Delete(EducationalDoc indexLayout);
        EducationalDoc FindById(long id);
    }
}