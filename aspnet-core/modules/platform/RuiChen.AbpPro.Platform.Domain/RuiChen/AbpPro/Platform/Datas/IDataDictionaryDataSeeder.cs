namespace RuiChen.AbpPro.Platform
{
    public interface IDataDictionaryDataSeeder
    {
        Task<Data> SeedAsync(
            string name,
            string code,
            string displayName,
            string description = "",
            Guid? parentId = null,
            Guid? tenantId = null,
            bool isStatic = false,
            CancellationToken cancellationToken = default);
    }
}
