using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.Platform
{
    [EventName("platform.menus.user_menu")]
    public class UserMenuEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid MenuId { get; set; }
        public Guid UserId { get; set; }
    }
}
