using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Tudou.Abp.Saas
{
    public class SaasTenantConnectionString : Entity
    {
        public virtual Guid TenantId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Value { get; protected set; }

        protected SaasTenantConnectionString()
        {
            
        }

        public SaasTenantConnectionString(Guid tenantId, [NotNull] string name, [NotNull] string value)
        {
            TenantId = tenantId;
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), SaasTenantConnectionStringConsts.MaxNameLength);
            SetValue(value);
        }

        public virtual void SetValue([NotNull] string value)
        {
            Value = Check.NotNullOrWhiteSpace(value, nameof(value), SaasTenantConnectionStringConsts.MaxValueLength);
        }

        public override object[] GetKeys()
        {
            return new object[] { TenantId, Name };
        }
    }
}