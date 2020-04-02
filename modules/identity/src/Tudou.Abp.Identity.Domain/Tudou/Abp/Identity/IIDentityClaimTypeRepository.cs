using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tudou.Abp.Identity
{
    public interface IIdentityClaimTypeRepository : IBasicRepository<IdentityClaimType, Guid>
    {
        /// <summary>
        /// Checks if there is a <see cref="IdentityClaimType"/> entity with given name.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <param name="ignoredId">
        /// An Id value to ignore on checking.
        /// If there is an entity with given <paramref name="ignoredId"/> it's ignored.
        /// </param>
        Task<bool> AnyAsync(string name, Guid? ignoredId = null);

        Task<List<IdentityClaimType>> GetListAsync(string sorting=null, int maxResultCount=int.MaxValue, int skipCount=0, string filter=null);

        Task<long> GetCountAsync(string filter=null, CancellationToken cancellationToken=default);
    }
}
