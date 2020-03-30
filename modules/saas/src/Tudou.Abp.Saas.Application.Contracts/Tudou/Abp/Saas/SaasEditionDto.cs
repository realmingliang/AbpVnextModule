using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Saas
{
    public class SaasEditionDto : EntityDto<Guid>
    {
        public string DisplayName { get; set; }
    }
}