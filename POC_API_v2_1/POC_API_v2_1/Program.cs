using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace POC_API_v2_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                var logger = LogManager.GetLogger(typeof(Program));
                logger.Info("Inicio de Servicio PROC_API");

                CreateWebHostBuilder(args).Build().Run();

            } catch (Exception ex){

                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                var logger = LogManager.GetLogger(typeof(Program));
                logger.Error(String.Concat("Error al inicial PROC_API: ",ex.Message));
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
