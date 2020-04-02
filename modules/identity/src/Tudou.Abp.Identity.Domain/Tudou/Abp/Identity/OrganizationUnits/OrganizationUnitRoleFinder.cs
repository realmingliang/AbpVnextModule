using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class OrganizationUnitRoleFinder : IOrganizationUnitRoleFinder, ITransientDependency
    {
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }
        public OrganizationUnitRoleFinder(IOrganizationUnitRepository organizationUnitRepository)
        {
            OrganizationUnitRepository = organizationUnitRepository;
        }
        public async Task<string[]> GetRolesAsync(Guid ouId)
        {
            return (await OrganizationUnitRepository.GetRoleNamesAsync(ouId)).ToArray();
        }
    }
}
