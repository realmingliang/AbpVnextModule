using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
  public  class EfCoreOrganizationUnitRoleRepository:EfCoreRepository<IIdentityDbContext, OrganizationUnitRole>,IOrganizationUnitRoleRepository
    {
        public EfCoreOrganizationUnitRoleRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }

        public async Task<long> GetOuRoleCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetUserQuerable(id)
            .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<OrganizationUnitRole>> GetOuRoleListAsync(Guid id,  int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = GetUserQuerable(id);
            return await query.PageBy(skipCount, maxResultCount)
                  .ToListAsync(GetCancellationToken(cancellationToken));
        }
        private IQueryable<OrganizationUnitRole> GetUserQuerable(Guid id)
        {

            var query = from ouRole in DbSet
                        join ou in DbContext.OrganizationUnits on ouRole.OrganizationUnitId equals ou.Id
                        where ouRole.OrganizationUnitId == id
                        select ouRole;
            return query;
        }
    }
}
