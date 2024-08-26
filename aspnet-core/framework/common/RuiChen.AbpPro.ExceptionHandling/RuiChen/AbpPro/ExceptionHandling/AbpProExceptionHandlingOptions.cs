using Volo.Abp.Collections;

namespace RuiChen.AbpPro.ExceptionHandling
{
    public class AbpProExceptionHandlingOptions
    {
        public ITypeList<Exception> Handlers { get; }
        public AbpProExceptionHandlingOptions()
        {
            Handlers = new TypeList<Exception>();
        }

        public bool HasNotifierError(Exception ex)
        {
            if (typeof(IHasNotifierErrorMessage).IsAssignableFrom(ex.GetType()))
            {
                return true;
            }
            return Handlers.Any(x => x.IsAssignableFrom(ex.GetType()));
        }
    }
}
