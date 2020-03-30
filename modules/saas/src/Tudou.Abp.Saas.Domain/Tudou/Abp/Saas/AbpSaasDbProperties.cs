using Volo.Abp.Data;

namespace Tudou.Abp.Saas
{
    public static class AbpSaasDbProperties
    {
        public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "AbpSaas";
    }
}
