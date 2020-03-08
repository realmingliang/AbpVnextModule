using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.AuditLogging
{
    [RemoteService]
    [Area("audit-logging")]
    [ControllerName("AuditLogs")]
    [Route("api/audit-logging/audit-logs")]
    public class AuditLoggingController : AbpController, IAuditLoggingAppService
    {
        private readonly IAuditLoggingAppService _autidLogAppService;

        public AuditLoggingController(IAuditLoggingAppService autidLogAppService) {

            _autidLogAppService = autidLogAppService;
        }
        [HttpGet]
        [Route("{id}")]
        public Task<AuditLogDto> GetAuditLogByIdAsync(Guid id)
        {
            return _autidLogAppService.GetAuditLogByIdAsync(id);
        }
        [HttpGet]
        public Task<PagedResultDto<AuditLogDto>> GetAuditLogsAsync(GetAuditLogsInput input)
        {
            return _autidLogAppService.GetAuditLogsAsync(input);
        }
        [HttpGet]
        [Route("statistics/average-execution-duration-per-day")]
        public Task<GetAverageExecutionDurationPerDayOutput> GetAverageExecutionDurationPerDay(GetAverageExecutionDurationPerDayInput input)
        {
            return _autidLogAppService.GetAverageExecutionDurationPerDay(input);
        }
    }
}
