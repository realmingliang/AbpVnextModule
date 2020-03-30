using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Saas
{
    public class SaasTenantDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string EditionName { get; set; }

        public Guid EditionId { get; set; }

    }
}