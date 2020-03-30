using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tudou.Abp.Saas
{
    public class SaasEdition : FullAuditedAggregateRoot<Guid>
    {
        public virtual string DisplayName { get; protected set; }
        protected SaasEdition()
        {
            ExtraProperties = new Dictionary<string, object>();
        }
        protected internal SaasEdition(Guid id, [NotNull] string diaplayName)
        {
            Id = id;
            SetDisplayName(diaplayName);
            ExtraProperties = new Dictionary<string, object>();
        }
        protected internal virtual void SetDisplayName([NotNull] string diaplayName)
        {
            DisplayName = Check.NotNullOrWhiteSpace(diaplayName, nameof(diaplayName), SaasEditionConsts.MaxNameLength);
        }
    }
}
