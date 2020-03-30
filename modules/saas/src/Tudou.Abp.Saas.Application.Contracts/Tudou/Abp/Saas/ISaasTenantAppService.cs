using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.Saas
{
    public interface ISaasTenantAppService : ICrudAppService<SaasTenantDto, Guid, GetSaasTenantsInput, SaasTenantCreateDto, SaasTenantUpdateDto>
    {
        Task<string> GetDefaultConnectionStringAsync(Guid id);

        Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString);

        Task DeleteDefaultConnectionStringAsync(Guid id);

    }
}
