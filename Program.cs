using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleCli
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            //Set appsetting.json file to save the configuration in .NET Core.
            var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            // Set the host for app startup and lifetime management.
            var builder = new HostBuilder()
                        .ConfigureServices((hostContext, services) =>
                        {
                            // set the logging by using Serilog
                            services.AddLogging(config =>
                            {
                                config.ClearProviders();
                                config.AddProvider(new SerilogLoggerProvider(Log.Logger));
                            });

                            //services.AddHttpClient();
                        });

                        try
                        {
                            //return await builder.RunCommandLineApplicationAsync<SimpleCliClass>(args);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return 1;
                        }
        }
    }
}
