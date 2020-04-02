using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.Identity
{
    public class IdentityClaimTypeDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool IsStatic { get;  set; }

        public string Regex { get; set; }

        public string RegexDescription { get; set; }

        public string Description { get; set; }
        public IdentityClaimValueType ValueType { get;  set; }

        public string ValueTypeAsString { get; set; }
    }
}