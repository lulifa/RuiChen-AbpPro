using Volo.Abp.Collections;

namespace RuiChen.AbpPro.SettingManagement;
public class SettingManagementMergeOptions
{
    public ITypeList<IUserSettingAppService> UserSettingProviders { get; }
    public ITypeList<IReadonlySettingAppService> GlobalSettingProviders { get; }
    public SettingManagementMergeOptions()
    {
        UserSettingProviders = new TypeList<IUserSettingAppService>();
        GlobalSettingProviders = new TypeList<IReadonlySettingAppService>();
    }
}
