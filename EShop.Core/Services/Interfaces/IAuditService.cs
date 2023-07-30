using System.Collections.Generic;
using System.Linq;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IAuditService : IBaseService<Audit>
    {
        IQueryable<Audit> GetListAudits(int skip,int take);
       
    }
}