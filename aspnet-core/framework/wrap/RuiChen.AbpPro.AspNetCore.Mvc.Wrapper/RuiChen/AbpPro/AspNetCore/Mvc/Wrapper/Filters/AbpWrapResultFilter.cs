﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using RuiChen.AbpPro.AspNetCore.Wrapper;
using RuiChen.AbpPro.Wrapper;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace RuiChen.AbpPro.AspNetCore.Mvc.Wrapper
{
    public class AbpWrapResultFilter : IAsyncResultFilter, ITransientDependency
    {

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (ShouldWrapResult(context))
            {
                await HandleAndWrapResult(context);
            }

            await next();
        }

        protected virtual bool ShouldWrapResult(ResultExecutingContext context)
        {
            var wrapResultChecker = context.GetRequiredService<IWrapResultChecker>();

            return wrapResultChecker.WrapOnExecution(context);
        }

        protected virtual Task HandleAndWrapResult(ResultExecutingContext context)
        {
            var options = context.GetRequiredService<IOptions<AbpWrapperOptions>>().Value;
            var httpResponseWrapper = context.GetRequiredService<IHttpResponseWrapper>();
            var actionResultWrapperFactory = context.GetRequiredService<IActionResultWrapperFactory>();
            actionResultWrapperFactory.CreateFor(context).Wrap(context);

            var wrapperHeaders = new Dictionary<string, string>()
            {
                { AbpHttpWrapConsts.AbpWrapResult, "true" }
            };
            var responseWrapperContext = new HttpResponseWrapperContext(
                context.HttpContext,
                (int)options.HttpStatusCode,
                wrapperHeaders);

            httpResponseWrapper.Wrap(responseWrapperContext);

            //context.HttpContext.Response.Headers.Add(AbpHttpWrapConsts.AbpWrapResult, "true");
            //context.HttpContext.Response.StatusCode = (int)options.HttpStatusCode;

            return Task.CompletedTask;
        }

    }
}
