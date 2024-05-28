using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionAlta
{
    public sealed class NotificacionAltaUseCase : INotificacionAltaUseCase
    {
        private readonly INotificacionAltaStorePRepository _NotificacionAltaStorePRepository;
        private readonly INotificacionAltaPresenterOutputPort _NotificacionAltaPresenterOutputPort;
        public NotificacionAltaUseCase(
            INotificacionAltaStorePRepository notificacionAltaStorePRepository,
            INotificacionAltaPresenterOutputPort notificacionAltaPresenterOutputPort)
        {
            _NotificacionAltaStorePRepository = notificacionAltaStorePRepository;
            _NotificacionAltaPresenterOutputPort = notificacionAltaPresenterOutputPort;
        }
        public async Task Execute(NotificacionAltaInput input)
        {
            try
            {
                string xmlData = "<Parametro>" +
                    "<Tabla>Notificacion</Tabla>" +
                    "<NotificacionId>0</NotificacionId>" +
                    "<CasoId>" + input.CasoId + "</CasoId>" +
                    "<NotificacionTipoId>" + input.TipoNotificacionDetalleId + "</NotificacionTipoId>" +
                    "<NotificacionTipoNotificacionId>" + input.TipoNotificacionId + "</NotificacionTipoNotificacionId>" +
                    "<ParteRolId>" + input.ParteId + "</ParteRolId>" +
                    "<LiquidacionId>" + input.LiquidacionId + "</LiquidacionId>" +
                    "<OficioBCRAId>" + input.OficioBCRAId + "</OficioBCRAId>" +
                    "<OperadorId>" + input.UsuarioId + "</OperadorId>" +
                    "</Parametro>";
                byte[] archivoData = null;

                int notificacionId = await _NotificacionAltaStorePRepository.AltaNotificacion(xmlData, archivoData);
                if (notificacionId <= 0)
                {
                    throw new Exception("No se ha podido crear la notificación");
                }
                var output = new NotificacionAltaOutput(notificacionId);
                this._NotificacionAltaPresenterOutputPort.Standard(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
