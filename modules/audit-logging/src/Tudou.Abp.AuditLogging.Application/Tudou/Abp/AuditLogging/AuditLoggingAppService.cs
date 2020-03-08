using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.AuditLogging
{
    public class AuditLoggingAppService : AuditLoggingAppServiceBase, IAuditLoggingAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AuditLoggingAppService(
            IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogDto> GetAuditLogByIdAsync(Guid id)
        {
            var auditLog = await _auditLogRepository.GetAsync(id);

            return ObjectMapper.Map<AuditLog, AuditLogDto>(auditLog);
        }

        public async Task<PagedResultDto<AuditLogDto>> GetAuditLogsAsync(GetAuditLogsInput input)
        {
            var count = await _auditLogRepository.GetCountAsync(null, null, input.HttpMethod, input.Url,
                input.UserName, input.ApplicationName, input.CorrelationId, input.MaxExecutionDuration,
                input.MinExecutionDuration, input.HasException, input.HttpStatusCode).ConfigureAwait(false);
            var list = await _auditLogRepository.GetListAsync(input.Sorting,
                input.MaxResultCount, input.SkipCount, null, null, input.HttpMethod, input.Url,
                input.UserName, input.ApplicationName, input.CorrelationId, input.MaxExecutionDuration,
                input.MinExecutionDuration, input.HasException, input.HttpStatusCode, true);

            return new PagedResultDto<AuditLogDto>(
                count,
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list)
            );
        }
        public async Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input)
        {
            var data = await _auditLogRepository.GetAverageExecutionDurationPerDayAsync(input.StartDate, input.EndDate);
            return new GetAverageExecutionDurationPerDayOutput()
            {

                Data = data
            };
        }
    }
}
