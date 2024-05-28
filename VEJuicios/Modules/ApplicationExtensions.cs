using VEJuicios.Application.UseCases.GenerarNotificacion;
using Microsoft.Extensions.DependencyInjection;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
using VEJuicios.Application.UseCases.NotificacionEliminarArchivo;
using VEJuicios.Application.UseCases.NotificacionGrabarRelacionArchivos;
using VEJuicios.Application.UseCases.NotificacionEliminarRelacionArchivos;
using VEJuicios.Application.WorkerUseCases.EnvioAfip;
using VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones;
using VEJuicios.Application.UseCases.NotificacionVerificarMegas;
using VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos;
using VEJuicios.Application.UseCases.NotificacionAlta;
using VEJuicios.Application.UseCases.InfractorAlta;
using VEJuicios.Application.UseCases.GenerarConstanciaNotificacion;
using VEJuicios.Application.UseCases.NotificacionDescargarAdjuntoEnviado;
using VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ;

namespace VEJuicios.Modules
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGenerarNotificacionPDFUseCase, GenerarNotificacionPDFUseCase>();
            services.AddScoped<IGenerarPDFEmbargoCuentaSOJUseCase, GenerarPDFEmbargoCuentaSOJUseCase>();
            services.AddScoped<INotificacionEliminarArchivoUseCase, NotificacionEliminarArchivoUseCase>();
            services.AddScoped<INotificacionAñadirAdjuntoUseCase, NotificacionAñadirAdjuntoUseCase>();
            services.AddScoped<INotificacionGrabarRelacionArchivosUseCase, NotificacionGrabarRelacionArchivosUseCase>();
            services.AddScoped<INotificacionEliminarRelacionArchivosUseCase, NotificacionEliminarRelacionArchivosUseCase>();
            services.AddScoped<INotificacionEnvioAfipWorkerUseCase, NotificacionEnvioAfipWorkerUseCase>();
            services.AddScoped<INotificacionConfirmarNotificacionesUseCase, NotificacionConfirmarNotificacionesUseCase>();
            services.AddScoped<INotificacionVerificarMegasUseCase, NotificacionVerificarMegasUseCase>();
            services.AddScoped<INotificacionGenerarCedulaValidarDatosUseCase, NotificacionGenerarCedulaValidarDatosUseCase>();
            services.AddScoped<INotificacionRecepcionAfipWorkerUseCase, NotificacionRecepcionAfipWorkerUseCase>();
            services.AddScoped<INotificacionAltaUseCase, NotificacionAltaUseCase>();
            services.AddScoped<IInfractorAltaUseCase, InfractorAltaUseCase>();
            services.AddScoped<IGenerarConstanciaNotificacionPDFUseCase, GenerarConstanciaNotificacionPDFUseCase>();
            services.AddScoped<INotificacionDescargarAdjuntoEnviadoUseCase, NotificacionDescargarAdjuntoEnviadoUseCase>();

            return services;
        }
    }
}
