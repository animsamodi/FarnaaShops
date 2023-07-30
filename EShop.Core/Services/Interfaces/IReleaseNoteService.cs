using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IReleaseNoteService : IBaseService<ReleaseNote>
    {
        List<ReleaseNote> GetListForAdmin();
        List<ReleaseNote> GetListForUser();
        bool Add(ReleaseNote indexLayout);
        bool Update(ReleaseNote indexLayout);
        bool Delete(ReleaseNote indexLayout);
        ReleaseNote FindById(long id);
    }
}