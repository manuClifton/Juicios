using Microsoft.Extensions.Hosting;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Globalization;
using VEJuicios.Application.Helpers;
using VEJuicios.Domain;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.GenerarConstanciaNotificacion
{
    public sealed class GenerarConstanciaNotificacionPDFUseCase : IGenerarConstanciaNotificacionPDFUseCase
    {
        private readonly IGenerarConstanciaNotificacionPDFOutputPort _GenerarConstanciaNotificacionOutputPort;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IVNotificacionConstanciaRepository _vistaNotificacionConstanciaRepository;

        public GenerarConstanciaNotificacionPDFUseCase(
            IGenerarConstanciaNotificacionPDFOutputPort GenerarNotificacionOutputPort, IHostEnvironment hostEnvironment, IVNotificacionConstanciaRepository vistaNotificacionConstanciaRepository )
        {
            this._GenerarConstanciaNotificacionOutputPort = GenerarNotificacionOutputPort;
            this._hostEnvironment = hostEnvironment;
            this._vistaNotificacionConstanciaRepository = vistaNotificacionConstanciaRepository;
        }

        public Task Execute(GenerarConstanciaNotificacionPDFInput input)
        {
            try
            {

                var consultaDB = this._vistaNotificacionConstanciaRepository.GetById(input.NotificacionId);
                if (consultaDB == null)
                {
                    throw new Exception("No se Encontro una Notificacion con el Id solicitado.");
                }
                #region creacion de documento

                //Creamos el documento
                PdfDocument doc = new();

                //Creamos la pagina
                PdfPage page = doc.AddPage();
                //Tamaño A4 = 595 x 842
                page.Size = PageSize.A4;

                var configuracionPDF = new PdfConfigurationNotificacion().Constancia(_hostEnvironment);

                #region crecion de variables

                int margenInferior = configuracionPDF.Margenes.Inferior;
                int margenSuperior = configuracionPDF.Margenes.Superior;
                int margenIzquierda = configuracionPDF.Margenes.Izquieda;
                int margenDerecha = configuracionPDF.Margenes.Derecha;

                int tamañoXLogo = configuracionPDF.Logo.TamañoX;
                int tamañoYLogo = configuracionPDF.Logo.TamañoY;

                int maximoInferior = (int)page.Height.Value - margenInferior;

                //determina la altura que estamos trabajando
                int currentY = margenSuperior;

                //determina cuanto vamos a darle de espaciado entre cada registro
                int interlineado = 15;

                int margenEncabezadoIzquierda = margenIzquierda;
                int margenIntermiedio = margenIzquierda + 70;
                int margenEncabezadoDerecha = (int)page.Width.Value - margenDerecha - margenIntermiedio;

                #region Estilos de letras y bordes
                XFont tituloEstilo = configuracionPDF.EstilosLetras.Titulo;
                XFont contenidoEstiloNegrita = configuracionPDF.EstilosLetras.ContenidoNegrita;
                XFont contenidoEstilo = configuracionPDF.EstilosLetras.Contenido;
                XFont tablaEstiloNegrita = configuracionPDF.EstilosLetras.TablaNegrita;
                XFont tablaEstilo = configuracionPDF.EstilosLetras.Tabla;
                XFont juzgadoEstiloNegrita = configuracionPDF.EstilosLetras.JuzgadoNegrita;
                XFont juzgadoEstilo = configuracionPDF.EstilosLetras.Juzgado;

                XPen bordeTabla = configuracionPDF.BordeTabla;

                #endregion

                #endregion

                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);
                XImage logo = XImage.FromFile(configuracionPDF.Logo.RutaLogo);

                #region añadimos al pdf los datos superiores

                //dibujamos la imagen
                gfx.DrawImage(logo, configuracionPDF.Logo.MargenIzquieda, configuracionPDF.Logo.MargenSuperior, tamañoXLogo, tamañoYLogo);

                tf.Alignment = XParagraphAlignment.Center;
                XRect margenesAux = new(190, configuracionPDF.Logo.MargenSuperior + 65, (int)page.Width.Value - 190 * 2, maximoInferior - currentY);
                tf.DrawString("Constancia Notificación Domicilio Fiscal Electrónico", tituloEstilo, XBrushes.Black, margenesAux);
                #endregion

                #region Datos

                #region Recudrado Superior
                currentY += interlineado * 3;

                int topMargen = currentY - 5;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Juzgado:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 55, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 55, maximoInferior - currentY);
                tf.DrawString(consultaDB.Juzgado != null ? consultaDB.Juzgado : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Secretaría:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 62, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 62, maximoInferior - currentY);
                tf.DrawString(consultaDB.Secretaria != null ? consultaDB.Secretaria : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Domicilio Juzgado:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 113, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 113, maximoInferior - currentY);
                tf.DrawString(consultaDB.JuzgadoDireccionCompleta != null ? consultaDB.JuzgadoDireccionCompleta : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Carátula:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 53, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 53, maximoInferior - currentY);
                tf.DrawString(consultaDB.Carpeta != null ? consultaDB.Carpeta : "", contenidoEstilo, XBrushes.Black, margenesAux);


                string textoAMedir = consultaDB.Carpeta;
                XSize availableSize = new XSize(page.Width.Value - margenIzquierda * 2 - margenDerecha - (margenIzquierda + 53), maximoInferior - currentY);
                int totalLines = 2;

                XSize measuredSize = gfx.MeasureString(textoAMedir, tablaEstilo);

                if (measuredSize.Width > availableSize.Width)
                {
                    var palabrasDivididas = textoAMedir.Split(" ");
                    string textoAux = palabrasDivididas[0];
                    for (int j = 1; j < palabrasDivididas.Count(); j++)
                    {
                        textoAux += " " + palabrasDivididas[j];
                        measuredSize = gfx.MeasureString(textoAux, tablaEstilo);
                        if (measuredSize.Width >= availableSize.Width)
                        {
                            totalLines++;
                            textoAux = palabrasDivididas[j];
                        }
                    }
                }

                currentY += interlineado * totalLines;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Expediente N°:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 87, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 87, maximoInferior - currentY);
                tf.DrawString(consultaDB.Expediente != null ? consultaDB.Expediente : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;
                margenesAux = new XRect(margenIzquierda - 5, topMargen, (int)page.Width.Value - margenIzquierda - margenDerecha + 10, currentY + 10 - topMargen);
                gfx.DrawRectangle(bordeTabla, margenesAux);
                topMargen = currentY + 10;
                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Apellido y Nombre / Razón Social:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 197, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 197, maximoInferior - currentY);
                tf.DrawString(consultaDB.PersonaNombre != null ? consultaDB.PersonaNombre : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("C.U.I.T. N°:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 65, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 65, maximoInferior - currentY);
                tf.DrawString(consultaDB.Cuit != null ? consultaDB.Cuit : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Domicilio Constituido:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 128, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 128, maximoInferior - currentY);
                tf.DrawString(consultaDB.PersonaDireccion != null ? consultaDB.PersonaDireccion : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;
                margenesAux = new XRect(margenIzquierda - 5, topMargen, (int)page.Width.Value - margenIzquierda - margenDerecha + 10, currentY + 10 - topMargen);
                gfx.DrawRectangle(bordeTabla, margenesAux);
                topMargen = currentY + 10;
                currentY += interlineado;

                #endregion

                #region Recudrado Inferior
                currentY += interlineado * 2;

                topMargen = currentY - 5;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Por medio de la presente en los autos citados se notifica lo siguiente:", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - (margenIzquierda + margenDerecha), maximoInferior - currentY);
                tf.DrawString(consultaDB.DescripcionMotivo != null ? consultaDB.DescripcionMotivo : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Fecha Publicación Ventanilla Electrónica:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 232, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 232, maximoInferior - currentY);
                tf.DrawString(consultaDB.FechaRecepcionAfip != null ? ((DateTime)consultaDB.FechaRecepcionAfip).ToString("dd/MM/yyyy") : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Fecha Notificación Fehaciente:", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda + 175, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha - 175, maximoInferior - currentY);
                tf.DrawString(consultaDB.FechaNotificacion != null ? ((DateTime)consultaDB.FechaNotificacion).ToString("dd/MM/yyyy") : "", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                margenesAux = new XRect(margenIzquierda - 5, topMargen, (int)page.Width.Value - margenIzquierda - margenDerecha + 10, currentY + 10 - topMargen);
                gfx.DrawRectangle(bordeTabla, margenesAux);

                #endregion
                #endregion
                #region Salvar Documento
                MemoryStream stream = new();
                doc.Save(stream, false);

                byte[] archivoFinal = stream.ToArray();
                var size = stream.Length;
                stream.Close();
                //Agregar Cedula a la tabla de NotificacionArchivoTemporal
                if (archivoFinal != null)
                {
                    string fileName = "Constancia";
                    string metadataStr = "{\"FileName\":\"" + fileName + "\", \"Type\":\"aplication/pdf\", \"Size\":" + size + "}";
                    bool generadoAutomatico = true;

                    //var archivo = new NotificacionArchivoTemporal(fileName, true, metadataStr, "application/pdf", archivoFinal, generadoAutomatico, DateTime.Now, DateTime.Now, input.UserId, input.UserId, consultaDB.TipoNotificacionCodigo);
                    //int archivoId = this._NotificacionArchivoTemporalRepository.AddArchivo(archivo);
                    var output = new GenerarConstanciaNotificacionPDFOutput(archivoFinal, 0, fileName + ".pdf");
                    this._GenerarConstanciaNotificacionOutputPort.Standard(output);
                }
                else
                {
                    throw new Exception("Archivo no generado");
                }
                #endregion
                #endregion 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }




            return Task.CompletedTask;

        }
    }
}