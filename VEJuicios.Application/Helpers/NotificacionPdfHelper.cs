using System.Globalization;
using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Application.Helpers
{
    public sealed class NotificacionPdfHelper
    {
        public NotificacionPdfHelper()
        {
        }

        public NotificacionPdfGenerico ObtenerDatosPdfGenerico(GenerarNotificacionPDFInput input, VistaNotificacionPDF consultaDB)
        {
            try
            {
                var caso = new NotificacionPdfCaso(consultaDB.CasoAutos, consultaDB.CasoExpedienteJudicial, consultaDB.CasoFechaSentencia);
                var oficina = new NotificacionPdfOficina(consultaDB.OficinaDomicilio, consultaDB.OficinaLocalidad);
                var popUp = new NotificacionPdfPopUp(input.FechaAsociada, input.popUpFecha, input.CopiasSiNo);
                var juzgado = new NotificacionPdfJuzgado(consultaDB.JuzgadoDescripcion, consultaDB.JuzgadoDireccion, consultaDB.JuzgadoJuez, consultaDB.JuzgadoCiudad, consultaDB.JuzgadoCodPostal, consultaDB.ProvinciaDescripcion);
                var secretaria = new NotificacionPdfSecretaria(consultaDB.SecretariaDescripcion);
                var fuero = new NotificacionPdfFuero(consultaDB.FueroDescripcion);
                var abogado = new NotificacionPdfAbogado(consultaDB.AbogadoNombre, consultaDB.AbogadoTomo, consultaDB.AbogadoFolio, consultaDB.AbogadoEmail, consultaDB.AbogadoTelefono, consultaDB.AbogadoCelular);
                var parte = new NotificacionPdfParte(consultaDB.PersonaNombre, consultaDB.ParteDireccion, consultaDB.ParteCiudad);
                var tipoNotificacion = new NotificacionPdfTipoNotificacion(consultaDB.TipoNotificacionCodigo);
                var jurisdiccion = new NotificacionPdfJurisdiccion(consultaDB.JurisdiccionDescripcion);
                string articulo = "";

                caso.ExpedienteJudicial = AgregarEspaciosALosDatos(caso.ExpedienteJudicial);
                oficina.OficinaDomicilio = AgregarEspaciosALosDatos(oficina.OficinaDomicilio);
                fuero.Descripcion = AgregarEspaciosALosDatos(fuero.Descripcion);
                juzgado.Descripcion = AgregarEspaciosALosDatos(juzgado.Descripcion);
                secretaria.Descripcion = AgregarEspaciosALosDatos(secretaria.Descripcion);

                if (tipoNotificacion.Codigo == "SENTENCIA" || tipoNotificacion.Codigo == "LIQUIDACION" || tipoNotificacion.Codigo == "TRABA IGB")
                {
                    articulo = "la";
                }
                else
                {
                    articulo = "el";
                }
                string[] contenidoParrafo = { "Hago saber a Ud. que en expediente caratulado:", caso.Autos, "se notifica " + articulo, tipoNotificacion.Codigo, "de fecha", popUp.FechaAsociada.ToString("dd/MM/yyyy"), "cuya copia se acompaña." };

                var salida = new NotificacionPdfGenerico(caso, oficina, popUp, juzgado, secretaria, fuero, abogado, parte, jurisdiccion, contenidoParrafo);

                return salida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public sealed class NotificacionPdfGenerico
        {
            public NotificacionPdfCaso Caso;
            public NotificacionPdfOficina Oficina;
            public NotificacionPdfPopUp PopUp;
            public NotificacionPdfJuzgado Juzgado;
            public NotificacionPdfSecretaria Secretaria;
            public NotificacionPdfFuero Fuero;
            public NotificacionPdfAbogado Abogado;
            public NotificacionPdfParte Parte;
            public NotificacionPdfJurisdiccion Jurisdiccion;

            public string[] ContenidoParrafo;
            public NotificacionPdfGenerico(NotificacionPdfCaso caso, NotificacionPdfOficina oficina, NotificacionPdfPopUp popUp, NotificacionPdfJuzgado juzgado,
                NotificacionPdfSecretaria secretaria, NotificacionPdfFuero fuero, NotificacionPdfAbogado abogado,
                NotificacionPdfParte parte, NotificacionPdfJurisdiccion jurisdiccion, string[] contenidoParrafo)
            {
                Caso = caso;
                Oficina = oficina;
                PopUp = popUp;
                Juzgado = juzgado;
                Secretaria = secretaria;
                Fuero = fuero;
                Abogado = abogado;
                Parte = parte;
                ContenidoParrafo = contenidoParrafo;
                Jurisdiccion = jurisdiccion;
            }
        }
        public NotificacionPdfEmbargoCuentaSoj ObtenerDatosPdfEmbargoCuentaSoj(GenerarPDFEmbargoCuentaSOJInput input, List<vNotificacionPDFEmbargoCuentaSOJ> consultaDB)
        {
            try
            {
                var caso = new NotificacionPdfCaso(consultaDB[0].CasoAutos, consultaDB[0].CasoExpedienteJudicial, consultaDB[0].CasoFechaSentencia);
                var oficina = new NotificacionPdfOficina(consultaDB[0].OficinaDomicilio, consultaDB[0].OficinaLocalidad);
                var popUp = new NotificacionPdfPopUp(input.FechaAsociada, input.popUpFecha, input.CopiasSiNo);
                var juzgado = new NotificacionPdfJuzgado(consultaDB[0].JuzgadoDescripcion, consultaDB[0].JuzgadoDireccion, consultaDB[0].JuzgadoJuez, consultaDB[0].JuzgadoCiudad, consultaDB[0].JuzgadoCodPostal, consultaDB[0].ProvinciaDescripcion);
                var secretaria = new NotificacionPdfSecretaria(consultaDB[0].SecretariaDescripcion);
                var fuero = new NotificacionPdfFuero(consultaDB[0].FueroDescripcion);
                var abogado = new NotificacionPdfAbogado(consultaDB[0].AbogadoNombre, consultaDB[0].AbogadoTomo, consultaDB[0].AbogadoFolio, consultaDB[0].AbogadoEmail, consultaDB[0].AbogadoTelefono, consultaDB[0].AbogadoCelular);
                var parte = new NotificacionPdfParte(consultaDB[0].PersonaNombre, consultaDB[0].ParteDireccion, consultaDB[0].ParteCiudad);
                var tipoNotificacion = new NotificacionPdfTipoNotificacion(consultaDB[0].TipoNotificacionCodigo);
                var jurisdiccion = new NotificacionPdfJurisdiccion(consultaDB[0].JurisdiccionDescripcion);
                List<CuentasEmbargadas> cuentasEmbargadas = new List<CuentasEmbargadas>();

                foreach (var cuenta in consultaDB)
                {
                    var embargo = new CuentasEmbargadas(cuenta.OficioBCRARespuestaFechaRetencion, cuenta.OficioBCRARespuestaImporteRetenido, cuenta.OficioBCRARespuestaMontoInformado, cuenta.OficioBCRARespuestaNumeroCuenta, cuenta.BancoAFIPDescripcion, cuenta.BancoTipoCuentaDescripcion);
                    embargo.BancoAFIPDescripcion = AgregarEspaciosALosDatos(embargo.BancoAFIPDescripcion);
                    embargo.BancoTipoCuentaDescripcion = AgregarEspaciosALosDatos(embargo.BancoTipoCuentaDescripcion);
                    cuentasEmbargadas.Add(embargo);
                }

                caso.ExpedienteJudicial = AgregarEspaciosALosDatos(caso.ExpedienteJudicial);
                oficina.OficinaDomicilio = AgregarEspaciosALosDatos(oficina.OficinaDomicilio);
                fuero.Descripcion = AgregarEspaciosALosDatos(fuero.Descripcion);
                juzgado.Descripcion = AgregarEspaciosALosDatos(juzgado.Descripcion);
                secretaria.Descripcion = AgregarEspaciosALosDatos(secretaria.Descripcion);



                string oficinaYFecha = (oficina.OficinaLocalidad.ToUpper() + ", " + popUp.PopUpFecha.Day.ToString() + " de " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(popUp.PopUpFecha.Month).ToString() + " de " + popUp.PopUpFecha.Year.ToString()) + ".";

                string dependencia = "Dependencia: Secretaría de Trabajo, Empleo y Seguridad Social. Domicilio: " + oficina.OficinaDomicilio + " - " + oficina.OficinaLocalidad + ".";

                string[] contenidoParrafo = { "Hago saber a Ud. que en expediente caratulado:", caso.Autos, "se notifica que el juez interviniente ha ordenado la traba del embargo general de todos los fondos y valores que tenga depositado el ejecutado, en cualquier entidad financiera del país. Medida que ha sido debidamente gestionada y trabaja, conforme el siguiente detalle:" };

                var salida = new NotificacionPdfEmbargoCuentaSoj(caso, oficina, popUp, juzgado, secretaria, fuero, abogado, parte, jurisdiccion, cuentasEmbargadas, oficinaYFecha, dependencia, contenidoParrafo);

                return salida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public string AgregarEspaciosALosDatos(string dato)
        {
            dato = dato.Replace(".", ". ");
            dato = dato.Replace(",", ", ");
            dato = dato.Replace("-", " - ");
            dato = dato.Replace("/", " / ");
            return dato;
        }

        public sealed class NotificacionPdfEmbargoCuentaSoj
        {
            public NotificacionPdfCaso Caso;
            public NotificacionPdfOficina Oficina;
            public NotificacionPdfPopUp PopUp;
            public NotificacionPdfJuzgado Juzgado;
            public NotificacionPdfSecretaria Secretaria;
            public NotificacionPdfFuero Fuero;
            public NotificacionPdfAbogado Abogado;
            public NotificacionPdfParte Parte;
            public NotificacionPdfJurisdiccion Jurisdiccion;
            public List<CuentasEmbargadas> CuentasEmbargadas;

            public string OficinaYFecha;
            public string Dependencia;
            public string[] ContenidoParrafo;
            public NotificacionPdfEmbargoCuentaSoj(NotificacionPdfCaso caso, NotificacionPdfOficina oficina, NotificacionPdfPopUp popUp, NotificacionPdfJuzgado juzgado,
                NotificacionPdfSecretaria secretaria, NotificacionPdfFuero fuero, NotificacionPdfAbogado abogado,
                NotificacionPdfParte parte, NotificacionPdfJurisdiccion jurisdiccion, List<CuentasEmbargadas> cuentasEmbargadas, string oficinaYFecha, string dependencia, string[] contenidoParrafo)
            {
                Caso = caso;
                Oficina = oficina;
                PopUp = popUp;
                Juzgado = juzgado;
                Secretaria = secretaria;
                Fuero = fuero;
                Abogado = abogado;
                Parte = parte;
                Jurisdiccion = jurisdiccion;
                CuentasEmbargadas = cuentasEmbargadas;
                OficinaYFecha = oficinaYFecha;
                Dependencia = dependencia;
                ContenidoParrafo = contenidoParrafo;
            }
        }

        public sealed class NotificacionPdfCaso
        {
            public NotificacionPdfCaso(string? autos, string? expedienteJudicial, DateTime? fechaSentencia)
            {
                ExpedienteJudicial = expedienteJudicial == null ? "" : expedienteJudicial;
                Autos = autos == null ? "" : autos;
                FechaSentencia = (DateTime)(fechaSentencia == null ? DateTime.Now : fechaSentencia);
            }
            public string ExpedienteJudicial;
            public string Autos;
            public DateTime FechaSentencia;
        }

        public sealed class NotificacionPdfOficina
        {
            public NotificacionPdfOficina(string? oficinaDomicilio, string? oficinaLocalidad)
            {
                OficinaDomicilio = oficinaDomicilio == null ? "" : oficinaDomicilio;
                OficinaLocalidad = oficinaLocalidad == null ? "" : oficinaLocalidad;
            }
            public string OficinaDomicilio;
            public string OficinaLocalidad;
        }

        public sealed class NotificacionPdfPopUp
        {
            public NotificacionPdfPopUp(DateTime fechaAsociada, DateTime popUpFecha, string copiasSiNo)
            {
                FechaAsociada = fechaAsociada;
                PopUpFecha = popUpFecha;
                CopiasSiNo = copiasSiNo;
            }
            public DateTime FechaAsociada;
            public DateTime PopUpFecha;
            public string CopiasSiNo;
        }

        public sealed class NotificacionPdfJuzgado
        {
            public NotificacionPdfJuzgado(string? juzgadoDescripcion, string? juzgadoDireccion, string? juzgadoJuez, string? juzgadoCiudad, string? juzgadoCodPostal, string? provinciaDescripcion)
            {
                Descripcion = juzgadoDescripcion == null ? "" : juzgadoDescripcion;
                Direccion = juzgadoDireccion == null ? "" : juzgadoDireccion;
                Juez = juzgadoJuez == null ? "" : juzgadoJuez;
                Ciudad = juzgadoCiudad == null ? "" : juzgadoCiudad;
                CodPostal = juzgadoCodPostal == null ? "" : juzgadoCodPostal;
                ProvinciaDescripcion = provinciaDescripcion == null ? "" : provinciaDescripcion;
            }
            public string Descripcion;
            public string Direccion;
            public string Juez;
            public string Ciudad;
            public string CodPostal;
            public string ProvinciaDescripcion;
        }

        public sealed class NotificacionPdfSecretaria
        {
            public NotificacionPdfSecretaria(string? secretariaDescripcion)
            {
                Descripcion = secretariaDescripcion == null ? "" : secretariaDescripcion;
            }
            public string Descripcion;
        }

        public sealed class NotificacionPdfFuero
        {
            public NotificacionPdfFuero(string? fueroDescripcion)
            {
                Descripcion = fueroDescripcion == null ? "" : fueroDescripcion;
            }
            public string Descripcion;
        }

        public sealed class NotificacionPdfAbogado
        {
            public NotificacionPdfAbogado(string? abogadoNombre, string? abogadoTomo, string? abogadoFolio, string? abogadoEmail, string? abogadoTelefono, string? abogadoCelular)
            {
                Nombre = abogadoNombre == null ? "" : abogadoNombre;
                Matricula = "Tomo: " + (abogadoTomo == null ? "" : abogadoTomo) + " / Folio: " + (abogadoFolio == null ? "" : abogadoFolio);
                Email = abogadoEmail == null ? "" : "Correo: " + abogadoEmail;
                Telefono = abogadoTelefono == null ? "" : "Teléfono fijo: " + abogadoTelefono;
                Celular = abogadoCelular == null ? "" : "Celular: " + abogadoCelular;
            }
            public string Nombre;
            public string Matricula;
            public string Email;
            public string Telefono;
            public string Celular;
        }

        public sealed class NotificacionPdfParte
        {
            public NotificacionPdfParte(string? personaNombre, string? parteDireccion, string? parteCiudad)
            {
                Nombre = personaNombre == null ? "" : personaNombre;
                Direccion = parteDireccion == null ? "" : parteDireccion;
                Ciudad = parteCiudad == null ? "" : parteCiudad;
            }
            public string Nombre;
            public string Direccion;
            public string Ciudad;
        }

        public sealed class NotificacionPdfTipoNotificacion
        {
            public NotificacionPdfTipoNotificacion(string? tipoNotificacionCodigo)
            {
                Codigo = tipoNotificacionCodigo == null ? "" : tipoNotificacionCodigo;
            }
            public string Codigo;
        }

        public sealed class NotificacionPdfJurisdiccion
        {
            public NotificacionPdfJurisdiccion(string? jurisdiccionDescripcion)
            {
                Descripcion = jurisdiccionDescripcion == null ? "" : jurisdiccionDescripcion;
            }
            public string Descripcion;
        }

        public sealed class CuentasEmbargadas
        {
            public CuentasEmbargadas(DateTime? oficioBCRARespuestaFechaRetencion, decimal? oficioBCRARespuestaImporteRetenido, decimal? oficioBCRARespuestaMontoInformado, string? oficioBCRARespuestaNumeroCuenta, string? bancoAFIPDescripcion, string? bancoTipoCuentaDescripcion)
            {
                FechaRetencion = (DateTime)(oficioBCRARespuestaFechaRetencion == null ? DateTime.Now : oficioBCRARespuestaFechaRetencion);
                ImporteRetenido = (decimal)(oficioBCRARespuestaImporteRetenido == null ? 0 : oficioBCRARespuestaImporteRetenido);
                MontoInformado = (decimal)(oficioBCRARespuestaMontoInformado == null ? 0 : oficioBCRARespuestaMontoInformado);
                NumeroCuenta = oficioBCRARespuestaNumeroCuenta == null ? "" : oficioBCRARespuestaNumeroCuenta;
                BancoAFIPDescripcion = bancoAFIPDescripcion == null ? "" : bancoAFIPDescripcion;
                BancoTipoCuentaDescripcion = bancoTipoCuentaDescripcion == null ? "" : bancoTipoCuentaDescripcion;
            }
            public DateTime FechaRetencion;
            public decimal ImporteRetenido;
            public decimal MontoInformado;
            public string NumeroCuenta;
            public string BancoAFIPDescripcion;
            public string BancoTipoCuentaDescripcion;
        }
    }
}
