using VEJuicios.Model;
using DGIIT.Integration.VEAfipApi.NetCore;
using DGIIT.Integration.WSAfip.NetCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using VEJuicios.Domain.Model;
using Microsoft.Extensions.Options;
using VEJuicios.Domain.Notificaciones;
using Newtonsoft.Json;

namespace VEJuicios.Controllers
{
    [ApiController]
    [Route("Afip")]
    [EnableCors]
    public class AfipController : ControllerBase
    {
        private readonly ILogger<AfipController> _logger;
        AfipConnection afipConnection = new AfipConnection();
        CredentialNetworkAfip credentialNetworkAfip = new CredentialNetworkAfip();

        public AfipController(ILogger<AfipController> logger, IOptions<AfipConnection> afipConnection, IOptions<CredentialNetworkAfip> credentialNetworkAfip)
        {
            _logger = logger;
            this.credentialNetworkAfip = credentialNetworkAfip.Value;
            this.afipConnection = afipConnection.Value;
        }

        [HttpPost("/PostNotificacionesConAdjunto")]
        public async Task<IActionResult> postNotificacionAdjunto([FromBody] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var req = new RequestNotificacion();

                req.AfipSistemaId = (int)AfipSistemaId.PNRT;
                req.Asunto = requestGetNotificaciones.asunto.Substring(0, 1000);
                req.Cuit = requestGetNotificaciones.cuit;

                var resp = cliente.AddNotificacionAsync(req);
                Task.WaitAll(resp);

                var adjunto = new RequestAdjunto();
                adjunto.AfipSistemaId = (int)AfipSistemaId.PNRT;
                byte[] bytes = Convert.FromBase64String(requestGetNotificaciones.archivo);
                adjunto.Content = bytes;
                adjunto.Metadata = requestGetNotificaciones.metadata;
                adjunto.ContentType = "application/pdf";
                adjunto.NombreArchivo = requestGetNotificaciones.nombreDeArchivo;

                var esperandoFichero = await cliente.AddAdjuntoAsync(adjunto);

                var attachArchivo = await cliente.AttachAdjuntoAsync(resp.Result.Id, esperandoFichero.Id);

                if (attachArchivo != null)
                    return Ok(attachArchivo);
                else
                    return NotFound("Error al generar la notificacion.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de generacion de Notificaciones. El error es: " + ex.Message.ToString());
            }

        }

        [HttpPost("/PostNotificacionesSinAdjunto")]
        public async Task<IActionResult> postNotificacion([FromBody] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var req = new RequestNotificacion();

                req.AfipSistemaId = (int)AfipSistemaId.PNRT;
                req.Asunto = requestGetNotificaciones.asunto.Substring(0, 3);
                req.Cuit = requestGetNotificaciones.cuit;

                var resp = await cliente.AddNotificacionAsync(req);

                if (resp != null)
                    return Ok(resp);
                else
                    return NotFound("Error al generar la notificacion.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de generacion de Notificaciones. El error es: " + ex.Message.ToString());
            }

        }

        [HttpPost("/AttachAdjuntos")]
        public async Task<IActionResult> postAttachAdjunto([FromBody] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var attachArchivo = await cliente.AttachAdjuntoAsync(requestGetNotificaciones.idNotificaciones, requestGetNotificaciones.idAdjuntos);

                if (attachArchivo != null)
                    return Ok(attachArchivo);
                else
                    return NotFound("Error al adjuntar el archivo.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de adjuntar archivos a las Notificaciones. El error es: " + ex.Message.ToString());
            }


        }

        [HttpGet("/getNotificaciones")]
        public async Task<IActionResult> getNotificacion([FromQuery] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var result = await cliente.GetNotificacionAsync(requestGetNotificaciones.idNotificaciones);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Error al obtener la notificacion.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de Obtencion de Notificaciones. El error es: " + ex.Message.ToString());
            }

        }

        [HttpGet("/getDomicilioFiscal")]
        public async Task<IActionResult> getDomicilioFiscal([FromQuery] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var result = await cliente.GetContribuyenteAsync(requestGetNotificaciones.cuit);
                //TODO Mover y refactorizar estas validaciones
                if (result != null)
                {
                    if (result.AdheridoDFE)
                    {
                        result.ErrorMsg = null;
                    }
                    else
                    {
                        result.ErrorMsg = null;
                    }
                    return Ok(result);
                }
                else
                    return NotFound("Error al solicitar el domicilio fiscal.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de Obtencion de Notificaciones. El error es: " + ex.Message.ToString());

            }
        }
        
        [HttpGet("/validarCuit")]
        public async Task<IActionResult> getCuitValido([FromQuery] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);

                var cliente = WSAfipClient.GetClientV3(afipConnection.urlWSAfip, networkCredential);

                var result = await cliente.PadronA10_GetPersonaAsync(requestGetNotificaciones.cuit);


                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Error al solicitar el domicilio fiscal.");
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de Obtencion de Notificaciones. El error es: " + ex.Message.ToString());

            }
        }

        [HttpGet("/getArchivosAfip")]
        public async Task<IActionResult> getArchivosAfip([FromQuery] RequestNotificaciones requestGetNotificaciones)
        {
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(credentialNetworkAfip.user, credentialNetworkAfip.password, credentialNetworkAfip.dominio);
                var cliente = new VEAfipApiClient(afipConnection.urlVeAfip, networkCredential);

                var result = await cliente.GetAdjuntoAsync(requestGetNotificaciones.idAdjuntos);
                //TODO Mover y refactorizar estas validaciones
                if (result != null)
                {
                    dynamic metaData = JsonConvert.DeserializeObject(result.Metadata);
                    string fileName = metaData.FileName;

                    if (result.ErrorMsg != null || result.ErrorMsg != "")
                    {
                        return File(result.Content, "application/pdf", fileName);
                    }
                    else
                    {
                        return NotFound(result.ErrorMsg);
                    }
                }
                else
                {
                    return NotFound("No se pudo encontrar el archivo solitado.");
                }
            }
            catch (Exception ex)
            {
                return NotFound("Se genero un error al invocar el servicio de Obtencion de Archivos. El error es: " + ex.Message.ToString());

            }
        }

    }
}
