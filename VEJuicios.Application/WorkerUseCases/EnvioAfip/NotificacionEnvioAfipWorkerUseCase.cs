using DGIIT.Integration.VEAfipApi.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.WorkerUseCases.EnvioAfip
{
    public class NotificacionEnvioAfipWorkerUseCase : INotificacionEnvioAfipWorkerUseCase
    {
        private readonly CredentialNetworkAfip _credentialNetworkAfip;
        private readonly AfipConnection _afipConnection;
        private readonly ILogger<NotificacionEnvioAfipWorkerUseCase> _logger;
        private readonly IVistaNotificacionRepository _vistaNotificacionRepository;
        private readonly IVNotificacionArchivoEnvioWorkerRepository _vNotificacionArchivoEnvioWorkerRepository;
        private readonly INotificacionStoreRepository _notificacionStorePRepository;
        private readonly INotificacionAdjuntoEnviadoRepository _notificacionAdjuntoEnviadoRepository;

        public NotificacionEnvioAfipWorkerUseCase(ILogger<NotificacionEnvioAfipWorkerUseCase> logger, IOptions<CredentialNetworkAfip> credentialNetworkAfip, IOptions<AfipConnection> afipConnection,
            IVistaNotificacionRepository vistaNotificacionRepository, IVNotificacionArchivoEnvioWorkerRepository vNotificacionArchivoEnvioWorkerRepository,
            INotificacionStoreRepository notificacionStorePRepository, INotificacionAdjuntoEnviadoRepository notificacionAdjuntoEnviadoRepository)
        {
            _credentialNetworkAfip = credentialNetworkAfip.Value;
            _afipConnection = afipConnection.Value;
            _logger = logger;
            _vistaNotificacionRepository = vistaNotificacionRepository;
            _vNotificacionArchivoEnvioWorkerRepository = vNotificacionArchivoEnvioWorkerRepository;
            _notificacionStorePRepository = notificacionStorePRepository;
            _notificacionAdjuntoEnviadoRepository = notificacionAdjuntoEnviadoRepository;
        }

        public async Task ExecuteSync()
        {
            try
            {
                NetworkCredential networkCredential = new(_credentialNetworkAfip.user, _credentialNetworkAfip.password, _credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(_afipConnection.urlVeAfip, networkCredential);

                var notificacionesAEnviar = await _vistaNotificacionRepository.GetAllPendientesEnvio();
                _logger.LogInformation("Notificaciones a Enviar:{0} ", notificacionesAEnviar.Count);

                if (notificacionesAEnviar.Any())
                {
                    //Hoy Notificaciones para enviar

                    List<VNotificacionArchivoEnvioWorker> archivosEnviar = new();
                    archivosEnviar = await _vNotificacionArchivoEnvioWorkerRepository.GetAllNotificacionArchivoEnvioWorkerAsync();

                    foreach (var notificacion in notificacionesAEnviar)
                    {
                        var archivosNotificacion = archivosEnviar.Where(x => x.NotificacionId == notificacion.NotificacionId).ToList();

                        if (archivosNotificacion.Count > 0)
                        {
                            Dictionary<int, string> archivosEnviados = new Dictionary<int, string>();
                            string cedulaId = "";

                            foreach (var archivo in archivosNotificacion)
                            {

                                var adjunto = new RequestAdjunto
                                {
                                    AfipSistemaId = (int)AfipSistemaId.PNRT,
                                    Content = archivo.Data,
                                    Metadata = archivo.MetaData,
                                    ContentType = archivo.ContentType,
                                    NombreArchivo = archivo.FileName
                                };

                                //Envio adjunto
                                var responseAdjunto = cliente.AddAdjuntoAsync(adjunto).Result;

                                if (responseAdjunto == null || (responseAdjunto.ErrorMsg != null && responseAdjunto.ErrorMsg != ""))
                                {
                                    _logger.LogError("Fallo en la subida del Archivo " + archivo.ArchivoId.ToString());
                                    return;
                                }

                                //Guardo Ids de Adjuntos
                                if (archivo.Cedula == true)
                                {
                                    cedulaId = responseAdjunto.Id;
                                }
                                else
                                {
                                    archivosEnviados.Add(archivo.ArchivoId, responseAdjunto.Id);
                                }
                            } // Fin loop Archivos


                            var ref1 = "Cedula: " + cedulaId;
                            if (archivosEnviados.Count > 0)
                            {
                                ref1 += " Adjuntos:";
                                foreach (var item in archivosEnviados)
                                {
                                    ref1 += " " + item.Value;
                                }
                            }

                            var req = new RequestNotificacion
                            {
                                AfipSistemaId = (int)AfipSistemaId.PNRT,
                                Asunto = "Notificacion",
                                Cuit = notificacion.Cuit.Replace("-", ""),
                                Ref1 = ref1
                            };

                            var respNotificacion = cliente.AddNotificacionAsync(req).Result;

                            if (respNotificacion == null || respNotificacion.AfipError == true)
                            {
                                _logger.LogError("Fallo en la subida la notificación " + notificacion.NotificacionId.ToString());
                                return;
                            }

                            _logger.LogInformation("Notificacion Id:{0} Enviada", notificacion.NotificacionId);

                            //Adjunto Cédula
                            var attachCedula = await cliente.AttachAdjuntoAsync(respNotificacion.Id, cedulaId);

                            if (attachCedula == null || (attachCedula.ErrorMsg != null && attachCedula.ErrorMsg != ""))
                            {
                                _logger.LogError("Fallo en la subida la relacion " + notificacion.Cuit);
                                return;
                            }
                            try
                            {
                                var archivoCedula = archivosNotificacion.SingleOrDefault(x => x.Cedula);

                                if (archivoCedula != null)
                                {
                                    await _notificacionAdjuntoEnviadoRepository.AddAsync(new NotificacionAdjuntoEnviado(notificacion.NotificacionId,
                                                                                            attachCedula.AdjuntoId, true, archivoCedula.GeneradoAutomatico, archivoCedula.FileName,
                                                                                            archivoCedula.TipoNotificacionDescripcion, archivoCedula.FechaInsert, archivoCedula.UsuarioInsertId));
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("Fallo en la subida de la Cedula Enviada " + attachCedula.AdjuntoId);
                            }

                            //Adjunto Archivos
                            foreach (var item in archivosEnviados)
                            {
                                var archivoActual = archivosNotificacion.FirstOrDefault(an => an.ArchivoId == item.Key);

                                if (archivoActual != null)
                                {
                                    var adjunto = await cliente.AttachAdjuntoAsync(respNotificacion.Id, item.Value);

                                    if (adjunto == null || (adjunto.ErrorMsg != null && adjunto.ErrorMsg != ""))
                                    {
                                        _logger.LogError("Fallo en la subida la relacion " + notificacion.Cuit);
                                        return;
                                    }

                                    try
                                    {

                                        await _notificacionAdjuntoEnviadoRepository.AddAsync(new NotificacionAdjuntoEnviado(notificacion.NotificacionId,
                                                                                                    adjunto.AdjuntoId, false, false, archivoActual.FileName,
                                                                                                    archivoActual.TipoNotificacionDescripcion, archivoActual.FechaInsert, archivoActual.UsuarioInsertId));
                                        _logger.LogInformation("Notificacion Id:{0} Adjunto Id:{1} File:{2}", notificacion.NotificacionId, adjunto.AdjuntoId, archivoActual.FileName);

                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError("Fallo en la subida la del Adjunto Enviado " + adjunto.AdjuntoId);
                                    }
                                }
                            }

                            //Update Notificacion
                            await _notificacionStorePRepository.EnvioAfip(notificacion.NotificacionId, new Guid(respNotificacion.Id));

                            _logger.LogInformation("EnvioAfip Notificacion Id:{0} AfipId {1}", notificacion.NotificacionId, respNotificacion.Id);
                        } 
                        else
                        {
                            _logger.LogError("Notificacion Id:{0} No tiene ningún archivo para notificar", notificacion.NotificacionId);
                        }
                    }
                }           
            }
            catch (Exception ex)
            {
                _logger.LogError("Se genero un error el servicio de Envio de Notificaciones. El error es: " + ex.Message.ToString());
            }
            await Task.CompletedTask;
        }

    }



}
