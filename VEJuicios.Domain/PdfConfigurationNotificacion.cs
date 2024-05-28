using Microsoft.Extensions.Hosting;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public class PdfConfigurationNotificacion
    {
        public string ImagesPath;

        public PdfConfigurationMargenes Margenes;

        public PdfConfigurationLogo Logo;

        public int Interlineado;

        public PdfConfigurationEstilosLetras EstilosLetras;

        public XPen BordeTabla;

        public PdfConfigurationNotificacion NotificacionGenerica(IHostEnvironment hostEnvironment)
        {
            var configuracion = new PdfConfigurationNotificacion();
            string folderPath = hostEnvironment.ContentRootPath;
            var rutaImagen = Path.Combine(folderPath, "wwwroot/images", "LogoMinCapitalHumanoBn.png");
           

            configuracion.ImagesPath = rutaImagen;

            configuracion.Margenes = new PdfConfigurationMargenes();

            configuracion.Margenes.Superior = 141;
            configuracion.Margenes.Inferior = 56;
            configuracion.Margenes.Izquieda = 56;
            configuracion.Margenes.Derecha = 56;

            configuracion.Logo = new PdfConfigurationLogo();

            configuracion.Logo.RutaLogo = rutaImagen;

            configuracion.Logo.TamañoX = 125;
            configuracion.Logo.TamañoY = 125;
            configuracion.Logo.MargenIzquieda = 42;
            configuracion.Logo.MargenSuperior = 28;

            /*
            configuracion.Logo.TamañoX = 180;
            configuracion.Logo.TamañoY = 100;
            configuracion.Logo.MargenIzquieda = 42;
            configuracion.Logo.MargenSuperior = 28;*/


            configuracion.Interlineado = 15;

            configuracion.EstilosLetras = new PdfConfigurationEstilosLetras();

            configuracion.EstilosLetras.Titulo = new XFont("Arial", 10, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Contenido = new XFont("Arial", 9, XFontStyleEx.Regular);
            configuracion.EstilosLetras.ContenidoNegrita = new XFont("Arial", 9, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Tabla = new XFont("Arial", 8, XFontStyleEx.Regular);
            configuracion.EstilosLetras.TablaNegrita = new XFont("Arial", 8, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Juzgado = new XFont("Arial", 9, XFontStyleEx.Regular);
            configuracion.EstilosLetras.JuzgadoNegrita = new XFont("Arial", 9, XFontStyleEx.Bold);

            configuracion.BordeTabla = new XPen(XColors.Black, 1);

            return configuracion;
        }

        public PdfConfigurationNotificacion Constancia(IHostEnvironment hostEnvironment)
        {
            var configuracion = new PdfConfigurationNotificacion();
            string folderPath = hostEnvironment.ContentRootPath;
            var rutaImagen = Path.Combine(folderPath, "wwwroot/images", "LogoMinCapitalHumanoBn.png");


            configuracion.ImagesPath = rutaImagen;

            configuracion.Margenes = new PdfConfigurationMargenes();

            configuracion.Margenes.Superior = 125;
            configuracion.Margenes.Inferior = 40;
            configuracion.Margenes.Izquieda = 60;
            configuracion.Margenes.Derecha = 60;

            configuracion.Logo = new PdfConfigurationLogo();

            configuracion.Logo.RutaLogo = rutaImagen;

            configuracion.Logo.TamañoX = 125;
            configuracion.Logo.TamañoY = 125;
            configuracion.Logo.MargenIzquieda = 42;
            configuracion.Logo.MargenSuperior = 28;

            /*configuracion.Logo.TamañoX = 130;
            configuracion.Logo.TamañoY = 80;
            configuracion.Logo.MargenIzquieda = 40;
            configuracion.Logo.MargenSuperior = 40;*/


            configuracion.Interlineado = 15;

            configuracion.EstilosLetras = new ();

            configuracion.EstilosLetras.Titulo = new XFont("Arial", 15, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Contenido = new XFont("Arial", 12, XFontStyleEx.Regular);
            configuracion.EstilosLetras.ContenidoNegrita = new XFont("Arial", 12, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Tabla = new XFont("Arial", 10, XFontStyleEx.Regular);
            configuracion.EstilosLetras.TablaNegrita = new XFont("Arial", 10, XFontStyleEx.Bold);
            configuracion.EstilosLetras.Juzgado = new XFont("Arial", 11, XFontStyleEx.Regular);
            configuracion.EstilosLetras.JuzgadoNegrita = new XFont("Arial", 11, XFontStyleEx.Bold);

            configuracion.BordeTabla = new XPen(XColors.Black, 2);

            return configuracion;
        }
    }

    public class PdfConfigurationMargenes
    {
        public int Superior;

        public int Inferior;

        public int Izquieda;

        public int Derecha;
    }

    public class PdfConfigurationLogo
    {
        public int TamañoX;

        public int TamañoY;

        public string RutaLogo;

        public int MargenSuperior;

        public int MargenIzquieda;
    }

    public class PdfConfigurationEstilosLetras
    {
        public XFont Titulo;

        public XFont Contenido;

        public XFont ContenidoNegrita;

        public XFont Tabla;

        public XFont TablaNegrita;

        public XFont Juzgado;

        public XFont JuzgadoNegrita;
    }
}
