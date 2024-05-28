using DGIIT.Integration.VEAfipApi.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.GenerarConstanciaNotificacion;
using VEJuicios.Application.UseCases.NotificacionAlta;
using VEJuicios.Application.WorkerUseCases.EnvioAfip;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionDescargarAdjuntoEnviado
{
    public sealed class NotificacionDescargarAdjuntoEnviadoUseCase : INotificacionDescargarAdjuntoEnviadoUseCase
    {
        private readonly INotificacionAltaStorePRepository _NotificacionAltaStorePRepository;
        private readonly INotificacionDescargarAdjuntoEnviadoOutputPort _NotificacionDescargarAdjuntoEnviadoOutputPort;
        private readonly AfipConnection _afipConnection;
        private readonly CredentialNetworkAfip _credentialNetworkAfip;
        private readonly ILogger<NotificacionDescargarAdjuntoEnviadoUseCase> _logger;

        public NotificacionDescargarAdjuntoEnviadoUseCase(
            INotificacionAltaStorePRepository notificacionAltaStorePRepository,
            INotificacionDescargarAdjuntoEnviadoOutputPort notificacionDescargarAdjuntoEnviadoOutputPort,
            IOptions<AfipConnection> afipConnection,
            IOptions<CredentialNetworkAfip> credentialNetworkAfip,
            ILogger<NotificacionDescargarAdjuntoEnviadoUseCase> logger)
        {
            _NotificacionAltaStorePRepository = notificacionAltaStorePRepository;
            _NotificacionDescargarAdjuntoEnviadoOutputPort = notificacionDescargarAdjuntoEnviadoOutputPort;
            this._afipConnection = afipConnection.Value;
            this._credentialNetworkAfip = credentialNetworkAfip.Value;

            _logger = logger;
        }
        public async Task Execute(NotificacionDescargarAdjuntoEnviadoInput input)
        {
            NetworkCredential networkCredential = new(_credentialNetworkAfip.user, _credentialNetworkAfip.password, _credentialNetworkAfip.dominio);
            var cliente = new VEAfipApiClient(_afipConnection.urlVeAfip, networkCredential);
            _logger.LogInformation("urlVeAfip: {0} user: {1}", _afipConnection.urlVeAfip, _credentialNetworkAfip.user);

            try
            {

                var adjuntoId = input.ReferenciaApiId.Replace("{", "").Replace("}", "");
                _logger.LogInformation("adjuntoId: {0} input.ReferenciaApiId: {1}", adjuntoId, input.ReferenciaApiId);

                var archivoAdjuntoEnviado = await cliente.GetAdjuntoAsync(adjuntoId);

                if (archivoAdjuntoEnviado != null)
                {
                    if (!String.IsNullOrEmpty(archivoAdjuntoEnviado.ErrorMsg)) {
                        _logger.LogError("Error: {0}", archivoAdjuntoEnviado.ErrorMsg);
                        return;
                    }

                    var json = archivoAdjuntoEnviado.Metadata;
                    _logger.LogInformation("jsonMetadata: {0}", json);

                    // Deserializar el JSON en un objeto JsonDocument.
                    using (JsonDocument doc = JsonDocument.Parse(archivoAdjuntoEnviado.Metadata))
                    {
                        // Obtener el objeto raíz.
                        JsonElement adjuntoMetadata = doc.RootElement;

                        // Acceder al valor de la variable "FileName" utilizando una cadena como clave.
                        string fileName = adjuntoMetadata.GetProperty("FileName").GetString();

                        var output = new NotificacionDescargarAdjuntoEnviadoOutput(archivoAdjuntoEnviado.Content, fileName);
                        this._NotificacionDescargarAdjuntoEnviadoOutputPort.Standard(output);
                    }
                }
                else
                {
                    _logger.LogError("ReferenciaApiId: {0} no encontrada", input.ReferenciaApiId);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("ReferenciaApiId: {0} Error: {1} {2}", input.ReferenciaApiId, ex.Message, ex.StackTrace);
            }
        }
    }
}
