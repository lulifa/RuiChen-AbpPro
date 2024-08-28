using Microsoft.Extensions.Logging;
using RuiChen.AbpPro.Saas;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EntityFrameworkCore.Migrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace RuiChen.AbpPro.Admin.EntityFrameworkCore
{
    public class RuiChenAbpProAdminMigrationEventHandler : EfCoreDatabaseMigrationEventHandlerBase<RuiChenAbpProAdminMigrationDbContext>, IDistributedEventHandler<EntityDeletedEto<TenantEto>>
    {

        public RuiChenAbpProAdminMigrationEventHandler(
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        IDistributedEventBus distributedEventBus,
        ILoggerFactory loggerFactory)
        : base(
            ConnectionStringNameAttribute.GetConnStringName<RuiChenAbpProAdminMigrationDbContext>(),
            currentTenant, unitOfWorkManager, tenantStore, distributedEventBus, loggerFactory)
        {
        }

        public Task HandleEventAsync(EntityDeletedEto<TenantEto> eventData)
        {
            throw new NotImplementedException();
        }
    }
}
