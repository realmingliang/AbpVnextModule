using Volo.Abp.Reflection;

namespace Tudou.Abp.OrganizationUnit.Authorization
{
    public class OrganizationUnitPermissions
    {
        public const string GroupName = "AbpOrganizationUnit";

        public static class OrganizationUnit
        {
            public const string Default = GroupName + ".OrganizationUnit";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Move = Default + ".Move";
        }
        public static class OrganizationUnitRole
        {
            public const string Default = GroupName + ".OrganizationUnitRole";
            public const string RemoveRoleFromOrganizationUnit = Default + ".RemoveRoleFromOrganizationUnit";
            public const string AddRolesToOrganizationUnit = Default + ".AddRolesToOrganizationUnit";
            public const string FindRoles = Default + ".FindRoles";
        }
        public static class OrganizationUnitUser
        {
            public const string Default = GroupName + ".OrganizationUnitUser";
            public const string RemoveUserFromOrganizationUnit = Default + ".RemoveUserFromOrganizationUnit";
            public const string AddUsersToOrganizationUnit = Default + ".AddUsersToOrganizationUnit";
            public const string FindUsers = Default + ".FindUsers";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OrganizationUnitPermissions));
        }
    }
}