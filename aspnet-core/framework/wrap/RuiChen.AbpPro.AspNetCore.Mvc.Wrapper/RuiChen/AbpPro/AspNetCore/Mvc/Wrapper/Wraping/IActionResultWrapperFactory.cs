using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.AspNetCore.Mvc.Wrapper
{
    public interface IActionResultWrapperFactory : ITransientDependency
    {
        IActionResultWrapper CreateFor(FilterContext context);
    }
}
