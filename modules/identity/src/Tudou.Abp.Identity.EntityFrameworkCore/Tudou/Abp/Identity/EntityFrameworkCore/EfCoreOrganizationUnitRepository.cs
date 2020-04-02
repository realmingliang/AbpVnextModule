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
    public class EfCoreOrganizationUnitRepository : EfCoreRepository<IIdentityDbContext, OrganizationUnit, Guid>, IOrganizationUnitRepository
    {
        public EfCoreOrganizationUnitRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<List<OrganizationUnit>> GetChildrenAsync(Guid? parentId, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(x => x.ParentId == parentId)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<OrganizationUnit>> GetAllChildrenWithParentCodeAsync(string code, Guid? parentId, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        public async Task<List<OrganizationUnit>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(t => ids.Contains(t.Id)).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<List<OrganizationUnit>> GetListAsync(bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<OrganizationUnit> GetOrganizationUnitAsync(string displayName, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .FirstOrDefaultAsync(
                    ou => ou.DisplayName == displayName,
                    GetCancellationToken(cancellationToken)
                );
        }

        public async Task<Dictionary<Guid, int>> GetAllMemberCountAsync(CancellationToken cancellationToken = default)
        {
            return  await DbContext.Set<IdentityUserOrganizationUnit>().GroupBy(x => x.OrganizationUnitId)
                .Select(groupedUsers => new
                {
                    organizationUnitId = groupedUsers.Key,
                    count = groupedUsers.Count()
                }).ToDictionaryAsync(x => x.organizationUnitId, y => y.count, GetCancellationToken(cancellationToken));

        }

        public async Task<Dictionary<Guid, int>> GetAllRoleCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<OrganizationUnitRole>().GroupBy(x => x.OrganizationUnitId)
               .Select(groupedRoles => new
               {
                   organizationUnitId = groupedRoles.Key,
                   count = groupedRoles.Count()
               }).ToDictionaryAsync(x => x.organizationUnitId, y => y.count, GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetMemberCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUserOrganizationUnit>().CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetRoleCountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<OrganizationUnitRole>().CountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<string>> GetRoleNamesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = from ouRole in DbContext.Set<OrganizationUnitRole>()
                        join role in DbContext.Roles on ouRole.RoleId equals role.Id
                        where ouRole.OrganizationUnitId == id
                        select role.Name;

            return await query.ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
