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

namespace VEJuicios.Application.UseCases.GenerarNotificacion
{
    public sealed class GenerarNotificacionPDFUseCase : IGenerarNotificacionPDFUseCase
    {
        private readonly IGenerarNotificacionPDFOutputPort _GenerarNotificacionOutputPort;
        private readonly INotificacionRepository _NotificacionRepository;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IVistaNotificacionPDFRepository _VistaNotificacionPDFRepository;
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;

        public GenerarNotificacionPDFUseCase(
            IGenerarNotificacionPDFOutputPort GenerarNotificacionOutputPort,
            INotificacionRepository notificacionRepository,
            IHostEnvironment hostEnvironment,
            IVistaNotificacionPDFRepository vistaNotificacionPDFRepository,
            INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository
            )
        {
            this._GenerarNotificacionOutputPort = GenerarNotificacionOutputPort;
            this._NotificacionRepository = notificacionRepository;
            this._hostEnvironment = hostEnvironment;
            this._VistaNotificacionPDFRepository = vistaNotificacionPDFRepository;
            this._NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
        }

        public Task Execute(GenerarNotificacionPDFInput input)
        {
            try
            {
                var consultaDB = this._VistaNotificacionPDFRepository.GetById(input.NotificacionId);
                if (consultaDB == null)
                {
                    throw new Exception("No se Encontro una Notificacion con el Id solicitado.");
                }

                var datos = new NotificacionPdfHelper().ObtenerDatosPdfGenerico(input, consultaDB);
                if (datos == null)
                {
                    throw new Exception("Error al generar los datos.");
                }
                #region creacion de documento

                //Creamos el documento
                PdfDocument doc = new();

                //Creamos la pagina
                PdfPage page = doc.AddPage();
                //Tamaño A4 = 595 x 842
                page.Size = PageSize.A4;

                var configuracionPDF = new PdfConfigurationNotificacion().NotificacionGenerica(_hostEnvironment);

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
                int interlineado = 13;

                int margenEncabezadoIzquierda = margenIzquierda;
                int margenIntermiedio = margenIzquierda + 55;
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
                #endregion

                #region encabezado

                tf.Alignment = XParagraphAlignment.Center;

                //TITULO
                //creamos el margen tenemos que pasarle su poscion x e y, y despues el tamaño que van a tener 
                XRect margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("STEySS", tituloEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Center;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("CÉDULA DE NOTIFICACIÓN", tituloEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2 + 6;

                //TRIBUNAL
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio - margenEncabezadoIzquierda, maximoInferior - currentY);
                tf.DrawString("TRIBUNAL:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio, currentY, page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Juzgado.Descripcion, juzgadoEstiloNegrita, XBrushes.Black, margenesAux);

                string textoAMedir = datos.Juzgado.Descripcion;
                XSize availableSize = new XSize(page.Width.Value - margenIntermiedio * 2 - margenDerecha, maximoInferior - currentY);
                int totalLines = 1;

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

                //SECRETARIA
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio - margenEncabezadoIzquierda, maximoInferior - currentY);
                tf.DrawString("SECRETARIA:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio, currentY, page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Secretaria.Descripcion, juzgadoEstiloNegrita, XBrushes.Black, margenesAux);

                textoAMedir = datos.Secretaria.Descripcion;
                availableSize = new XSize(page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                totalLines = 1;

                measuredSize = gfx.MeasureString(textoAMedir, tablaEstilo);

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

                //DOMICILIO
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio + 100, maximoInferior - currentY);
                tf.DrawString("DOMICILIO DEL JUZGADO/TRIBUNAL:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio + 100, currentY, page.Width.Value - (margenIntermiedio + 100), maximoInferior - currentY);
                tf.DrawString(datos.Juzgado.Direccion + ". " + datos.Juzgado.Ciudad, juzgadoEstiloNegrita, XBrushes.Black, margenesAux);

                textoAMedir = datos.Juzgado.Direccion;
                availableSize = new XSize(page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                totalLines = 1;

                measuredSize = gfx.MeasureString(textoAMedir, tablaEstilo);

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

                currentY += (interlineado * totalLines) + interlineado;

                //SEÑOR/ES
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio - margenEncabezadoIzquierda, maximoInferior - currentY);
                tf.DrawString("SEÑOR/ES:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio, currentY, page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Parte.Nombre, contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                textoAMedir = datos.Parte.Nombre;
                availableSize = new XSize(page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                totalLines = 1;
                measuredSize = gfx.MeasureString(textoAMedir, tablaEstilo);

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

                //DOMICILIO
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio - margenEncabezadoIzquierda, maximoInferior - currentY);
                tf.DrawString("DOMICILIO:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio, currentY, page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Parte.Direccion + ". " + datos.Parte.Ciudad, contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                textoAMedir = datos.Parte.Direccion;
                availableSize = new XSize(page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                totalLines = 1;
                measuredSize = gfx.MeasureString(textoAMedir, tablaEstilo);

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
                currentY += (interlineado * totalLines);

                //CARACTER
                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenEncabezadoIzquierda, currentY, margenIntermiedio - margenEncabezadoIzquierda, maximoInferior - currentY);
                tf.DrawString("CARACTER:", contenidoEstilo, XBrushes.Black, margenesAux);

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIntermiedio, currentY, page.Width.Value - margenIntermiedio - margenDerecha, maximoInferior - currentY);
                tf.DrawString("CONSTITUIDO", contenidoEstiloNegrita, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;
                #endregion

                #region Contenido

                #region Tabla



                //Tamaño Oficio 612x 1008
                #region Variables
                List<int> columnasTamaño = new List<int>
                {
                    margenIzquierda
                };

                columnasTamaño.Add(columnasTamaño[^1] + 60);
                columnasTamaño.Add(columnasTamaño[^1] + 60);
                columnasTamaño.Add(columnasTamaño[^1] + 90);
                columnasTamaño.Add(columnasTamaño[^1] + 120);
                columnasTamaño.Add(columnasTamaño[^1] + 120);
                columnasTamaño.Add(columnasTamaño[^1] + 42);

                List<string> columnasTexto = new List<string>
                {
                    "N° ORDEN",
                    "EXP. N°",
                    "FUERO",
                    "JUZGADO",
                    "SECRETARÍA",
                    "COPIAS"
                };

                List<string> columnasDato = new List<string>
            {
                "1",
                datos.Caso.ExpedienteJudicial,
                datos.Fuero.Descripcion,
                datos.Juzgado.Descripcion,
                datos.Secretaria.Descripcion + " " + datos.Juzgado.Juez,
                datos.PopUp.CopiasSiNo
            };

                int correcionMovimentoColumnas = 2;
                #endregion

                for (int i = 0; i < columnasTexto.Count; i++)
                {
                    tf.Alignment = XParagraphAlignment.Center;
                    margenesAux = new XRect(columnasTamaño[i] + correcionMovimentoColumnas, currentY, columnasTamaño[i + 1] - columnasTamaño[i] - correcionMovimentoColumnas * 2, maximoInferior - currentY);
                    tf.DrawString(columnasTexto[i], tablaEstiloNegrita, XBrushes.Black, margenesAux);

                    margenesAux = new XRect(columnasTamaño[i], currentY, columnasTamaño[i + 1] - columnasTamaño[i], interlineado * 2);
                    gfx.DrawRectangle(bordeTabla, margenesAux);
                }
                currentY += interlineado * 2;
                int maxWidthTable = 0;
                for (int i = 0; i < columnasDato.Count; i++)
                {
                    tf.Alignment = XParagraphAlignment.Justify;
                    margenesAux = new XRect(columnasTamaño[i] + correcionMovimentoColumnas, currentY, columnasTamaño[i + 1] - columnasTamaño[i] - correcionMovimentoColumnas * 2, maximoInferior - currentY);
                    tf.DrawString(columnasDato[i], tablaEstilo, XBrushes.Black, margenesAux);

                    string texto = columnasDato[i];
                    availableSize = new XSize(columnasTamaño[i + 1] - columnasTamaño[i] - correcionMovimentoColumnas * 2, page.Height);
                    totalLines = 1;


                    measuredSize = gfx.MeasureString(texto, tablaEstilo);

                    if (measuredSize.Width > availableSize.Width)
                    {
                        var palabrasDivididas = texto.Split(" ");
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


                    if (maxWidthTable < totalLines)
                    {
                        maxWidthTable = totalLines;
                    }
                }

                for (int i = 0; i < columnasDato.Count; i++)
                {
                    margenesAux = new XRect(columnasTamaño[i], currentY, columnasTamaño[i + 1] - columnasTamaño[i], interlineado * maxWidthTable);
                    gfx.DrawRectangle(bordeTabla, margenesAux);
                }
                currentY += interlineado * maxWidthTable;

                #endregion

                currentY += interlineado * 2;

                double x = margenIzquierda;
                int margenMaxDerecha = (int)page.Width - margenDerecha;

                bool tipoletraNegrita = false;
                XFont parrafoLetra;
                List<Tuple<string, XFont, int>> datosParrafo = new List<Tuple<string, XFont, int>>();
                double tamañoFila = margenIzquierda;
                int distancia = 5;
                int resto = 0;
                bool verificador = false;
                foreach (string frase in datos.ContenidoParrafo)
                {
                    if (tipoletraNegrita)
                    {
                        tipoletraNegrita = false;
                        parrafoLetra = contenidoEstiloNegrita;
                    }
                    else
                    {
                        tipoletraNegrita = true;
                        parrafoLetra = contenidoEstilo;
                    }

                    string[] palabras = frase.Split(' ');

                    foreach (string palabra in palabras)
                    {
                        XSize textSize = gfx.MeasureString(palabra, parrafoLetra);

                        if (x + textSize.Width > margenMaxDerecha)
                        {

                            while (verificador == false)
                            {
                                if (tamañoFila + distancia * (datosParrafo.Count - 1) > margenMaxDerecha)
                                {
                                    resto = (margenMaxDerecha - (int)tamañoFila) / distancia;
                                    verificador = true;
                                }
                                else if (margenMaxDerecha - 1 <= (int)tamañoFila + distancia * (datosParrafo.Count - 1) && (int)tamañoFila + distancia * (datosParrafo.Count - 1) <= margenMaxDerecha + 1)
                                {
                                    resto = -1;
                                    verificador = true;
                                }
                                else
                                {
                                    distancia += 1;
                                }

                            }
                            x = margenIzquierda;

                            foreach (Tuple<string, XFont, int> generacionParrafo in datosParrafo)
                            {
                                if (resto == 0)
                                {
                                    distancia -= 1;
                                }
                                resto -= 1;
                                gfx.DrawString(generacionParrafo.Item1, generacionParrafo.Item2, XBrushes.Black, x, currentY);
                                x += distancia + generacionParrafo.Item3;
                            }
                            verificador = false;
                            tamañoFila = margenIzquierda;
                            datosParrafo.Clear();
                            x = margenIzquierda;
                            currentY += interlineado;
                            distancia = 5;

                            datosParrafo.Add(new Tuple<string, XFont, int>(palabra, parrafoLetra, (int)textSize.Width));
                            x += textSize.Width + distancia;
                            tamañoFila += textSize.Width;
                        }
                        else
                        {
                            datosParrafo.Add(new Tuple<string, XFont, int>(palabra, parrafoLetra, (int)textSize.Width));
                            x += textSize.Width + distancia;
                            tamañoFila += textSize.Width;
                        }
                    }
                }
                if (datosParrafo.Count == 2)
                {
                    while (verificador == false)
                    {
                        if (tamañoFila + distancia * (datosParrafo.Count - 1) > margenMaxDerecha)
                        {
                            resto = (margenMaxDerecha - (int)tamañoFila) / distancia;
                            verificador = true;
                        }
                        else if (margenMaxDerecha - 1 <= (int)tamañoFila + distancia * (datosParrafo.Count - 1) && (int)tamañoFila + distancia * (datosParrafo.Count - 1) <= margenMaxDerecha + 1)
                        {
                            resto = -1;
                            verificador = true;
                        }
                        else
                        {
                            distancia += 1;
                        }

                    }
                }
                x = margenIzquierda;

                foreach (Tuple<string, XFont, int> generacionParrafo in datosParrafo)
                {
                    if (resto == 0)
                    {
                        distancia -= 1;
                    }
                    resto -= 1;
                    gfx.DrawString(generacionParrafo.Item1, generacionParrafo.Item2, XBrushes.Black, x, currentY);
                    x += distancia + generacionParrafo.Item3;
                }

                currentY += interlineado;


                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, margenEncabezadoDerecha, maximoInferior - currentY);
                tf.DrawString("QUEDA UD. NOTIFICADO.", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;


                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, margenEncabezadoDerecha, maximoInferior - currentY);
                tf.DrawString((datos.Oficina.OficinaLocalidad.ToUpper() + ", " + datos.PopUp.PopUpFecha.Day.ToString() + " de " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(datos.PopUp.PopUpFecha.Month).ToString() + " de " + datos.PopUp.PopUpFecha.Year.ToString()) + ".", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("Dependencia: Secretaría de Trabajo, Empleo y Seguridad Social. Domicilio: " + datos.Oficina.OficinaDomicilio + " - " + datos.Oficina.OficinaLocalidad +".", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 4;

                tf.Alignment = XParagraphAlignment.Right;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Abogado.Nombre, contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Right;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Abogado.Matricula, contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado * 2;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString("DATOS DE REFERENCIA", contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Abogado.Email, contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Abogado.Telefono, contenidoEstilo, XBrushes.Black, margenesAux);

                currentY += interlineado;

                tf.Alignment = XParagraphAlignment.Justify;
                margenesAux = new XRect(margenIzquierda, currentY, (int)page.Width.Value - margenIzquierda - margenDerecha, maximoInferior - currentY);
                tf.DrawString(datos.Abogado.Celular, contenidoEstilo, XBrushes.Black, margenesAux);
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
                    string fileName = consultaDB.TipoNotificacionCodigo;
                    string metadataStr = "{\"FileName\":\""+ fileName + "\", \"Type\":\"aplication/pdf\", \"Size\":" + size + "}";
                    bool generadoAutomatico = true;

                    var archivo = new NotificacionArchivoTemporal(fileName, true, metadataStr, "application/pdf", archivoFinal, generadoAutomatico, DateTime.Now, DateTime.Now, input.UserId, input.UserId, consultaDB.TipoNotificacionCodigo);
                    int archivoId = this._NotificacionArchivoTemporalRepository.AddArchivo(archivo);
                    var output = new GenerarNotificacionPDFOutput(archivoFinal, archivoId, fileName + ".pdf");
                    this._GenerarNotificacionOutputPort.Standard(output);
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