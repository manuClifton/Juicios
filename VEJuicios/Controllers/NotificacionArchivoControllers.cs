using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.GenerarConstanciaNotificacion;
using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ;
using VEJuicios.Application.UseCases.NotificacionAlta;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
using VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones;
using VEJuicios.Application.UseCases.NotificacionDescargarAdjuntoEnviado;
using VEJuicios.Application.UseCases.NotificacionEliminarArchivo;
using VEJuicios.Application.UseCases.NotificacionEliminarRelacionArchivos;
using VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos;
using VEJuicios.Application.UseCases.NotificacionGrabarRelacionArchivos;
using VEJuicios.Application.UseCases.NotificacionVerificarMegas;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Model;
using VEJuicios.Presenters;

namespace VEJuicios.Controllers
{
    [ApiController]
    [Route("NotificacionArchivo")]
    [EnableCors]
    public class NotificacionArchivoController : ControllerBase
    {
        private readonly ILogger<NotificacionArchivoController> _logger;

        private readonly IGenerarNotificacionPDFUseCase _GenerarNotificacionUseCase;
        private readonly GenerarNotificacionPresenter _GenerarNotificacionPresenter;
        
        private readonly IGenerarPDFEmbargoCuentaSOJUseCase _GenerarPDFEmbargoCuentaSOJUseCase;
        private readonly GenerarPDFEmbargoCuentaSOJPresenter _GenerarPDFEmbargoCuentaSOJPresenter;
        
        private readonly IGenerarConstanciaNotificacionPDFUseCase _GenerarConstanciaNotificacionPDFUseCase;
        private readonly GenerarConstanciaPresenter _GenerarConstanciaPresenter;
        
        private readonly INotificacionEliminarArchivoUseCase _EliminarArchivoUseCase;

        private readonly INotificacionAñadirAdjuntoUseCase _IAñadirAdjuntoUseCase;
        private readonly NotificacionAñadirArchivoPresenter _AñadirArchivoPresenter;

        private readonly INotificacionGrabarRelacionArchivosUseCase _NotificacionGrabarArchivosMultiplesUseCase;
        private readonly INotificacionEliminarRelacionArchivosUseCase _NotificacionEliminarArchivosMultiplesUseCase;
        private readonly INotificacionVerificarMegasUseCase _NotificacionVerificarMegas;
        private readonly VNotificacionVerificarMegasPresenter _VNotificacionVerificarMegasPresenter;

        private readonly INotificacionConfirmarNotificacionesUseCase _NotificacionConfirmarNotificacionesUseCase;
        private readonly NotificacionConfirmarNotificacionesPresenter _ConfirmarNotificacionesPresenter;

        private readonly INotificacionGenerarCedulaValidarDatosUseCase _NotificacionGenerarCedulaValidarDatosUseCase;
        private readonly NotificacionGenerarCedulaValidarDatosPresenter _NotificacionGenerarCedulaValidarDatosPresenter;

        private readonly INotificacionAltaUseCase _NotificacionAltaUseCase;
        private readonly NotificacionAltaPresenter _NotificacionAltaPresenter;

        private readonly INotificacionDescargarAdjuntoEnviadoUseCase _NotificacionDescargarAdjuntoEnviadoUseCase;
        private readonly NotificacionDescargarAdjuntoEnviadoPresenter _NotificacionDescargarAdjuntoEnviadoPresenter;

        private readonly IvNotificacionesAConfirmarRepository _vNotificacionesAConfirmarRepository;

        public NotificacionArchivoController(
            ILogger<NotificacionArchivoController> logger,

            IGenerarNotificacionPDFUseCase generarNotificacionUseCase,
            GenerarNotificacionPresenter generarNotificacionPresenter,

            IGenerarPDFEmbargoCuentaSOJUseCase generarPDFEmbargoCuentaSOJUseCase,
            GenerarPDFEmbargoCuentaSOJPresenter generarPDFEmbargoCuentaSOJPresenter,

            IGenerarConstanciaNotificacionPDFUseCase generarConstanciaNotificacionPDFUseCase,
            GenerarConstanciaPresenter generarConstanciaPresenter,

            INotificacionEliminarArchivoUseCase eliminarArchivoUseCase,

            INotificacionAñadirAdjuntoUseCase añadirArchivoUseCase,
            NotificacionAñadirArchivoPresenter añadirArchivoPresenter,

            INotificacionGrabarRelacionArchivosUseCase notificacionGrabarArchivosMultiplesUseCase,
            INotificacionEliminarRelacionArchivosUseCase notificacionEliminarArchivosMultiplesUseCase,

            INotificacionVerificarMegasUseCase notificacionVerificarMegas,
            VNotificacionVerificarMegasPresenter vNotificacionVerificarMegasPresenter,

            INotificacionConfirmarNotificacionesUseCase notificacionConfirmarNotificacionesUseCase,
            NotificacionConfirmarNotificacionesPresenter notificacionConfirmarNotificacionesPresenter,

            INotificacionGenerarCedulaValidarDatosUseCase notificacionGenerarCedulaValidarDatosUseCase,
            NotificacionGenerarCedulaValidarDatosPresenter notificacionGenerarCedulaValidarDatosPresenter,

            INotificacionAltaUseCase notificacionAltaUseCase,
            NotificacionAltaPresenter notificacionAltaPresenter,

            INotificacionDescargarAdjuntoEnviadoUseCase notificacionDescargarAdjuntoEnviadoUseCase,
            NotificacionDescargarAdjuntoEnviadoPresenter notificacionDescargarAdjuntoEnviadoPresenter,

            IvNotificacionesAConfirmarRepository vNotificacionesAConfirmarRepository
            )
        {
            _logger = logger;

            _GenerarNotificacionUseCase = generarNotificacionUseCase;
            _GenerarPDFEmbargoCuentaSOJUseCase = generarPDFEmbargoCuentaSOJUseCase;
            _GenerarNotificacionPresenter = generarNotificacionPresenter;
            _GenerarPDFEmbargoCuentaSOJPresenter = generarPDFEmbargoCuentaSOJPresenter;

            _GenerarConstanciaNotificacionPDFUseCase = generarConstanciaNotificacionPDFUseCase;
            _GenerarConstanciaPresenter = generarConstanciaPresenter;

            _EliminarArchivoUseCase = eliminarArchivoUseCase;

            _IAñadirAdjuntoUseCase = añadirArchivoUseCase;
            _AñadirArchivoPresenter = añadirArchivoPresenter;

            _NotificacionGrabarArchivosMultiplesUseCase = notificacionGrabarArchivosMultiplesUseCase;
            _NotificacionEliminarArchivosMultiplesUseCase = notificacionEliminarArchivosMultiplesUseCase;

            _NotificacionVerificarMegas = notificacionVerificarMegas;
            _VNotificacionVerificarMegasPresenter = vNotificacionVerificarMegasPresenter;

            _NotificacionConfirmarNotificacionesUseCase = notificacionConfirmarNotificacionesUseCase;
            _ConfirmarNotificacionesPresenter = notificacionConfirmarNotificacionesPresenter;

            _NotificacionGenerarCedulaValidarDatosUseCase = notificacionGenerarCedulaValidarDatosUseCase;
            _NotificacionGenerarCedulaValidarDatosPresenter = notificacionGenerarCedulaValidarDatosPresenter;

            _NotificacionAltaUseCase = notificacionAltaUseCase;
            _NotificacionAltaPresenter = notificacionAltaPresenter;

            _NotificacionDescargarAdjuntoEnviadoUseCase = notificacionDescargarAdjuntoEnviadoUseCase;
            _NotificacionDescargarAdjuntoEnviadoPresenter = notificacionDescargarAdjuntoEnviadoPresenter;

            _vNotificacionesAConfirmarRepository = vNotificacionesAConfirmarRepository;
        }

        [HttpPost("/NotificacionDescargarPDF")]
        public async Task<IActionResult> GetPdfNotificacion(int notificacionId, string fechaAsociada, string popUpFecha, string CopiasSiNo, int userId, bool file = false)
        {
            try
            {
                var useCaseInput = new GenerarNotificacionPDFInput(notificacionId, fechaAsociada, popUpFecha, CopiasSiNo, userId);
                await this._GenerarNotificacionUseCase.Execute(useCaseInput);
                var models = _GenerarNotificacionPresenter.ViewModel;

                if (file)
                {
                    return File(models.File, "application/pdf", models.FileName);
                }

                var objeto = new
                {
                    ArchivoId = models.ArchivoId,
                    FileName = models.FileName
                };

                var errorResponse = new { message = "No se pudo generar archivo PDF." };

                if (models != null)
                    return new ObjectResult(objeto);
                else
                    return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound("Se generó un error al invocar el servicio: " + ex.Message.ToString());
            }

        }

        [HttpPost("/NotificacionDescargarPDFEmbargoCuentaSoj")]
        public async Task<IActionResult> GetPdfNotificacionEmbargoCuentaSoj(int notificacionId, string fechaAsociada, string popUpFecha, string CopiasSiNo, int userId, bool file = false)
        {
            try
            {
                var useCaseInput = new GenerarPDFEmbargoCuentaSOJInput(notificacionId, fechaAsociada, popUpFecha, CopiasSiNo, userId);
                await this._GenerarPDFEmbargoCuentaSOJUseCase.Execute(useCaseInput);
                var models = _GenerarPDFEmbargoCuentaSOJPresenter.ViewModel;

                if (file)
                {
                    return File(models.File, "application/pdf", models.FileName);
                }

                var objeto = new
                {
                    ArchivoId = models.ArchivoId,
                    FileName = models.FileName
                };

                var errorResponse = new { message = "No se pudo generar archivo PDF." };

                if (models != null)
                    return new ObjectResult(objeto);
                else
                    return NotFound(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound("Se generó un error al invocar el servicio: " + ex.Message.ToString());
            }

        }

        [HttpPost("/NotificacionContanciaPDF")]
        public async Task<IActionResult> GetPdfConstanciaNotificacion(int notificacionId)
        {
            try
            {
                var useCaseInput = new GenerarConstanciaNotificacionPDFInput(notificacionId);
                await this._GenerarConstanciaNotificacionPDFUseCase.Execute(useCaseInput);
                var models = _GenerarConstanciaPresenter.ViewModel;

                if (models == null)
                {
                    var errorResponse = new { message = "No se pudo generar archivo PDF." };
                    return BadRequest(errorResponse);
                }

                // Devuelve un objeto FileContentResult con el archivo PDF para descargar
                return File(models.File, "application/pdf", "Constancia.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound("Se generó un error al invocar el servicio: " + ex.Message.ToString());
            }
        }

        [HttpPost("/NotificacionAniadirArchivoAdjunto")]
        public async Task<IActionResult> AniadirArchivoAdjuntoNotificacion([FromForm] NotificacionArchivoAdjuntoModel model)
        {
            if(model.Archivo.ContentType != "application/pdf")
            {
                return NotFound("El archivo debe ser '.pdf'");
            }

            // Aquí puedes acceder a los datos del archivo y los parámetros
            var myfile = model.Archivo;
            var userId = model.UserId;
            var cedula = model.Cedula;
            var descripcion = model.Descripcion;
            var metadata = model.MetaData;
            bool generadoAutomatico = false;

            try
            {

                if (myfile == null || myfile.Length == 0)
                {
                    return BadRequest("No se ha proporcionado ningún archivo.");
                }
                else if (userId == 0)
                {
                    return BadRequest("No se ha proporcionado ningún userId.");
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    myfile.CopyTo(memoryStream);
                    await this._IAñadirAdjuntoUseCase.Execute(new NotificacionAñadirAdjuntoInput(userId,
                        new NotificacionArchivoTemporal(myfile.FileName, cedula, metadata, myfile.ContentType, memoryStream.ToArray(),
                        generadoAutomatico, DateTime.Now, DateTime.Now, userId, userId, descripcion)));
                }
                var models = _AñadirArchivoPresenter.ViewModel;

                return new JsonResult(new
                {
                    id = models.ArchivoId,
                    nombreArchivo = myfile.FileName
                });



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound("Se genero un error al invocar el servicio: " + ex.Message.ToString());
            }

        }

        [HttpPost("/NotificacionGrabaYEliminarRelacionArchivo")]
        public async Task<ActionResult> GrabarYEliminarRelacionArchivosNotificacion([FromForm] NotificacionGrabarYEliminarRelacionModel model)
        {
            try
            {
                //realizar funcion para verificar carga menor a 8mb
                var useCaseInputVerificar = new NotificacionVerificarMegasInput(model.GuardarArchivosID, model.EliminarArchivosID, model.NotificacionID, model.CedulaID);


                await this._NotificacionVerificarMegas.Execute(useCaseInputVerificar);
                var respuestaVerificacion = _VNotificacionVerificarMegasPresenter.res;
                if (respuestaVerificacion)
                {
                    var useCaseInputGrabar = new NotificacionGrabarRelacionArchivosInput(model.GuardarArchivosID, model.NotificacionID, model.CedulaID);
                    await this._NotificacionGrabarArchivosMultiplesUseCase.Execute(useCaseInputGrabar);

                    var useCaseInputEliminar = new NotificacionEliminarRelacionArchivosInput(model.EliminarArchivosID, model.NotificacionID, model.CedulaID);
                    await this._NotificacionEliminarArchivosMultiplesUseCase.Execute(useCaseInputEliminar);
                }
                else
                {
                    return BadRequest("No se puede guardar la notificación. El tamaño total de los archivos adjuntos no puede superar los 8 megabytes. Por favor, reduzca el tamaño de los archivos adjuntos e intente nuevamente.");
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.ToString());
            }

            return Ok();
        }

        [HttpPost("/NotificacionGrabarYConfirmar")]
        public async Task<ActionResult> NotificacionGrabarYConfirmar([FromForm] NotificacionGrabarYEliminarRelacionModel model)
        {
            try
            {
                string mensajeDeError;
                bool notificacionCorrecta = this._vNotificacionesAConfirmarRepository.GetCorrectoParaGrabarYConfirmar(model.NotificacionID, model.RelacionadosYARelacionarID, model.TipoNotificacion, model.CedulaID);

                if (!notificacionCorrecta)
                {
                    return BadRequest("La notificación no se puede confirmar pues le falta una cédula o un adjunto.");
                }

                //realizar funcion para verificar carga menor a 8mb
                var useCaseInputVerificar = new NotificacionVerificarMegasInput(model.GuardarArchivosID, model.EliminarArchivosID, model.NotificacionID, model.CedulaID);


                await this._NotificacionVerificarMegas.Execute(useCaseInputVerificar);
                var respuestaVerificacion = _VNotificacionVerificarMegasPresenter.res;


                if (respuestaVerificacion)
                {
                    var useCaseInputGrabar = new NotificacionGrabarRelacionArchivosInput(model.GuardarArchivosID, model.NotificacionID, model.CedulaID);
                    await this._NotificacionGrabarArchivosMultiplesUseCase.Execute(useCaseInputGrabar);

                    var useCaseInputEliminar = new NotificacionEliminarRelacionArchivosInput(model.EliminarArchivosID, model.NotificacionID, model.CedulaID);
                    await this._NotificacionEliminarArchivosMultiplesUseCase.Execute(useCaseInputEliminar);
                }
                else
                {
                    return BadRequest("No se puede guardar la notificación. El tamaño total de los archivos adjuntos no puede superar los 8 megabytes. Por favor, reduzca el tamaño de los archivos adjuntos e intente nuevamente.");
                }

                int[] notificacionID = { model.NotificacionID };
                var useCaseInput = new NotificacionConfirmarNotificacionesInput(notificacionID, model.UserId);
                await this._NotificacionConfirmarNotificacionesUseCase.Execute(useCaseInput);
                mensajeDeError = _ConfirmarNotificacionesPresenter.MensajeDeError;

                if (mensajeDeError != null)
                {
                    return NotFound(mensajeDeError);
                }
                else
                {
                    mensajeDeError = "Los cambios fueron efectuados.";
                    return Ok(mensajeDeError);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.ToString());
            }
        }

        [HttpDelete("/NotificacionEliminarArchivo")]
        public async Task<ActionResult> EliminarArchivo(int[] archivosId)
        {
            try
            {
                var useCaseInput = new NotificacionEliminarArchivoInput(archivosId);
                await this._EliminarArchivoUseCase.Execute(useCaseInput);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.ToString());
            }
            return Ok();
        }

        [HttpPost("/ConfirmarNotificaciones")]
        public async Task<ActionResult> ConfirmarNotificaciones([FromForm] NotificacionConfirmarModel model)
        {
            string mensajeDeError;

            try
            {
                var useCaseInput = new NotificacionConfirmarNotificacionesInput(model.NotificacionesId, model.UserId);
                await this._NotificacionConfirmarNotificacionesUseCase.Execute(useCaseInput);
                mensajeDeError = _ConfirmarNotificacionesPresenter.MensajeDeError;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.ToString());
            }

            if (mensajeDeError != null)
            {
                return NotFound(mensajeDeError);
            }
            else
            {
                mensajeDeError = "Los cambios fueron efectuados.";
                return Ok(mensajeDeError);
            }
        }

        [HttpGet("/NotificacionGenerarCedulaValidarDatos")]
        public async Task<ActionResult> NotificacionGenerarCedulaValidarDatos(int NotificacionId)
        {
            string mensajeDeError;
            try
            {
                var useCaseInput = new NotificacionGenerarCedulaValidarDatosInput(NotificacionId);
                await this._NotificacionGenerarCedulaValidarDatosUseCase.Execute(useCaseInput);
                mensajeDeError = _ConfirmarNotificacionesPresenter.MensajeDeError;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message.ToString());
            }
            return Ok();
        }

        [HttpPost("/NotificacionAlta")]
        public async Task<ActionResult> NotificacionAlta([FromForm] NotificacionAltaInput model)
        {
            int notificacionId;
            try
            {
                await this._NotificacionAltaUseCase.Execute(model);
                notificacionId = _NotificacionAltaPresenter.NotificacionId;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message.ToString());
            }
            return Ok(notificacionId);
        }

        [HttpGet("/NotificacionDescargarAdjuntoEnviado")]
        public async Task<ActionResult> NotificacionDescargarAdjuntoEnviado([FromQuery] NotificacionDescargarAdjuntoEnviadoInput model)
        {
            try
            {
                await this._NotificacionDescargarAdjuntoEnviadoUseCase.Execute(model);
                var models = _NotificacionDescargarAdjuntoEnviadoPresenter.ViewModel;

                if (models == null || models.File == null )
                {
                    var errorResponse = new { message = "No se pudo descargar archivo PDF." };
                    return BadRequest(errorResponse);
                }

                // Devuelve un objeto FileContentResult con el archivo PDF para descargar
                return File(models.File, "application/pdf", models.FileName);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound("Se generó un error al invocar el servicio: " + e.Message.ToString());
            }
        }
    }
}
