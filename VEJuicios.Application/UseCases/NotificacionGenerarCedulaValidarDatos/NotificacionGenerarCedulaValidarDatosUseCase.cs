using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos
{
    public sealed class NotificacionGenerarCedulaValidarDatosUseCase : INotificacionGenerarCedulaValidarDatosUseCase
    {
        private readonly INotificacionGenerarCedulaValidarDatosOutputPort _NotificacionGenerarCedulaValidarDatosOutputPort;
        private readonly IVNotificacionDatosParaGenerarCedulaRepository _VNotificacionDatosParaGenerarCedulaRepository;
        public NotificacionGenerarCedulaValidarDatosUseCase(INotificacionGenerarCedulaValidarDatosOutputPort notificacionGenerarCedulaValidarDatosOutputPort,
            IVNotificacionDatosParaGenerarCedulaRepository vNotificacionDatosParaGenerarCedulaRepository)
        {
            this._NotificacionGenerarCedulaValidarDatosOutputPort = notificacionGenerarCedulaValidarDatosOutputPort;
            this._VNotificacionDatosParaGenerarCedulaRepository = vNotificacionDatosParaGenerarCedulaRepository;
        }
        public Task Execute(NotificacionGenerarCedulaValidarDatosInput input)
        {
            try
            {
                var consultaDB = this._VNotificacionDatosParaGenerarCedulaRepository.GetById(input.NotificacionId);
                if (consultaDB == null)
                {
                    throw new Exception("No se Encontro una Notificacion con el Id solicitado.");
                }
                List<string> propiedadesNull = new List<string>();
                Type type = consultaDB.GetType();
                PropertyInfo[] propiedades = type.GetProperties();
                DescripcionPropiedadesVNotificacionPDF descripcionPropiedades = new();
                foreach (PropertyInfo propiedad in propiedades)
                {
                    object valor = propiedad.GetValue(consultaDB);

                    if (valor == null || valor == "")
                    {
                        if (propiedad.Name != "AbogadoTelefono" && propiedad.Name != "AbogadoCelular" && propiedad.Name != "AbogadoEmail")
                        {

                            PropertyInfo propiedadDescripcion = descripcionPropiedades.GetType().GetProperty(propiedad.Name);
                            if (propiedadDescripcion != null)
                            {
                                object valorDescripcion = propiedadDescripcion.GetValue(descripcionPropiedades);
                                propiedadesNull.Add(valorDescripcion.ToString());
                            }
                        }
                    }
                }
                if (propiedadesNull.Count > 0)
                {
                    string camposNulosError = "Antes de continuar revise estos datos: " + string.Join(", ", propiedadesNull);
                    throw new Exception(camposNulosError);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Task.CompletedTask;
        }
    }
}
