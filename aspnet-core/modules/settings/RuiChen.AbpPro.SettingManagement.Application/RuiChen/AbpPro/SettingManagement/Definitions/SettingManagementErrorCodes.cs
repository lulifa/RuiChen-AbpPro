using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiChen.AbpPro.SettingManagement
{
    public static class SettingManagementErrorCodes
    {
        public const string Namespace = "SettingManagement";

        public static class Definition
        {
            public const string Prefix = Namespace + ":001";

            public const string DuplicateName = Prefix + "001";

            public const string StaticSettingNotAllowedChanged = Prefix + "010";
        }
    }
}
