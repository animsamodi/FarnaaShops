
using EShop.Logging.AuditLog.Models;

namespace EShop.Logging.AuditLog
{
    public interface IAuditService
    {
        void CreateAuditScope<TEntity>(AuditLog<TEntity> log);
    }
}