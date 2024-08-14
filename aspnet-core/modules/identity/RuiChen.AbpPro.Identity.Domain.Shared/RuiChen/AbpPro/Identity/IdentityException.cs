using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Logging;
using Volo.Abp;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;

namespace RuiChen.AbpPro.Identity
{
    public class IdentityException : BusinessException, IExceptionWithSelfLogging
    {
        public IdentityException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {

        }

        public IdentityException(
            string code = null,
            string message = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(code, message, details, innerException, logLevel)
        {

        }

        public void Log(ILogger logger)
        {
            logger.Log(LogLevel, "An id error occurred,code: {0}, Message: {1}, Details: {2}", Code, Message, Details);
        }
    }
}
