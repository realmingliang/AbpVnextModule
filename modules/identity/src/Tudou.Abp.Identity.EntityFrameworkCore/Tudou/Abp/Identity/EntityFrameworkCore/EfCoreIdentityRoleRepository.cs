using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
    public class EfCoreIdentityRoleRepository : EfCoreRepository<IIdentityDbContext, IdentityRole, Guid>, IIdentityRoleRepository
    {
        public EfCoreIdentityRoleRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<IdentityRole> FindByNormalizedNameAsync(
            string normalizedRoleName, 
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityRole>> GetListAsync(
            string sorting = null, 
            int maxResultCount = int.MaxValue, 
            int skipCount = 0, 
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .OrderBy(sorting ?? nameof(IdentityRole.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<IdentityRole>> GetDefaultOnesAsync(
            bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbSet.IncludeDetails(includeDetails).Where(r => r.IsDefault).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<IdentityRole> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }

        public async Task<List<IdentityRole>> GetListByOrganizationUnitIdAsync(
            Guid organizationUnitId,
            int maxResultCount = int.MaxValue,
            int skipCount = 0, 
            string filter =null,CancellationToken cancellationToken = default)
        {
            var query = GetRolesQuerableByOrganizationUnitIdAsync(organizationUnitId, filter);

            return await query
                  .PageBy(skipCount, maxResultCount)
                  .ToListAsync(GetCancellationToken(cancellationToken));
        }
        private IQueryable<IdentityRole> GetRolesQuerableByOrganizationUnitIdAsync(Guid organizationUnitId,
           string filter = null)
        {
            var roleIdsInOrganizationUnit = DbContext.Set<OrganizationUnitRole>()
                .Where(uou => uou.OrganizationUnitId == organizationUnitId)
                .Select(uou => uou.RoleId);
           return DbSet
               .Where(u => !roleIdsInOrganizationUnit.Contains(u.Id))
               .WhereIf(
               !filter.IsNullOrWhiteSpace(),
               u =>
                   u.Name.Contains(filter)
              );

        }
        public async Task<long> GetCountByOrganizationUnitIdAsync(Guid organizationUnitId, string filter = null, CancellationToken cancellationToken = default)
        {
            var query = GetRolesQuerableByOrganizationUnitIdAsync(organizationUnitId, filter);
            return await query
                  .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}