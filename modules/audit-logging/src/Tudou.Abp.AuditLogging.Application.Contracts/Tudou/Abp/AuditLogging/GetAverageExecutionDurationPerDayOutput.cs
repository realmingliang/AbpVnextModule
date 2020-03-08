using System;
using System.Collections.Generic;

namespace Tudou.Abp.AuditLogging
{
    public class GetAverageExecutionDurationPerDayOutput
    {
        public Dictionary<DateTime, double> Data { get; set; }
    }
}