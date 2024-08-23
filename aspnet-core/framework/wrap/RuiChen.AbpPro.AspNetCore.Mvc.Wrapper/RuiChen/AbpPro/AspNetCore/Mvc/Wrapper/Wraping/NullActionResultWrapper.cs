using Microsoft.AspNetCore.Mvc.Filters;

namespace RuiChen.AbpPro.AspNetCore.Mvc.Wrapper
{
    public class NullActionResultWrapper : IActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {

        }
    }
}
