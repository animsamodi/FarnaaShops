

using EShop.Logging.AuditLog.Interfaces;

namespace EShop.Logging.AuditLog.Models
{

    public class AuditLog<TEntity> : EShop.Logging.AuditLog.Interfaces.Audit
    {
        public string Modifier { get; set; }
        public TEntity Entite { get; set; }
        public Command Action { get; set; }

    }

    public enum Command : int
    {
        Create = 1,
        Update,
        Remove

    }
}
