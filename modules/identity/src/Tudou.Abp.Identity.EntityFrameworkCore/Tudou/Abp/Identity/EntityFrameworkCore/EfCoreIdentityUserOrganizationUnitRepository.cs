using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
   public class EfCoreIdentityUserOrganizationUnitRepository: EfCoreRepository<IIdentityDbContext, IdentityUserOrganizationUnit>,IdentityUserOrganizationUnitRepository
    {
        public EfCoreIdentityUserOrganizationUnitRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }
    }
}
