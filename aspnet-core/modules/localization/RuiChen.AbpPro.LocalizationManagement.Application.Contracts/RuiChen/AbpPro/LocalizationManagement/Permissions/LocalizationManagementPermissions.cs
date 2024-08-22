namespace RuiChen.AbpPro.LocalizationManagement
{
    public static class LocalizationManagementPermissions
    {

        public const string GroupName = "LocalizationManagement";

        public class Resource
        {
            public const string Default = GroupName + ".Resource";

            public const string Create = Default + ".Create";

            public const string Update = Default + ".Update";

            public const string Delete = Default + ".Delete";
        }

        public class Language
        {
            public const string Default = GroupName + ".Language";

            public const string Create = Default + ".Create";

            public const string Update = Default + ".Update";

            public const string Delete = Default + ".Delete";
        }

        public class LanguageText
        {
            public const string Default = GroupName + ".LanguageText";

            public const string Create = Default + ".Create";

            public const string Update = Default + ".Update";

            public const string Delete = Default + ".Delete";
        }

    }
}
