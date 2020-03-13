using Volo.Abp.Data;

namespace Tudou.Abp.OrganizationUnit
{
    public static class OrganizationUnitDbProperties
    {
        public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "AbpOrganizationUnit";
    }
}
