using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Order;
using System.Collections.Generic;

namespace EShop.Core.Services.Interfaces
{
    public interface ICrmService : IBaseService<CrmLog>
    {
        long AddCrmLog(CrmLog crmLog);
        CrmLog GetCrmLogById(long id);

        bool UpdateCrmLog(CrmLog crmLog);

        CrmAccount GetCrmAccount();

        void UpdateToken(string token);
        List<CrmLog> GetCrmLogByOrderId(long id);
    }
}