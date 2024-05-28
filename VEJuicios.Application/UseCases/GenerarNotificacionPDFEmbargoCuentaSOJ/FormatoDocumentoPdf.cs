using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using VEJuicios.Domain;
using PdfSharp;
using Microsoft.Extensions.Hosting;

namespace VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ
{
    public class FormatoDocumentoPdf
    {
        public int MargenSuperior;
        public int MargenInferior;
        public int MargenIzquierdo;
        public int MargenIzquierdoAmpliado;
        public int MargenDerecho;
        public int MargenDerechoEncabezado;
        public int TamañoXLogo;
        public int TamañoYLogo;
        public int MaximoEjeY;
        public int MaximoEjeX;
        public int ActualEjeY;
        public int RestoEjeY;
        public double ActualEjeX;
        public int SaltoDeLinea;
        public int EspacioEntreMargenesX;
        public XFont EstiloTitulo;
        public XFont EstiloContenidoNegrita;
        public XFont EstiloContenido;
        public XFont EstiloTablaNegrita;
        public XFont EstiloTabla;
        public XFont EstiloJuzgado;
        public XFont EstiloJuzgadoNegrita;
        public XBrush ColorTexto;
        public XPen BordeTabla;
        public XGraphics ContextoGrafico;
        public XTextFormatter FormateadorTexto;
        public XImage ImagenLogo;
        public XRect Coordenadas;
        public PdfDocument Documento = new();
        public PdfPage Pagina;
        public PdfConfigurationNotificacion ConfiguracionPdf;
        public FormatoDocumentoPdf(IHostEnvironment hostEnvironment)
        {
            ConfiguracionPdf = new PdfConfigurationNotificacion().NotificacionGenerica(hostEnvironment);

            Documento = new();
            Pagina = Documento.AddPage();
            Pagina.Size = PageSize.A4;
            ContextoGrafico = XGraphics.FromPdfPage(Pagina);
            FormateadorTexto = new XTextFormatter(ContextoGrafico);
            ImagenLogo = XImage.FromFile(ConfiguracionPdf.Logo.RutaLogo);

            MargenInferior = ConfiguracionPdf.Margenes.Inferior;
            MargenSuperior = ConfiguracionPdf.Margenes.Superior;
            MargenIzquierdo = ConfiguracionPdf.Margenes.Izquieda;
            MargenDerecho = ConfiguracionPdf.Margenes.Derecha;
            MargenIzquierdoAmpliado = MargenIzquierdo + 55;
            MargenDerechoEncabezado = (int)Pagina.Width.Value - MargenDerecho - MargenIzquierdoAmpliado;
            EspacioEntreMargenesX = (int)Pagina.Width.Value - MargenIzquierdo - MargenDerecho;

            TamañoXLogo = ConfiguracionPdf.Logo.TamañoX;
            TamañoYLogo = ConfiguracionPdf.Logo.TamañoY;

            MaximoEjeY = (int)Pagina.Height.Value - MargenInferior;
            MaximoEjeX = (int)Pagina.Width - MargenDerecho;
            ActualEjeY = MargenSuperior;
            RestoEjeY = MaximoEjeY - ActualEjeY;
            ActualEjeX = MargenIzquierdo;

            SaltoDeLinea = 13;

            EstiloTitulo = ConfiguracionPdf.EstilosLetras.Titulo;
            EstiloContenidoNegrita = ConfiguracionPdf.EstilosLetras.ContenidoNegrita;
            EstiloContenido = ConfiguracionPdf.EstilosLetras.Contenido;
            EstiloTablaNegrita = ConfiguracionPdf.EstilosLetras.TablaNegrita;
            EstiloTabla = ConfiguracionPdf.EstilosLetras.Tabla;
            EstiloJuzgadoNegrita = ConfiguracionPdf.EstilosLetras.JuzgadoNegrita;
            EstiloJuzgado = ConfiguracionPdf.EstilosLetras.Juzgado;

            ColorTexto = XBrushes.Black;

            BordeTabla = ConfiguracionPdf.BordeTabla;
        }
        public void Escribir(string texto, XFont estiloTexto, double inicioEjeX, double finalEjeX, int renglonesASaltar = 0)
        {
            int totalLineas = CalcularCantidadLineas(texto, estiloTexto, finalEjeX, RestoEjeY);
            Coordenadas = new XRect(inicioEjeX, ActualEjeY, finalEjeX, RestoEjeY);
            FormateadorTexto.DrawString(texto, estiloTexto, ColorTexto, Coordenadas);
            CalcularSaltoPagina(renglonesASaltar * totalLineas);
        }
        public void EscribirParrafo(string[] frases, XFont fuenteNormal, XFont fuenteNegrita, double inicioEjeX, double finalEjeX, int saltarRenglones = 0)
        {
            string cadenaEspacios = "";
            XFont estiloActual = fuenteNormal;
            bool esNegrita = false;
            foreach (string frase in frases)
            {
                Escribir(cadenaEspacios + frase, estiloActual, inicioEjeX, finalEjeX);
                string[] palabrasEnFrase = frase.Split(' ');
                string cadenaPalabras = "";
                foreach (string palabra in palabrasEnFrase)
                {
                    cadenaPalabras += palabra + " ";
                    double largoPalabras = ContextoGrafico.MeasureString(cadenaEspacios + cadenaPalabras, estiloActual).Width;
                    if (largoPalabras >= EspacioEntreMargenesX)
                    {
                        cadenaPalabras = palabra + " ";
                        cadenaEspacios = "";
                        CalcularSaltoPagina(SaltoDeLinea);
                    }
                }
                double largoCadenaPalabras = ContextoGrafico.MeasureString(cadenaEspacios + cadenaPalabras, estiloActual).Width;
                estiloActual = esNegrita ? fuenteNormal : fuenteNegrita;
                double largoEspacio = ContextoGrafico.MeasureString(" ", estiloActual).Width;
                cadenaEspacios = new string(' ', (int)Math.Ceiling(largoCadenaPalabras / largoEspacio));

                if (esNegrita) { ActualEjeY--; RestoEjeY++; }
                esNegrita = !esNegrita;
            }
            CalcularSaltoPagina(saltarRenglones);
        }
        public void DibujarTabla(List<int> columnasTablaLargo, List<List<string>> filasTabla, int renglonesASaltar = 0)
        {
            int grosorLineaTabla = 2;
            columnasTablaLargo.Insert(0, MargenIzquierdo);
            for (int i = 1; i < columnasTablaLargo.Count; i++)
            {
                columnasTablaLargo[i] += columnasTablaLargo[i - 1];
            }
            DibujarEncabezadoTabla(filasTabla[0], grosorLineaTabla, columnasTablaLargo);
            DibujarDatosTabla(filasTabla, grosorLineaTabla, columnasTablaLargo);
            CalcularSaltoPagina(renglonesASaltar);
        }
        public void DibujarEncabezadoTabla(List<string> encabezados, int grosorLineaTabla, List<int> columnasTablaLargo)
        {
            //Recorremos la posición 0 de la lista, donde deben estar los encabezados de la tabla
            for (int i = 0; i < encabezados.Count; i++)
            {
                FormateadorTexto.Alignment = XParagraphAlignment.Center;
                Escribir(encabezados[i], EstiloTablaNegrita, columnasTablaLargo[i] + grosorLineaTabla, columnasTablaLargo[i + 1] - columnasTablaLargo[i] - grosorLineaTabla * 2);

                Coordenadas = new XRect(columnasTablaLargo[i], ActualEjeY, columnasTablaLargo[i + 1] - columnasTablaLargo[i], SaltoDeLinea * 2);
                ContextoGrafico.DrawRectangle(BordeTabla, Coordenadas);
            }
            bool saltoDePagina = CalcularSaltoPagina(SaltoDeLinea * 2);
            if (saltoDePagina)
            {
                DibujarEncabezadoTabla(encabezados, grosorLineaTabla, columnasTablaLargo);
            }
        }
        public void DibujarDatosTabla(List<List<string>> filasTabla, int grosorLineaTabla, List<int> columnasTablaLargo)
        {
            //Recorremos la lista desde la posición 1, donde empiezan los datos de la tabla
            int tablaAltoMaximo = 0;
            for (int j = 1; j < filasTabla.Count; j++)
            {
                List<string> filaDatos = filasTabla[j];
                for (int i = 0; i < filasTabla[j].Count; i++)
                {
                    FormateadorTexto.Alignment = XParagraphAlignment.Left;
                    Escribir(filaDatos[i], EstiloTabla, columnasTablaLargo[i] + grosorLineaTabla, columnasTablaLargo[i + 1] - columnasTablaLargo[i] - grosorLineaTabla * 2);
                    int totalLineas = CalcularCantidadLineas(filaDatos[i], EstiloTabla, columnasTablaLargo[i + 1] - columnasTablaLargo[i] - grosorLineaTabla * 2, MaximoEjeY);
                    if (tablaAltoMaximo < totalLineas)
                    {
                        tablaAltoMaximo = totalLineas;
                    }
                }
                for (int i = 0; i < filasTabla[0].Count; i++)
                {
                    Coordenadas = new XRect(columnasTablaLargo[i], ActualEjeY, columnasTablaLargo[i + 1] - columnasTablaLargo[i], SaltoDeLinea * tablaAltoMaximo);
                    ContextoGrafico.DrawRectangle(BordeTabla, Coordenadas);
                }
                bool saltoDePagina = CalcularSaltoPagina(SaltoDeLinea * tablaAltoMaximo);
                if (saltoDePagina)
                {
                    DibujarEncabezadoTabla(filasTabla[0], grosorLineaTabla, columnasTablaLargo);
                }
            }
        }
        public int CalcularCantidadLineas(string textoAMedir, XFont estiloTexto, double largoMaximo, double altoMaximo)
        {
            XSize tamañoMaximoDelTexto = new(largoMaximo, altoMaximo);
            int cantidadLineas = 1;
            XSize largoFrase = ContextoGrafico.MeasureString(textoAMedir, estiloTexto);
            if (largoFrase.Width > tamañoMaximoDelTexto.Width)
            {
                string[]? conjuntoPalabras = textoAMedir.Split(" ");
                string palabra = conjuntoPalabras[0];
                for (int j = 1; j < conjuntoPalabras.Length; j++)
                {
                    palabra += " " + conjuntoPalabras[j];
                    largoFrase = ContextoGrafico.MeasureString(palabra, estiloTexto);
                    if (largoFrase.Width >= tamañoMaximoDelTexto.Width)
                    {
                        cantidadLineas++;
                        palabra = conjuntoPalabras[j];
                    }
                }
            }
            if (RestoEjeY - cantidadLineas * SaltoDeLinea <= 0)
            {
                CalcularSaltoPagina();
            }
            return cantidadLineas;
        }
        public bool CalcularSaltoPagina(int renglonesASaltar = 0)
        {
            ActualEjeY += renglonesASaltar;
            RestoEjeY -= renglonesASaltar;
            bool saltoDePagina = RestoEjeY <= 0;
            if (saltoDePagina)
            {
                Pagina = Documento.AddPage();
                ContextoGrafico = XGraphics.FromPdfPage(Pagina);
                FormateadorTexto = new XTextFormatter(ContextoGrafico);
                Pagina.Size = PageSize.A4;
                ActualEjeY = MargenSuperior;
                RestoEjeY = MaximoEjeY - ActualEjeY;
                ActualEjeX = MargenIzquierdo;
                ContextoGrafico.DrawImage(ImagenLogo, ConfiguracionPdf.Logo.MargenIzquieda, ConfiguracionPdf.Logo.MargenSuperior, TamañoXLogo, TamañoYLogo);
                ActualEjeY += SaltoDeLinea * 3 + 6;
                RestoEjeY -= SaltoDeLinea * 3 + 6;
            }
            return saltoDePagina;
        }
    }
}