namespace RuiChen.AbpPro.MultiTenancy
{
    public interface IEditionStore
    {
        Task<EditionInfo> FindByTenantAsync(Guid tenantId);
    }
}
