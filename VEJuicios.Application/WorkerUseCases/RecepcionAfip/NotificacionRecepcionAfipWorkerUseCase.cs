using DGIIT.Integration.VEAfipApi.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using VEJuicios.Domain;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.WorkerUseCases.EnvioAfip
{
    public class NotificacionRecepcionAfipWorkerUseCase : INotificacionRecepcionAfipWorkerUseCase
    {
        private readonly CredentialNetworkAfip _credentialNetworkAfip;
        private readonly AfipConnection _afipConnection;
        private readonly ILogger<NotificacionEnvioAfipWorkerUseCase> _logger;
        private readonly INotificacionRepository _notificacionRepository;
        private readonly INotificacionStoreRepository _notificacionStoreRepository;

        public NotificacionRecepcionAfipWorkerUseCase(ILogger<NotificacionEnvioAfipWorkerUseCase> logger, 
                                                    IOptions<CredentialNetworkAfip> credentialNetworkAfip, 
                                                    IOptions<AfipConnection> afipConnection,
                                                    INotificacionRepository notificacionRepository, 
                                                    INotificacionStoreRepository notificacionStoreRepository)
        {
            _credentialNetworkAfip = credentialNetworkAfip.Value;
            _afipConnection = afipConnection.Value;
            _logger = logger;
            _notificacionRepository = notificacionRepository;
            _notificacionStoreRepository = notificacionStoreRepository;
        }

        public async Task Execute()
        {
            try
            {
                NetworkCredential credentials = new NetworkCredential(_credentialNetworkAfip.user, _credentialNetworkAfip.password, _credentialNetworkAfip.dominio);
                var client = new VEAfipApiClient(_afipConnection.urlVeAfip, credentials);

                var notificaciones = await _notificacionRepository.GetAllWaitingAfipResponse();
                _logger.LogInformation("Notificaciones a procesar:{0} ", notificaciones.Count);

                foreach(var notificacion in notificaciones)
                {
                    if (notificacion.AfipId != null)
                    {
                        var result = await client.GetNotificacionAsync(notificacion.AfipId.ToString());
                        var jsonresult = JsonConvert.SerializeObject(result);
                        _logger.LogInformation("Result:{0}", jsonresult);

                        _logger.LogInformation("Notificacion Id:{0} AfipId:{1} EnviadoAfip:{2} LeidoAfip:{3} Commid:{4} EstadoId:{5}", 
                                                notificacion.NotificacionId,
                                                notificacion.AfipId.ToString(),
                                                result.Enviado,
                                                result.Leida,
                                                result.CommID,
                                                notificacion.EstadoId);

                        if (result != null)
                        {
                            bool tieneCommid = result.CommID > 0 ? true : false;

                            if (result.AfipError)
                            {
                                await _notificacionStoreRepository.RecepcionAfip(notificacion.NotificacionId, EnumEstadoNotificaciones.CanceladoAfip, null, null, null, DateTime.Now);
                            }
                            else
                            {
                                bool cambios = false;

                                //Enviado por AFIP = PublicadoVE
                                if (result.Enviado == true && tieneCommid)
                                {
                                    if (result.Fecha_Envio != null )
                                    {
                                        if (notificacion.EstadoId < EnumEstadoNotificaciones.PublicadoVE)
                                        {
                                            _logger.LogInformation("Notificacion Id:{0} Estado: PublicadoVE", notificacion.NotificacionId);
                                            notificacion.EstadoId = EnumEstadoNotificaciones.PublicadoVE;
                                            notificacion.FechaRecepcionAfip = result.Fecha_Envio;
                                            cambios = true;
                                        }
                                    }
                                }

                                //Notificado
                                if (result.Leida == true && result.Fecha_Notificacion != null )
                                {
                                    if (notificacion.EstadoId < EnumEstadoNotificaciones.Notificado)
                                    {
                                        _logger.LogInformation("Notificacion Id:{0} Estado: Notificado", notificacion.NotificacionId);
                                        notificacion.EstadoId = EnumEstadoNotificaciones.Notificado;
                                        notificacion.FechaNotificacion = result.Fecha_Notificacion;
                                        cambios = true;
                                    }
                                }

                                if (cambios)
                                {
                                    await _notificacionStoreRepository.RecepcionAfip(notificacion.NotificacionId, notificacion.EstadoId, notificacion.FechaEnvio, notificacion.FechaRecepcionAfip, notificacion.FechaNotificacion, notificacion.FechaCancelacion);
                                }
                                else 
                                {
                                    _logger.LogInformation("Notificacion Id:{0} No tuvo cambio de estado", notificacion.NotificacionId);
                                }

                            }
                        }
                        else
                        {
                            _logger.LogError("Se genero un error el servicio de Recepcion de " +
                                "Notificaciones. No se pudo conectar con el servicio para la notificacion"
                                + notificacion.NotificacionId.ToString() + ".");
                        }
                    }
                    else
                    {
                        _logger.LogError("Se genero un error el servicio de Recepcion de " +
                                   "Notificaciones. No se encontro el id de afip para la notificacion"
                                   + notificacion.NotificacionId.ToString() + ".");
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Se genero un error el servicio de Recepcion de Notificaciones. El error es: " + ex.Message.ToString());
            }
            await Task.CompletedTask;
        }

    }



}
