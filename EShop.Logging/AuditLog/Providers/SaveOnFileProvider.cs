
using Audit.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EShop.Logging.AuditLog.Providers
{
    public class SaveOnFileProvider : AuditDataProvider
    {
        public override object InsertEvent(AuditEvent auditEvent)
        {
            var fileName = $"Log{Guid.NewGuid()}.json";
            File.WriteAllText(fileName, auditEvent.ToJson(),encoding:System.Text.Encoding.UTF8);
            return fileName;
        }
        public override void ReplaceEvent(object eventId, AuditEvent auditEvent)
        {
            var fileName = eventId.ToString();
            File.WriteAllText(fileName, auditEvent.ToJson(), encoding: System.Text.Encoding.UTF8);
        }
        public override T GetEvent<T>(object eventId)
        {
            var fileName = eventId.ToString();
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
        // async implementation:
        public override async Task<object> InsertEventAsync(AuditEvent auditEvent)
        {
            var fileName = $"Log{Guid.NewGuid()}.json";
            await File.WriteAllTextAsync(fileName, auditEvent.ToJson(), encoding: System.Text.Encoding.UTF8);
            return fileName;
        }
        public override async Task ReplaceEventAsync(object eventId, AuditEvent auditEvent)
        {
            var fileName = eventId.ToString();
            await File.WriteAllTextAsync(fileName, auditEvent.ToJson(), encoding: System.Text.Encoding.UTF8);
        }
    }
}

