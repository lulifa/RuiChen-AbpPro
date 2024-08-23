using Microsoft.AspNetCore.Mvc.Filters;

namespace RuiChen.AbpPro.AspNetCore.Mvc.Wrapper
{
    public interface IActionResultWrapper
    {
        void Wrap(FilterContext context);
    }
}
