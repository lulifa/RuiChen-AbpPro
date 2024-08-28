using Volo.Abp.EventBus;

namespace RuiChen.AbpPro.Platform
{
    [EventName("platform.menus.menu")]
    public class MenuEto : RouteEto
    {
        public string Framework { get; set; }
    }
}
