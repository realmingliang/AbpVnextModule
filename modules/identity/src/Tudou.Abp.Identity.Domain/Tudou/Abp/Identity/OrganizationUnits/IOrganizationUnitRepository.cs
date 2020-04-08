using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public interface IOrganizationUnitRepository : IBasicRepository<OrganizationUnit, Guid>
    {
        Task<List<OrganizationUnit>> GetChildrenAsync(Guid? parentId, CancellationToken cancellationToken = default);

        Task<List<OrganizationUnit>> GetAllChildrenWithParentCodeAsync(string code, Guid? parentId, CancellationToken cancellationToken = default);

        Task<List<OrganizationUnit>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        Task<Dictionary<Guid, int>> GetAllMemberCountAsync(CancellationToken cancellationToken = default);

        Task<Dictionary<Guid, int>> GetAllRoleCountAsync(CancellationToken cancellationToken = default);

        Task<int> GetMemberCountAsync(Guid id, CancellationToken cancellationToken = default);

        Task<int> GetRoleCountAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<string>> GetRoleNamesAsync(
           Guid id,
           CancellationToken cancellationToken = default
        );
        Task<List<string>> GetCurrentUserRoleNamesByOrganizationUnitAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
