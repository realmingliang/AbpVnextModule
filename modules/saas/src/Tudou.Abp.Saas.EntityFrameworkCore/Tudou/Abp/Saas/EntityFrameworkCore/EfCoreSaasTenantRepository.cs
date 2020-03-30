using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    public class EfCoreSaasTenantRepository : EfCoreRepository<ISaasDbContext, SaasTenant, Guid>, ISaasTenantRepository
    {
        public EfCoreSaasTenantRepository(IDbContextProvider<ISaasDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<SaasTenant> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(t => t.Name == name, GetCancellationToken(cancellationToken));
        }

        public virtual SaasTenant FindByName(string name, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Name == name);
        }

        public virtual SaasTenant FindById(Guid id, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Id == id);
        }

        public virtual async Task<List<SaasTenant>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                )
                .OrderBy(sorting ?? nameof(SaasTenant.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await this
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                ).CountAsync(cancellationToken: cancellationToken);
        }

        public override IQueryable<SaasTenant> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}
