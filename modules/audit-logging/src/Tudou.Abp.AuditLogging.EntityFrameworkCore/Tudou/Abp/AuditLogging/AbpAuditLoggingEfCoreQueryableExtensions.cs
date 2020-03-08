using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tudou.Abp.AuditLogging
{
    public static class AbpAuditLoggingEfCoreQueryableExtensions
    {
        public static IQueryable<AuditLog> IncludeDetails(
            this IQueryable<AuditLog> queryable,
            bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Actions)
                .Include(x => x.EntityChanges).ThenInclude(ec=>ec.PropertyChanges);
        }
    }
}
