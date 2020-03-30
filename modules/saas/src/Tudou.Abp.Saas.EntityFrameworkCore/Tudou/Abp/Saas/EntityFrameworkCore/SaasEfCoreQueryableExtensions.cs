using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    public static class SaasEfCoreQueryableExtensions
    {
        public static IQueryable<SaasTenant> IncludeDetails(this IQueryable<SaasTenant> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.ConnectionStrings)
                .Include(x => x.SaasEdition);
        }
    }
}