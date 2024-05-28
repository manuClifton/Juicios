using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Application.UseCases.NotificacionEliminarArchivo;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones
{
    public sealed class NotificacionConfirmarNotificacionesUseCase : INotificacionConfirmarNotificacionesUseCase
    {
        private readonly IvNotificacionesAConfirmarRepository _vNotificacionesAConfirmarRepository;
        private readonly INotificacionRepository _notificacionRepository;
        private readonly INotificacionConfirmarNotificacionesOutputPort _ConfirmarNotificacionesOutputPort;
        private readonly IVNotificacionDatosParaGenerarCedulaRepository _VNotificacionDatosParaGenerarCedulaRepository;
        private readonly INotificacionMovimientoAltaStorePRepository _NotificacionMovimientoAltaStorePRepository;
        public NotificacionConfirmarNotificacionesUseCase(IvNotificacionesAConfirmarRepository vNotificacionesAConfirmarRepository,
            INotificacionRepository notificacionRepository,
            INotificacionConfirmarNotificacionesOutputPort notificacionConfirmarNotificacionesOutputPort,
            IVNotificacionDatosParaGenerarCedulaRepository vNotificacionDatosParaGenerarCedulaRepository,
            INotificacionMovimientoAltaStorePRepository notificacionMovimientoAltaStorePRepository
            )
        {
            this._vNotificacionesAConfirmarRepository = vNotificacionesAConfirmarRepository;
            this._notificacionRepository = notificacionRepository;
            this._ConfirmarNotificacionesOutputPort = notificacionConfirmarNotificacionesOutputPort;

            this._NotificacionMovimientoAltaStorePRepository = notificacionMovimientoAltaStorePRepository;
            this._VNotificacionDatosParaGenerarCedulaRepository = vNotificacionDatosParaGenerarCedulaRepository;
        }

        public async Task Execute(NotificacionConfirmarNotificacionesInput input)
        {
            try
            {
                bool correctoParaConfirmar = true;
                List<string> notificacionesSinCuit = new List<string>();
                List<int> notificacionesConErrores = new List<int>();
                foreach (var notificacionId in input.NotificacionesId)
                {
                    bool notificacionCorrecta = this._vNotificacionesAConfirmarRepository.GetCorrectoParaConfirmar(notificacionId);
                    if (!notificacionCorrecta)
                    {
                        correctoParaConfirmar = false;
                        notificacionesConErrores.Add(notificacionId);
                    }

                    var personaCuit = this._VNotificacionDatosParaGenerarCedulaRepository.GetById(notificacionId).PersonaCuit;
                    if (personaCuit == null || personaCuit == "")
                    {
                        notificacionesSinCuit.Add(notificacionId.ToString());
                    }
                }
                if (correctoParaConfirmar && notificacionesSinCuit.Count == 0)
                {
                    foreach (var notificacionId in input.NotificacionesId)
                    {
                        this._notificacionRepository.ConfirmarNotificacion(notificacionId, input.UserId);
                    }
                }
                else
                {
                    string mensajeError;
                    if (notificacionesConErrores.Count > 0)
                    {
                        if (notificacionesConErrores.Count > 1)
                        {
                            notificacionesConErrores = this._notificacionRepository.GetNumerosInternosNotificaciones(notificacionesConErrores);
                            notificacionesConErrores.Sort();
                            mensajeError = string.Join(", ", notificacionesConErrores);
                            mensajeError = "Las notificaciones " + mensajeError + " no se pueden confirmar pues les falta una cédula o un adjunto.";
                        }
                        else
                        {
                            notificacionesConErrores = this._notificacionRepository.GetNumerosInternosNotificaciones(notificacionesConErrores);
                            mensajeError = string.Join(", ", notificacionesConErrores);
                            mensajeError = "La notificación " + mensajeError + " no se puede confirmar pues le falta una cédula o un adjunto.";
                        }
                    }
                    else
                    {
                        if (notificacionesSinCuit.Count > 1)
                        {
                            notificacionesSinCuit.Sort();
                            mensajeError = string.Join(", ", notificacionesSinCuit);
                            mensajeError = "Las notificaciones " + mensajeError + " no se pueden confirmar pues les falta cargar un Cuit.";
                        }
                        else
                        {
                            mensajeError = string.Join(", ", notificacionesSinCuit);
                            mensajeError = "La notificación " + mensajeError + " no se puede confirmar pues le falta cargar un Cuit.";
                        }
                    }
                    this._ConfirmarNotificacionesOutputPort.Standard(mensajeError);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
