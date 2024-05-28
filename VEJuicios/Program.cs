using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Config;
using NLog.Filters;
using NLog.Targets;
using NLog.Web;
using NLog;
using System;
using System.Configuration;
using VEJuicios.Domain;
using VEJuicios.Infrastructure;

namespace VEJuicios
{
    public class Program
    {
        static NLog.Logger _logger = null;
        static IConfiguration _config = null;

        public static void Main(string[] args)
        {
            var _hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("nlog.json", optional: true, reloadOnChange: true);
            })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.ConfigureKestrel(options =>
               {
                   options.Limits.KeepAliveTimeout = TimeSpan.MaxValue;
               });
               webBuilder.UseStartup<Startup>();
           }).ConfigureServices((hostContext, services) =>
           {
               _config = hostContext.Configuration;

               var connectionString = _config.GetConnectionString("DefaultConnection");
               services.AddDbContext<SQLServerContext>(options => 
                                                            options.UseSqlServer(connectionString));
           })
            .UseNLog();

            var _host = _hostBuilder.Build();

            // configure NLog using the configuration obtained above
            NLog.LogManager.Configuration = new NLog.Extensions.Logging.NLogLoggingConfiguration(_config.GetSection("NLog"));
            
            //Filtro los levels de Microsoft.*
            var x = NLog.LogManager.Configuration;
            x.AddTarget("nulltarget", new NullTarget());
            var nullTarget = x.FindTargetByName("nulltarget");

            var ruleMicrosoft = new LoggingRule("Microsoft.*", nullTarget);
            ruleMicrosoft.Filters.Add(new ConditionBasedFilter()
            {
                Condition = "starts-with(logger, 'Microsoft')",
                Action = FilterResult.Ignore
            });
            ruleMicrosoft.FinalMinLevel = LogLevel.Warn;
            ruleMicrosoft.Final = true;

            x.LoggingRules.Insert(0, ruleMicrosoft);
            //End Filtro

            // obtain the logger
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _host.Run();
        }
    }
}
