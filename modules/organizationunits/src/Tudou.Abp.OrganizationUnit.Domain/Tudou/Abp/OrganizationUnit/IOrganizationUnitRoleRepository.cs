using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitRoleRepository : IBasicRepository<OrganizationUnitRole, Guid>
    {
        Task<List<OrganizationUnitRole>> FindOrganizationUnitRolesAsync(
           Guid organizationUnitId,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(
          Guid organizationUnitId,
          CancellationToken cancellationToken = default);
        Task<OrganizationUnitRole> FindByOrganizationUnitIdAndRoleIdAsync(
          Guid organizationUnitId,
          Guid roleId,
          CancellationToken cancellationToken = default);
    }
}
