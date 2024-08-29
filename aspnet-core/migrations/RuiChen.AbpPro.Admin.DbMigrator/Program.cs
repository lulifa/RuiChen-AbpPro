﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace RuiChen.AbpPro.Admin.DbMigrator
{
    public class Program
    {
        public async static Task Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                    .MinimumLevel.Override("RuiChen.AbpPro.Admin.DbMigrator", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("RuiChen.AbpPro.Admin.DbMigrator", LogEventLevel.Information)
#endif
                    .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/migrations.txt")
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .AddAppSettingsSecretsJson()
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RuiChenAbpProAdminDbMigratorHostedService>();
                });
        }
    }

}
