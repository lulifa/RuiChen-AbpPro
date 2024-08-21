namespace RuiChen.AbpPro.SettingManagement
{
    public class UpdateSettingsDto
    {
        public UpdateSettingDto[] Settings { get; set; }
        public UpdateSettingsDto()
        {
            Settings = Array.Empty<UpdateSettingDto>();
        }
    }
}
