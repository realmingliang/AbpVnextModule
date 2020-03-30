using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Saas
{
    public interface ISaasEditionRepository : IBasicRepository<SaasEdition, Guid>
    {
        Task<SaasEdition> FindByNameAsync(
              string name,
              CancellationToken cancellationToken = default);
        Task<List<SaasEdition>> GetListAsync(
                   string sorting = null,
                   int maxResultCount = int.MaxValue,
                   int skipCount = 0,
                   string filter = null,
                   CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(
         string filter = null,
         CancellationToken cancellationToken = default);
    }
}
