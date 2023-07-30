

using Audit.Core;
using Audit.Elasticsearch.Providers;
using EShop.DataLayer.Configurations;
using EShop.Logging.AuditLog.Models;
using EShop.Logging.AuditLog.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EShop.Logging.AuditLog
{
    public class AuditService : IAuditService
    {
        private readonly string _index = "";
        private readonly IAuditScopeFactory _auditor;
        private readonly IConfiguration _configuration;
        
        public AuditService(IAuditScopeFactory auditor, IConfiguration configuration)
        {
            // _auditor = auditor;
            // _configuration = configuration;
            // _index = _configuration.GetSection("ElasticConnectionSettings").GetSection("ElasticAuditIndex").Value;
            // ConfigureLoggerProvider();
        }
        private void ConfigureLoggerProvider()
        {
            // Configuration.Setup()
            //   .UseElasticsearch(config => config.ConnectionSettings(new 
            //   Uri(_configuration.GetSection("ElasticConnectionSettings")
            //   .GetSection("ElasticSearchHost").Value))
            //   .Index(auditEvent => auditEvent.EventType)
            //   .Id(ev => Guid.NewGuid()));
        }
        public void CreateAuditScope<TEntity>(AuditLog<TEntity> log)
        {
            // var audit = AuditScope.Create(_index, () => log.Entite,new
            //     {
            //         modifier = log.Modifier,
            //         action= Command.Create,
            //         entite=log.Entite.GetType().Name
            //     });
            //
            // audit.Save();
            // audit.Dispose();
        }
    }
}