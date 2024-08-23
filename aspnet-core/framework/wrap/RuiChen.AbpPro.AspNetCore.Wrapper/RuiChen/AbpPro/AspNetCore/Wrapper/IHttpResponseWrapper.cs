namespace RuiChen.AbpPro.AspNetCore.Wrapper
{
    public interface IHttpResponseWrapper
    {
        void Wrap(HttpResponseWrapperContext context);
    }
}
