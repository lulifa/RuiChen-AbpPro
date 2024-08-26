using Microsoft.Extensions.Logging;

namespace RuiChen.AbpPro.Auditing
{
    public class LogDto
    {
        public DateTime TimeStamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public LogFieldDto Fields { get; set; }
        public List<LogExceptionDto> Exceptions { get; set; }
    }
}
