using RuiChen.AbpPro.Serilog.Enrichers.Application;
using Serilog.Configuration;

namespace Serilog
{
    public static class ApplicationLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithApplicationName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ApplicationNameEnricher>();
        }
    }
}
