using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Education.Web {
    public class Program {
        public static void Main(string[] args) {
            // Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging");
            CreateHostBuilder(args).Build().Run();
        }

        // inits the IConfigurationBuilder to read json settings
        // Initialized IConfigurationBuilder will be used to init IConfiguration prop in Startup
        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    var env = hostingContext.HostingEnvironment;

                    config
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging(logging => {
                    logging
                        .ClearProviders()
                        .AddConsole()
                        .AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIISIntegration();
                    webBuilder.ConfigureKestrel(options => {
                        //options.Limits = new Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerLimits { }
                    });

                    //var currentAssemblyName = typeof(Startup).GetType().Assembly.FullName;
                    //webBuilder.UseStartup(currentAssemblyName);
                });
        }
    }
}
