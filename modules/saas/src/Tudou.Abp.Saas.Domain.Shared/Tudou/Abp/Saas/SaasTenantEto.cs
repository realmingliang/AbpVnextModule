using System;

namespace Tudou.Abp.Saas
{
    [Serializable]
    public class SaasTenantEto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
