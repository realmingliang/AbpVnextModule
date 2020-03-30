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
    public class EfCoreSaasEditionRepository : EfCoreRepository<ISaasDbContext, SaasEdition, Guid>, ISaasEditionRepository
    {
        public EfCoreSaasEditionRepository(IDbContextProvider<ISaasDbContext> dbContextProvider)
               : base(dbContextProvider)
        {

        }
        public async Task<SaasEdition> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await DbSet
                 .FirstOrDefaultAsync(t => t.DisplayName == name, GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await this
               .WhereIf(
                   !filter.IsNullOrWhiteSpace(),
                   u =>
                       u.DisplayName.Contains(filter)
               ).CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<SaasEdition>> GetListAsync(string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbSet
               .WhereIf(
                   !filter.IsNullOrWhiteSpace(),
                   u =>
                       u.DisplayName.Contains(filter)
               )
               .OrderBy(sorting ?? nameof(SaasEdition.DisplayName))
               .PageBy(skipCount, maxResultCount)
               .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
