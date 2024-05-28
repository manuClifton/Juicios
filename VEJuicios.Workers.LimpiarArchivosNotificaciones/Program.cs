using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Config;
using NLog.Filters;
using NLog.Targets;
using NLog.Web;

using System.Reflection;

using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infraestructure.Repositories;
using VEJuicios.Infrastructure;
using VEJuicios.Infrastructure.Repositories;
using VEJuicios.Services;
using VEJuicios.Workers.Common.Configurations;
using VEJuicios.Workers.LimpiarArvhivosNotificaciones;

namespace VEJuicios.Workers
{
    public class Program
    {
        static NLog.Logger _logger = null;
        static IConfiguration _config = null;
        public static void Main(string[] args)
        {

            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("nlog.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    _config = hostContext.Configuration;
                    var configuration = hostContext.Configuration;

                    var connectionString = _config.GetConnectionString("DefaultConnection");
                    services.AddDbContext<SQLServerContext>(options =>
                                                                 options.UseSqlServer(connectionString));


                    services.Configure<WorkerMonOptions>(_config.GetSection(WorkerMonOptions.Section));
                    services.Configure<AppUserOptions>(_config.GetSection("AppUser"));
                    services.Configure<AfipConnection>(_config.GetSection("AfipConnection"));
                    services.Configure<CredentialNetworkAfip>(_config.GetSection("CredentialNetworkAfip"));

                    services.AddTransient<IVistaNotificacionRepository, VistaNotificacionRepository>();
                    services.AddTransient<IVNotificacionArchivoEnvioWorkerRepository, VNotificacionArchivoEnvioWorkerRepository>();
                    services.AddTransient<INotificacionStoreRepository, NotificacionStorePRepository>();
                    services.AddTransient<INotificacionAdjuntoEnviadoRepository, NotificacionAdjuntoEnviadoRepository>();

                    services.AddHostedService<WorkerLimpiarArvhivosNotificaciones>();
                })
                .UseNLog();

            var host = hostBuilder.Build();

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

            var version = Assembly.GetEntryAssembly()
                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                        .InformationalVersion;
            _logger.Info("Versión: {0}", version.ToString());

            host.Run();
        }
    }
}
