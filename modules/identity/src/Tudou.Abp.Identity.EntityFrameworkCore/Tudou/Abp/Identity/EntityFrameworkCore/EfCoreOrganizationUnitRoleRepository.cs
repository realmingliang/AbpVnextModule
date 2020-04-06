using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
