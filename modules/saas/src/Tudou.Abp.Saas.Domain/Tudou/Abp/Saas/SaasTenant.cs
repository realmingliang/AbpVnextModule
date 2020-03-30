using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tudou.Abp.Saas
{
    public class SaasTenant : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }

        public virtual Guid? EditionId { get;  set; }

        public virtual SaasEdition SaasEdition { get;  set; }

        public virtual List<SaasTenantConnectionString> ConnectionStrings { get; protected set; }

        protected SaasTenant()
        {
            ExtraProperties = new Dictionary<string, object>();
        }

        protected internal SaasTenant(Guid id, [NotNull] string name,Guid? editionId)
        {
            Id = id;
            SetName(name);
            SetEdition(editionId);
            ConnectionStrings = new List<SaasTenantConnectionString>();
            ExtraProperties = new Dictionary<string, object>();
        }

        [CanBeNull]
        public virtual string FindDefaultConnectionString()
        {
            return FindConnectionString(Volo.Abp.Data.ConnectionStrings.DefaultConnectionStringName);
        }

        [CanBeNull]
        public virtual string FindConnectionString(string name)
        {
            return ConnectionStrings.FirstOrDefault(c => c.Name == name)?.Value;
        }

        public virtual void SetDefaultConnectionString(string connectionString)
        {
            SetConnectionString(Volo.Abp.Data.ConnectionStrings.DefaultConnectionStringName, connectionString);
        }

        public virtual void SetConnectionString(string name, string connectionString)
        {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

            if (tenantConnectionString != null)
            {
                tenantConnectionString.SetValue(connectionString);
            }
            else
            {
                ConnectionStrings.Add(new SaasTenantConnectionString(Id, name, connectionString));
            }
        }

        public virtual void RemoveDefaultConnectionString()
        {
            RemoveConnectionString(Volo.Abp.Data.ConnectionStrings.DefaultConnectionStringName);
        }

        public virtual void RemoveConnectionString(string name)
        {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

            if (tenantConnectionString != null)
            {
                ConnectionStrings.Remove(tenantConnectionString);
            }
        }

        protected internal virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name),SaasTenantConsts.MaxNameLength);
        }

        public  virtual void SetEdition([CanBeNull] Guid? editionId)
        {
            EditionId = editionId;
        }
    }
}