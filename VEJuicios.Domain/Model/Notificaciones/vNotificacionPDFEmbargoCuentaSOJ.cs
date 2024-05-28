namespace VEJuicios.Domain.Model.Notificaciones
{
    public sealed class vNotificacionPDFEmbargoCuentaSOJ
    {
        public int NotificacionId { get; set; }
        public string? TipoNotificacionCodigo { get; set; }

        public string? CasoAutos { get; set; }
        public DateTime? CasoFechaSentencia { get; set; }
        public string? CasoExpedienteJudicial { get; set; }

        public string? OficinaDomicilio { get; set; }
        public string? OficinaLocalidad { get; set; }

        public string? SecretariaDescripcion { get; set; }

        public string? JuzgadoDireccion { get; set; }
        public string? JuzgadoCiudad { get; set; }
        public string? JuzgadoCodPostal { get; set; }
        public string? ProvinciaDescripcion { get; set; }
        public string? JuzgadoDescripcion { get; set; }
        public string? JuzgadoJuez { get; set; }

        public string? ParteDireccion { get; set; }
        public string? ParteCiudad { get; set; }

        public string? PersonaNombre { get; set; }

        public string? FueroDescripcion { get; set; }

        public string? AbogadoNombre { get; set; }
        public string? AbogadoTomo { get; set; }
        public string? AbogadoFolio { get; set; }
        public string? AbogadoEmail { get; set; }
        public string? AbogadoTelefono { get; set; }
        public string? AbogadoCelular { get; set; }

        public string? JurisdiccionDescripcion { get; set; }

        public string? OficioBCRARespuestaNumeroCuenta { get; set; }
        public decimal? OficioBCRARespuestaImporteRetenido { get; set; }
        public DateTime? OficioBCRARespuestaFechaRetencion { get; set; }
        public decimal? OficioBCRARespuestaMontoInformado { get; set; }
        public string? BancoAFIPDescripcion { get; set; }
        public string? BancoTipoCuentaDescripcion { get; set; }
    }
}
