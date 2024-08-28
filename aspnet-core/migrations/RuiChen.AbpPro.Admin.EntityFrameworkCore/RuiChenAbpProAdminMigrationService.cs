using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RuiChen.AbpPro.Saas;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Migrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace RuiChen.AbpPro.Admin.EntityFrameworkCore
{
    public class RuiChenAbpProAdminMigrationService : EfCoreRuntimeDatabaseMigratorBase<RuiChenAbpProAdminMigrationDbContext>, ITransientDependency
    {
        protected IDataSeeder DataSeeder { get; }
        protected ITenantRepository TenantRepository { get; }
        public RuiChenAbpProAdminMigrationService(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            IAbpDistributedLock abpDistributedLock,
            IDistributedEventBus distributedEventBus,
            ILoggerFactory loggerFactory,
            IDataSeeder dataSeeder,
            ITenantRepository tenantRepository)
            : base(
                ConnectionStringNameAttribute.GetConnStringName<RuiChenAbpProAdminMigrationDbContext>(),
                unitOfWorkManager, serviceProvider, currentTenant, abpDistributedLock, distributedEventBus, loggerFactory)
        {
            DataSeeder = dataSeeder;
            TenantRepository = tenantRepository;
        }
        protected async override Task LockAndApplyDatabaseMigrationsAsync()
        {
            await base.LockAndApplyDatabaseMigrationsAsync();

            Logger.LogInformation("Started database migrations...");

            var tenants = await TenantRepository.GetListAsync();
            foreach (var tenant in tenants.Where(x => x.IsActive))
            {
                Logger.LogInformation($"Trying to acquire the distributed lock for database migration: {DatabaseName} with tenant: {tenant.Name}.");

                var schemaMigrated = false;

                await using (var handle = await DistributedLock.TryAcquireAsync("DatabaseMigration_" + DatabaseName + "_Tenant" + tenant.Id.ToString()))
                {
                    if (handle is null)
                    {
                        Logger.LogInformation($"Distributed lock could not be acquired for database migration: {DatabaseName} with tenant: {tenant.Name}. Operation cancelled.");
                        return;
                    }

                    Logger.LogInformation($"Distributed lock is acquired for database migration: {DatabaseName} with tenant: {tenant.Name}...");

                    using (CurrentTenant.Change(tenant.Id))
                    {
                        // Create database tables if needed
                        using var uow = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: false);
                        var dbContext = await ServiceProvider
                            .GetRequiredService<IDbContextProvider<RuiChenAbpProAdminMigrationDbContext>>()
                            .GetDbContextAsync();

                        var pendingMigrations = await dbContext
                            .Database
                            .GetPendingMigrationsAsync();

                        if (pendingMigrations.Any())
                        {
                            await dbContext.Database.MigrateAsync();
                            schemaMigrated = true;
                        }

                        await uow.CompleteAsync();

                        await SeedAsync();

                        if (schemaMigrated || AlwaysSeedTenantDatabases)
                        {
                            await DistributedEventBus.PublishAsync(
                                new AppliedDatabaseMigrationsEto
                                {
                                    DatabaseName = DatabaseName,
                                    TenantId = tenant.Id
                                }
                            );
                        }
                    }
                }

                Logger.LogInformation($"Distributed lock has been released for database migration: {DatabaseName} with tenant: {tenant.Name}...");
            }

            Logger.LogInformation("Successfully completed all database migrations.");
            Logger.LogInformation("You can safely end this process...");
        }

        protected async override Task SeedAsync()
        {
            await DataSeeder.SeedAsync(CurrentTenant.Id);
        }
    }
}
