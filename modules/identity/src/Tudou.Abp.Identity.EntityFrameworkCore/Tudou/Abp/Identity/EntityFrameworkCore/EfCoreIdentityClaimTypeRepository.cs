using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
    public class EfCoreIdentityClaimTypeRepository : EfCoreRepository<IIdentityDbContext, IdentityClaimType, Guid>, IIdentityClaimTypeRepository
    {
        public EfCoreIdentityClaimTypeRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public virtual async Task<bool> AnyAsync(string name, Guid? ignoredId = null)
        {
            return await DbSet
                       .WhereIf(ignoredId != null, ct => ct.Id != ignoredId)
                       .CountAsync(ct => ct.Name == name) > 0;
        }

        public async Task<long> GetCountAsync(string filter, CancellationToken cancellationToken = default)
        {
            return await this.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                u =>
                    u.Name.Contains(filter)
            )
            .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityClaimType>> GetListAsync(string sorting=null, int maxResultCount=int.MaxValue, int skipCount=0, string filter=null)
        {
            var identityClaimTypes = await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                )
                .OrderBy(sorting ?? nameof(IdentityClaimType.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();

            return identityClaimTypes;
        }
    }
}
