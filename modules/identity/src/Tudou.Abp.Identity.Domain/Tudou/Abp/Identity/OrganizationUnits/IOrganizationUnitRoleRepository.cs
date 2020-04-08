using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Identity.OrganizationUnits
{
   public interface IOrganizationUnitRoleRepository : IRepository<OrganizationUnitRole>
    {
        Task<List<OrganizationUnitRole>> GetOuRoleListAsync(Guid id, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
        Task<long> GetOuRoleCountAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
