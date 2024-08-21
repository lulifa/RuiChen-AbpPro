using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.Saas
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(AbpProSaasDbProperties.ConnectionStringName)]
    public interface ISaasDbContext : IEfCoreDbContext
    {
        DbSet<Edition> Editions { get; }
        DbSet<Tenant> Tenants { get; }
        DbSet<TenantConnectionString> TenantConnectionStrings { get; }
    }
}
