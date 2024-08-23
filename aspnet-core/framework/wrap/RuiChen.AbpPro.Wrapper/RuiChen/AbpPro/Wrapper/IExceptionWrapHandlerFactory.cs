namespace RuiChen.AbpPro.Wrapper
{
    public interface IExceptionWrapHandlerFactory
    {
        IExceptionWrapHandler CreateFor(ExceptionWrapContext context);
    }
}
