using System.Net;
using Volo.Abp.Http;

namespace RuiChen.AbpPro.Wrapper
{
    public class ExceptionWrapContext
    {
        public ExceptionWrapContext(Exception exception, RemoteServiceErrorInfo errorInfo, IServiceProvider serviceProvider, HttpStatusCode? statusCode = null)
        {
            Exception = exception;
            ErrorInfo = errorInfo;
            ServiceProvider = serviceProvider;
            StatusCode = statusCode;
        }


        public Exception Exception { get; }
        public IServiceProvider ServiceProvider { get; }
        public RemoteServiceErrorInfo ErrorInfo { get; }
        public HttpStatusCode? StatusCode { get; set; }

        public ExceptionWrapContext WithCode(string code)
        {
            ErrorInfo.Code = code;
            return this;
        }

        public ExceptionWrapContext WithMessage(string message)
        {
            ErrorInfo.Message = message;
            return this;
        }

        public ExceptionWrapContext WithDetails(string details)
        {
            ErrorInfo.Details = details;
            return this;
        }

        public ExceptionWrapContext WithData(string key, object value)
        {
            ErrorInfo.Data[key] = value;
            return this;
        }

    }
}
