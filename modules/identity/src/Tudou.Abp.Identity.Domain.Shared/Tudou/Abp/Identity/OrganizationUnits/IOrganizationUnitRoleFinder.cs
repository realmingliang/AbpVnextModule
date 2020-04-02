using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tudou.Abp.Identity.OrganizationUnits
{
   public interface IOrganizationUnitRoleFinder
    {
        Task<string[]> GetRolesAsync(Guid ouId);
    }
}
