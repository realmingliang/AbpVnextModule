using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.Identity.MongoDB
{
    [ConnectionStringName(AbpIdentityDbProperties.ConnectionStringName)]
    public interface IAbpIdentityMongoDbContext : IAbpMongoDbContext
    {
        IMongoCollection<IdentityUser> Users { get; }

        IMongoCollection<IdentityRole> Roles { get; }

        IMongoCollection<IdentityClaimType> ClaimTypes { get; }
    }
}