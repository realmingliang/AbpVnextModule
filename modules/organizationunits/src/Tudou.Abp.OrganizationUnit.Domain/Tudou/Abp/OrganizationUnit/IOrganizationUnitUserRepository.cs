using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitUserRepository : IBasicRepository<OrganizationUnitUser, Guid>
    {
        Task<List<OrganizationUnitUser>> FindOrganizationUnitUsersAsync(
         Guid? organizationUnitId,
         int maxResultCount = int.MaxValue,
         int skipCount = 0,
         CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(
           Guid organizationUnitId,
           CancellationToken cancellationToken = default);
        Task<OrganizationUnitUser> FindByOrganizationUnitIdAndUserIdAsync(
           Guid organizationUnitId,
           Guid userId,
           CancellationToken cancellationToken = default);
    }
}
