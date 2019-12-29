using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Ingos.Api
{
    public class Program
    {
        /// <summary>
        /// Namespace name
        /// </summary>
        public static readonly string Namespace = typeof(Program).Namespace;

        /// <summary>
        /// App Name
        /// </summary>
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static void Main(string[] args)
        {
            // Use NLog to catch all errors
            //
            var logger = NLogBuilder.ConfigureNLog("Configurations/nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info($"{AppName} configuring web host...");
                var host = CreateWebHostBuilder(args).Build();

                logger.Info($"{AppName} starting web host...");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"{AppName} Program terminated unexpectedly");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logger =>
                {
                    logger.ClearProviders();
                    logger.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}