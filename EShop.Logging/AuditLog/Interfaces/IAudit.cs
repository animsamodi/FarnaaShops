
using System;

namespace EShop.Logging.AuditLog.Interfaces
{
    public class Audit
    {
        public Audit()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}