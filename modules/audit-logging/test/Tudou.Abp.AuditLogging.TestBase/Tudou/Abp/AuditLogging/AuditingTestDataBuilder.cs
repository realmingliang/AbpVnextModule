using Volo.Abp.DependencyInjection;

namespace Tudou.Abp.AuditLogging
{
    public class AuditingTestDataBuilder : ITransientDependency
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditingTestDataBuilder(IAuditLogRepository auditLogRepository )
        {
            _auditLogRepository = auditLogRepository;
        }

        public void Build()
        {
            
        }
    }
}