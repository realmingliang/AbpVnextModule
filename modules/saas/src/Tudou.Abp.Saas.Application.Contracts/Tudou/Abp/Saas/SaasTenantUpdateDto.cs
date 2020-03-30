using System;

namespace Tudou.Abp.Saas
{
    public class SaasTenantUpdateDto : SaasTenantCreateOrUpdateDtoBase
    {
        public Guid? EditionId { get; set; }
    }
}