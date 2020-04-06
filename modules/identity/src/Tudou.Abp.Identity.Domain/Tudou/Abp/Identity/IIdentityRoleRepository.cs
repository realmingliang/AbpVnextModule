using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Identity
{
    public interface IIdentityRoleRepository : IBasicRepository<IdentityRole, Guid>
    {
        Task<IdentityRole> FindByNormalizedNameAsync(
            string normalizedRoleName,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );
        Task<List<IdentityRole>> GetListByOrganizationUnitIdAsync(
            Guid organizationUnitId,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter=null,
            CancellationToken cancellationToken = default
        );
        Task<long> GetCountByOrganizationUnitIdAsync(
          Guid organizationUnitId,
          string filter = null,
          CancellationToken cancellationToken = default
        );
        Task<List<IdentityRole>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityRole>> GetDefaultOnesAsync(
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );
    }
}