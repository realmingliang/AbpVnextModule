using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Saas
{
    public interface ISaasTenantRepository : IBasicRepository<SaasTenant, Guid>
    {
        Task<SaasTenant> FindByNameAsync(
            string name, 
            bool includeDetails = true, 
            CancellationToken cancellationToken = default);

        SaasTenant FindByName(
            string name,
            bool includeDetails = true
        );

        SaasTenant FindById(
            Guid id,
            bool includeDetails = true
        );

        Task<List<SaasTenant>> GetListAsync(
            string sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            string filter = null, 
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null, 
            CancellationToken cancellationToken = default);
    }
}