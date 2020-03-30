using Volo.Abp.Reflection;

namespace Tudou.Abp.Saas
{
    public static class SaasPermissions
    {
        public const string GroupName = "AbpSaas";

        public static class Tenants
        {
            public const string Default = GroupName + ".Tenants";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManageFeatures = Default + ".ManageFeatures";
            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }
        public static class Editions
        {
            public const string Default = GroupName + ".Editions";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManageFeatures = Default + ".ManageFeatures";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SaasPermissions));
        }
    }
}