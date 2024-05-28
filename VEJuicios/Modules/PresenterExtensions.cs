using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Presenters;
using Microsoft.Extensions.DependencyInjection;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
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
    public static class PresenterExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {

            services.AddScoped<GenerarNotificacionPresenter, GenerarNotificacionPresenter>();
            services.AddScoped<IGenerarNotificacionPDFOutputPort>(x =>
                x.GetRequiredService<GenerarNotificacionPresenter>());

            services.AddScoped<GenerarPDFEmbargoCuentaSOJPresenter, GenerarPDFEmbargoCuentaSOJPresenter>();
            services.AddScoped<IGenerarPDFEmbargoCuentaSOJOutputPort>(x =>
                x.GetRequiredService<GenerarPDFEmbargoCuentaSOJPresenter>());

            services.AddScoped<NotificacionAñadirArchivoPresenter, NotificacionAñadirArchivoPresenter>();
            services.AddScoped<INotificacionAñadirArchivoPresenterOutputPort>(x =>
                x.GetRequiredService<NotificacionAñadirArchivoPresenter>());

            services.AddScoped<VNotificacionVerificarMegasPresenter, VNotificacionVerificarMegasPresenter>();
            services.AddScoped<INotificacionVerigficarMegasPresenterOutputPort>(x =>
                x.GetRequiredService<VNotificacionVerificarMegasPresenter>());

            services.AddScoped<NotificacionConfirmarNotificacionesPresenter, NotificacionConfirmarNotificacionesPresenter>();
            services.AddScoped<INotificacionConfirmarNotificacionesOutputPort>(x =>
                x.GetRequiredService<NotificacionConfirmarNotificacionesPresenter>());

            services.AddScoped<NotificacionGenerarCedulaValidarDatosPresenter, NotificacionGenerarCedulaValidarDatosPresenter>();
            services.AddScoped<INotificacionGenerarCedulaValidarDatosOutputPort>(x =>
                x.GetRequiredService<NotificacionGenerarCedulaValidarDatosPresenter>());

            services.AddScoped<NotificacionAltaPresenter, NotificacionAltaPresenter>();
            services.AddScoped<INotificacionAltaPresenterOutputPort>(x =>
                x.GetRequiredService<NotificacionAltaPresenter>());

            services.AddScoped<InfractorAltaPresenter, InfractorAltaPresenter>();
            services.AddScoped<IInfractorAltaOutputPort>(x =>
                x.GetRequiredService<InfractorAltaPresenter>());

            services.AddScoped<GenerarConstanciaPresenter, GenerarConstanciaPresenter>();
            services.AddScoped<IGenerarConstanciaNotificacionPDFOutputPort>(x =>
                x.GetRequiredService<GenerarConstanciaPresenter>());

            services.AddScoped<NotificacionDescargarAdjuntoEnviadoPresenter, NotificacionDescargarAdjuntoEnviadoPresenter>();
            services.AddScoped<INotificacionDescargarAdjuntoEnviadoOutputPort>(x =>
                x.GetRequiredService<NotificacionDescargarAdjuntoEnviadoPresenter>());

            return services;
        }
    }
}
