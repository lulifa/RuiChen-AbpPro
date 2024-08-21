using System.ComponentModel.DataAnnotations;

namespace RuiChen.AbpPro.SettingManagement
{
    public interface ISettingTestAppService
    {
        Task SendTestEmailAsync(SendTestEmailInput input);
    }

    public class SendTestEmailInput
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
