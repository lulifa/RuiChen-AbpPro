﻿using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.Platform
{
    [EventName("platform.menus.role_menu")]
    public class RoleMenuEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid MenuId { get; set; }
        public string RoleName { get; set; }
    }
}
