using RuiChen.AbpPro.Authorization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace RuiChen.AbpPro.PermissionManagement
{
    public class OrganizationUnitDeletedEventHandler : IDistributedEventHandler<EntityDeletedEto<OrganizationUnitEto>>, ITransientDependency
    {
        private readonly IPermissionManager permissionManager;

        public OrganizationUnitDeletedEventHandler(IPermissionManager permissionManager)
        {
            this.permissionManager = permissionManager;
        }
        public async Task HandleEventAsync(EntityDeletedEto<OrganizationUnitEto> eventData)
        {
            await permissionManager.DeleteAsync(OrganizationUnitPermissionValueProvider.ProviderName, eventData.Entity.Code);
            await permissionManager.DeleteAsync(OrganizationUnitPermissionValueProvider.ProviderName, eventData.Entity.Id.ToString());
        }
    }
}
