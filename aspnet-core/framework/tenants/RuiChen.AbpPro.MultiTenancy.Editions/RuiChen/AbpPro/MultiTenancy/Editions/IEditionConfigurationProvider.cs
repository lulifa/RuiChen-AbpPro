using System;
using System.Threading.Tasks;

namespace RuiChen.AbpPro.MultiTenancy
{
    public interface IEditionConfigurationProvider
    {
        Task<EditionConfiguration> GetAsync(Guid? tenantId = null);
    }
}
