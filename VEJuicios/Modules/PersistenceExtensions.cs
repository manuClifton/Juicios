using VEJuicios.Domain;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infraestructure.Repositories;
using VEJuicios.Infrastructure;
using VEJuicios.Infrastructure.Repositories;
//using VEJuicios.Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Model.Infractores;

namespace VEJuicios.Modules
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddSQLServer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SQLServerContext>(
                options =>
                {
                    options.UseSqlServer(
                        configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
                    options.EnableSensitiveDataLogging();
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<INotificacionAdjuntoEnviadoRepository, NotificacionAdjuntoEnviadoRepository>();
            services.AddScoped<IVistaNotificacionRepository, VistaNotificacionRepository>();
            services.AddScoped<IVistaNotificacionPDFRepository, VistaNotificacionPDFRepository>();
            services.AddScoped<IvNotificacionPDFEmbargoCuentaSOJRepository, vNotificacionPDFEmbargoCuentaSOJRepsitory>();
            services.AddScoped<INotificacionArchivoTemporalRepository, NotificacionArchivoTemporalRepository>();
            services.AddScoped<INotificacionArchivoTemporalRelacionRepository, NotificacionArchivoTemporalRelacionRepository>();
            services.AddScoped<IVNotificacionArchivoEnvioWorkerRepository, VNotificacionArchivoEnvioWorkerRepository>();
            services.AddScoped<INotificacionStoreRepository, NotificacionStorePRepository>();
            services.AddScoped<IVNotificacionArchivoTemporalMetadataRepository, VistaNotificacionArchivoTemporalMetadataRepository>();
            services.AddScoped<IvNotificacionesAConfirmarRepository, vNotificacionesAConfirmarRepository>();
            services.AddScoped<IVNotificacionDatosParaGenerarCedulaRepository, VNotificacionDatosParaGenerarCedulaRepository>();
            services.AddScoped<INotificacionAltaStorePRepository, NotificacionAltaStorePRepository>();
            services.AddScoped<IInfractorAltaStorePRepository, InfractorAltaStorePRepository>();
            services.AddScoped<IVNotificacionConstanciaRepository, NotificacionConstanciaRepository>();
            services.AddScoped<INotificacionMovimientoAltaStorePRepository, NotificacionMovimientoAltaStorePRepository>();

            return services;

        }
    }
}