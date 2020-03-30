using System;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.Saas
{
    public interface ISaasEditionAppService : ICrudAppService<SaasEditionDto, Guid, GetSaasEditionsInput, SaasEditionCreateDto, SaasEditionUpdateDto>
    {

    }
}
