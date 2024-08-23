namespace RuiChen.AbpPro.Wrapper
{
    [Serializable]
    public class WrapResult : WrapResult<object>
    {
        public WrapResult()
        {

        }

        public WrapResult(string code, string message, string details = null) : base(code, message, details)
        {

        }

        public WrapResult(string code, object result, string message = "OK") : base(code, result, message)
        {

        }

    }

    [Serializable]
    public class WrapResult<TResult>
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误提示消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 补充消息
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public TResult Result { get; set; }
        public WrapResult() { }
        public WrapResult(
            string code,
            string message,
            string details = null)
        {
            Code = code;
            Message = message;
            Details = details;
        }

        public WrapResult(
            string code,
            TResult result,
            string message = "OK")
        {
            Code = code;
            Result = result;
            Message = message;
        }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreWrapResultAttribute : Attribute
    {
        public IgnoreWrapResultAttribute()
        {

        }
    }

    public interface IWrapDisabled { }

}
