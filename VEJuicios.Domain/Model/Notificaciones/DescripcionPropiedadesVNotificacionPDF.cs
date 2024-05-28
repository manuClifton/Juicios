using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public class DescripcionPropiedadesVNotificacionPDF
    {
        public string NotificacionId { get; set; } = "Id de la notificación";
        public string CasoId { get; set; } = "Id del caso";
        public string Carpeta { get; set; } = "Carpeta";
        public string TipoNotificacionCodigo { get; set; } = "Tipo de notificación";
        public string CasoAutos { get; set; } = "Autos";
        public string CasoFechaSentencia { get; set; } = "Fecha de resolución";
        public string CasoExpedienteJudicial { get; set; } = "Expediente Judicial";
        public string SecretariaDescripcion { get; set; } = "Secretaría";
        public string JuzgadoDireccion { get; set; } = "Dirección del juzgado";
        public string JuzgadoCiudad { get; set; } = "Ciudad del juzgado";
        public string JuzgadoCodPostal { get; set; } = "Código Postal del juzgado";
        public string ProvinciaDescripcion { get; set; } = "Provincia del juzgado";
        public string JuzgadoDescripcion { get; set; } = "Juzgado";
        public string JuzgadoJuez { get; set; } = "Juez";
        public string ParteDireccion { get; set; } = "Dirección de la parte";
        public string ParteCiudad { get; set; } = "Ciudad de la parte";
        public string PersonaNombre { get; set; } = "Nombre de la persona";
        public string PersonaCuit { get; set; } = "Cuit de la persona";
        public string FueroDescripcion { get; set; } = "Fuero";
        public string AbogadoNombre { get; set; } = "Nombre del abogado";
        public string AbogadoTomo { get; set; } = "Tomo del abogado";
        public string AbogadoFolio { get; set; } = "Folio del abogado";
        public string AbogadoEmail { get; set; } = "Email del abogado";
        public string AbogadoTelefono { get; set; } = "Teléfono del abogado";
        public string AbogadoCelular { get; set; } = "Celular del abogado";
        public string JurisdiccionDescripcion { get; set; } = "Jurisdicción";
    }
}