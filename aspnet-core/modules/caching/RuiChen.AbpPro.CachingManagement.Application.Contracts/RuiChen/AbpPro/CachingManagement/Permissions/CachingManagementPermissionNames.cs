using Volo.Abp.Reflection;

namespace RuiChen.AbpPro.CachingManagement
{
    public static class CachingManagementPermissionNames
    {

        public const string GroupName = "AbpCachingManagement";

        public static class Cache
        {
            public const string Default = GroupName + ".Cache";
            public const string Refresh = Default + ".Refresh";
            public const string Delete = Default + ".Delete";
            public const string ManageValue = Default + ".ManageValue";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CachingManagementPermissionNames));
        }

    }
}
