using Microsoft.Extensions.Hosting;
using PdfSharp.Drawing.Layout;
using VEJuicios.Application.Helpers;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ
{
    public sealed class GenerarPDFEmbargoCuentaSOJUseCase : IGenerarPDFEmbargoCuentaSOJUseCase
    {
        private readonly IGenerarPDFEmbargoCuentaSOJOutputPort _GenerarPDFEmbargoCuentaSOJOutputPort;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IvNotificacionPDFEmbargoCuentaSOJRepository _vNotificacionPDFEmbargoCuentaSOJRepository;
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;
        public GenerarPDFEmbargoCuentaSOJUseCase(
            IGenerarPDFEmbargoCuentaSOJOutputPort GenerarPDFEmbargoCuentaSOJOutputPort,
            IHostEnvironment hostEnvironment,
            IvNotificacionPDFEmbargoCuentaSOJRepository vNotificacionPDFEmbargoCuentaSOJRepository,
            INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository)
        {
            this._GenerarPDFEmbargoCuentaSOJOutputPort = GenerarPDFEmbargoCuentaSOJOutputPort;
            this._hostEnvironment = hostEnvironment;
            this._vNotificacionPDFEmbargoCuentaSOJRepository = vNotificacionPDFEmbargoCuentaSOJRepository;
            this._NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
        }
        public Task Execute(GenerarPDFEmbargoCuentaSOJInput input)
        {
            try
            {
                var consultaDB = this._vNotificacionPDFEmbargoCuentaSOJRepository.GetById(input.NotificacionId) ?? throw new Exception("No se encontró una Notificación con el Id solicitado.");
                var datos = new NotificacionPdfHelper().ObtenerDatosPdfEmbargoCuentaSoj(input, consultaDB) ?? throw new Exception("Error al generar los datos.");
                FormatoDocumentoPdf pdf = new(_hostEnvironment);

                #region Títulos
                pdf.ContextoGrafico.DrawImage(pdf.ImagenLogo, pdf.ConfiguracionPdf.Logo.MargenIzquieda, pdf.ConfiguracionPdf.Logo.MargenSuperior, pdf.TamañoXLogo, pdf.TamañoYLogo);

                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Center;
                pdf.Escribir("STEySS", pdf.EstiloTitulo, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir("CÉDULA DE NOTIFICACIÓN", pdf.EstiloTitulo, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2 + 6);
                #endregion Títulos

                #region Datos Superiores
                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Justify;
                pdf.Escribir("TRIBUNAL:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir(datos.Juzgado.Descripcion, pdf.EstiloJuzgadoNegrita, pdf.MargenIzquierdoAmpliado, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir("SECRETARIA:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir(datos.Secretaria.Descripcion, pdf.EstiloJuzgadoNegrita, pdf.MargenIzquierdoAmpliado, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir("DOMICILIO DEL JUZGADO/TRIBUNAL:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir(datos.Juzgado.Direccion + ". " + datos.Juzgado.Ciudad, pdf.EstiloJuzgadoNegrita, pdf.MargenIzquierdoAmpliado + 100, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);

                pdf.Escribir("SEÑOR/ES:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir(datos.Parte.Nombre, pdf.EstiloContenidoNegrita, pdf.MargenIzquierdoAmpliado, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir("DOMICILIO:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir(datos.Parte.Direccion + ". " + datos.Parte.Ciudad, pdf.EstiloContenidoNegrita, pdf.MargenIzquierdoAmpliado, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir("CARACTER:", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX);

                pdf.Escribir("CONSTITUIDO", pdf.EstiloContenidoNegrita, pdf.MargenIzquierdoAmpliado, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);
                #endregion Datos Superiores

                #region Tabla
                List<int> columnasTablaLargo = new()
                {
                    60,
                    60,
                    90,
                    120,
                    120,
                    42
                };
                List<List<string>> filasTabla = new()
                {
                    new List<string>
                {
                    "N° ORDEN",
                    "EXP. N°",
                    "FUERO",
                    "JUZGADO",
                    "SECRETARÍA",
                    "COPIAS"
                },
                    new List<string>
                {
                    "1",
                    datos.Caso.ExpedienteJudicial,
                    datos.Fuero.Descripcion,
                    datos.Juzgado.Descripcion,
                    datos.Secretaria.Descripcion + " " + datos.Juzgado.Juez,
                    datos.PopUp.CopiasSiNo
                }
                };
                pdf.DibujarTabla(columnasTablaLargo, filasTabla, pdf.SaltoDeLinea);
                #endregion Tabla

                #region Párrafo
                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Justify;
                pdf.EscribirParrafo(datos.ContenidoParrafo, pdf.EstiloContenido, pdf.EstiloContenidoNegrita, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);
                #endregion'Párrafo

                #region Tabla Embargos
                columnasTablaLargo = new List<int>
                {
                    96,
                    90,
                    120,
                    60,
                    60,
                    66
                };
                filasTabla = new List<List<string>>
                {
                    new List<string>
                    {
                    "Banco",
                    "Tipo Cuenta",
                    "Nro. Cuenta",
                    "Importe Retenido",
                    "Fecha Retención",
                    "Monto Informado"
                    }
                };
                foreach (var cuenta in datos.CuentasEmbargadas)
                {
                    filasTabla.Add(new List<string>
                    {
                        cuenta.BancoAFIPDescripcion,
                        cuenta.BancoTipoCuentaDescripcion,
                        cuenta.NumeroCuenta,
                        cuenta.ImporteRetenido.ToString(),
                        cuenta.FechaRetencion.ToString(),
                        cuenta.MontoInformado.ToString()
                    });
                };
                pdf.DibujarTabla(columnasTablaLargo, filasTabla, pdf.SaltoDeLinea);
                #endregion Tabla Embargos

                #region Datos inferiores
                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Left;
                pdf.Escribir("QUEDA UD. NOTIFICADO.", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);

                pdf.Escribir(datos.OficinaYFecha, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);

                pdf.Escribir(datos.Dependencia, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);

                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Right;
                pdf.Escribir(datos.Abogado.Nombre, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir(datos.Abogado.Matricula, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea * 2);

                pdf.FormateadorTexto.Alignment = XParagraphAlignment.Justify;
                pdf.Escribir("DATOS DE REFERENCIA", pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir(datos.Abogado.Email, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir(datos.Abogado.Telefono, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);

                pdf.Escribir(datos.Abogado.Celular, pdf.EstiloContenido, pdf.MargenIzquierdo, pdf.EspacioEntreMargenesX, pdf.SaltoDeLinea);
                #endregion Datos Inferiores

                #region Crear Archivo Pdf
                using MemoryStream stream = new();
                pdf.Documento.Save(stream, false);
                byte[] archivoFinal = stream.ToArray();
                if (archivoFinal != null && archivoFinal.Length > 0)
                {
                    string nombreArchivo = consultaDB[0].TipoNotificacionCodigo ?? "";
                    string metadata = "{\"FileName\":\"" + nombreArchivo + "\", \"Type\":\"application/pdf\", \"Size\":" + stream.Length + "}";
                    bool generadoAutomatico = true;

                    var archivo = new NotificacionArchivoTemporal(nombreArchivo, true, metadata, "application/pdf", archivoFinal, generadoAutomatico, DateTime.Now, DateTime.Now, input.UserId, input.UserId, nombreArchivo);

                    int archivoId = this._NotificacionArchivoTemporalRepository.AddArchivo(archivo);
                    var output = new GenerarPDFEmbargoCuentaSOJOutPut(archivoFinal, archivoId, nombreArchivo + ".pdf");
                    this._GenerarPDFEmbargoCuentaSOJOutputPort.Standard(output);
                }
                else
                {
                    throw new Exception("Archivo no generado");
                }
                #endregion Crear Archivo Pdf
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Task.CompletedTask;
        }
    }
}