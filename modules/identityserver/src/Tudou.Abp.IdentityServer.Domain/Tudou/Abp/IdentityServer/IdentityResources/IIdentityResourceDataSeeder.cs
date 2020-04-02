using System.Threading.Tasks;

namespace Tudou.Abp.IdentityServer.IdentityResources
{
    public interface IIdentityResourceDataSeeder
    {
        Task CreateStandardResourcesAsync();
    }
}