using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

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
                var host = CreateHostBuilder(args).Build();

                logger.Info($"{AppName} starting web host...");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, $"{AppName} Program terminated unexpectedly");
                throw ex;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logger =>
                {
                    logger.ClearProviders();
                    logger.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
    }
}