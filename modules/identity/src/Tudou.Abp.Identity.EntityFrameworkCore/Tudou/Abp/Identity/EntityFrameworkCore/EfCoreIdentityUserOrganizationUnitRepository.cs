using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
   public class EfCoreIdentityUserOrganizationUnitRepository: EfCoreRepository<IIdentityDbContext, IdentityUserOrganizationUnit>, IdentityUserOrganizationUnitRepository
    {
        public EfCoreIdentityUserOrganizationUnitRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }

        public async Task<long> GetOuUserCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetUserQuerable(id)
            .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<IdentityUserOrganizationUnit>> GetOuUserListAsync(Guid id,  int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = GetUserQuerable(id);
            return await query.PageBy(skipCount, maxResultCount)
                  .ToListAsync(GetCancellationToken(cancellationToken));
        }

        private IQueryable<IdentityUserOrganizationUnit> GetUserQuerable(Guid id) {

            var query = from ouUser in DbSet
                        join ou in DbContext.OrganizationUnits on ouUser.OrganizationUnitId equals ou.Id
                        where ouUser.OrganizationUnitId == id
                        select ouUser;
            return query;
        }
    }
}
