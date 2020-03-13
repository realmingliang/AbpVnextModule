using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    public class EfCoreOrganizationUnitRepository : EfCoreRepository<IOrganizationUnitDbContext, OrganizationUnit, Guid>, IOrganizationUnitRepository
    {
        public EfCoreOrganizationUnitRepository(IDbContextProvider<IOrganizationUnitDbContext> dbContextProvider)
             : base(dbContextProvider)
        {
        }
        public async Task<List<OrganizationUnit>> FindChildrenAsync(Guid? parentId, bool recursive = false, CancellationToken cancellationToken = default)
        {
            if (!recursive)
            {
                return await DbSet.Where(t => t.ParentId == parentId).ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            if (!parentId.HasValue)
            {
                return await DbSet.ToListAsync(GetCancellationToken(cancellationToken));
            }
            var code = (await base.FindAsync(parentId.Value)).Code;
            var query = DbSet.Where(ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value);
            return await query.ToListAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
        }
    }
}
