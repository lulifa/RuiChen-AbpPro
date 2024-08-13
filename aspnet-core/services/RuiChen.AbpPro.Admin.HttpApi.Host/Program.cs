using RuiChen.AbpPro.Admin.HttpApi.Host;
using Serilog;
using Volo.Abp.IO;
using Volo.Abp.Modularity.PlugIns;

namespace Pure.Abp.Admin.HttpApi.Host;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        try
        {
            Console.Title = "Admin.Host";
            Log.Information("Starting Admin.Host.");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, provider, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                });
            await builder.AddApplicationAsync<RuichenAbpProAdminHttpApiHostModule>(options =>
            {
                RuichenAbpProAdminHttpApiHostModule.ApplicationName = Environment.GetEnvironmentVariable("APPLICATION_NAME")
                    ?? RuichenAbpProAdminHttpApiHostModule.ApplicationName;
                options.ApplicationName = RuichenAbpProAdminHttpApiHostModule.ApplicationName;
                // �ӻ�������ȡ�û���������, ��������������
                options.Configuration.UserSecretsId = Environment.GetEnvironmentVariable("APPLICATION_USER_SECRETS_ID");
                // �������û��ָ���û�����, ����Ŀ��ȡ
                options.Configuration.UserSecretsAssembly = typeof(RuichenAbpProAdminHttpApiHostModule).Assembly;
                // ���� Modules Ŀ¼�������ļ���Ϊ���
                // ȡ����ʾ��������������Ŀ��ģ�飬��Ϊͨ���������ʽ����
                var pluginFolder = Path.Combine(
                        Directory.GetCurrentDirectory(), "Modules");
                DirectoryHelper.CreateIfNotExists(pluginFolder);
                options.PlugInSources.AddFolder(
                    pluginFolder,
                    SearchOption.AllDirectories);
            });
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            Console.WriteLine("Host terminated unexpectedly!");
            Console.WriteLine(ex.ToString());
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
