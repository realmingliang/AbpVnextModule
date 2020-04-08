using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Identity
{
   public interface IdentityUserOrganizationUnitRepository : IRepository<IdentityUserOrganizationUnit>
    {
        Task<List<IdentityUserOrganizationUnit>> GetOuUserListAsync(Guid id, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
        Task<long> GetOuUserCountAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
