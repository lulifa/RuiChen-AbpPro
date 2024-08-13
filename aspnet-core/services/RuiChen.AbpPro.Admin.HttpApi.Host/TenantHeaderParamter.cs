using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.MultiTenancy;

namespace RuiChen.AbpPro.Admin.HttpApi.Host
{
    public class TenantHeaderParamter : IOperationFilter
    {
        private readonly IOptions<AbpMultiTenancyOptions> multiTenancyOptions;
        private readonly IOptions<AbpAspNetCoreMultiTenancyOptions> aspNetCoreMultiTenancyOptions;

        public TenantHeaderParamter(IOptions<AbpMultiTenancyOptions> multiTenancyOptions,
        IOptions<AbpAspNetCoreMultiTenancyOptions> aspNetCoreMultiTenancyOptions)
        {
            this.multiTenancyOptions = multiTenancyOptions;
            this.aspNetCoreMultiTenancyOptions = aspNetCoreMultiTenancyOptions;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (multiTenancyOptions.IsEnabled)
            {
                operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = aspNetCoreMultiTenancyOptions.TenantKey,
                    In = ParameterLocation.Header,
                    Description = "Tenant Id in http header",
                    Required = false
                });
            }
        }
    }
}
