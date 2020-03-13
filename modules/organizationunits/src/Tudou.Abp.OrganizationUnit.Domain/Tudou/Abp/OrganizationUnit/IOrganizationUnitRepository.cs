using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitRepository : IBasicRepository<OrganizationUnit, Guid>
    {
        Task<List<OrganizationUnit>> FindChildrenAsync(
            Guid? parentId,
            bool recursive = false,
            CancellationToken cancellationToken = default);

    }
}
