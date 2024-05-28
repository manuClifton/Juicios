using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionVerificarMegas
{
    public sealed class NotificacionVerificarMegasUseCase : INotificacionVerificarMegasUseCase
    {
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;
        private readonly IVNotificacionArchivoTemporalMetadataRepository _NotificacionArchivoTemporalMetadataRepository;
        private readonly INotificacionVerigficarMegasPresenterOutputPort _NotificacionVerigficarMegasPresenterOutputPort;
        public NotificacionVerificarMegasUseCase(
            INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository,
            IVNotificacionArchivoTemporalMetadataRepository notificacionArchivoTemporalMetadataRepository,
            INotificacionVerigficarMegasPresenterOutputPort notificacionVerigficarMegasPresenterOutputPort)
        {
            _NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
            _NotificacionArchivoTemporalMetadataRepository = notificacionArchivoTemporalMetadataRepository;
            _NotificacionVerigficarMegasPresenterOutputPort = notificacionVerigficarMegasPresenterOutputPort;
        }
        public  Task Execute(NotificacionVerificarMegasInput input)
        {
            List<VistaNotificacionArchivoTemporalMetadata> listaArchivosRelacionados = new List<VistaNotificacionArchivoTemporalMetadata>();
            List<NotificacionArchivoTemporal> listaArchivosEliminar = new List<NotificacionArchivoTemporal>();
            List<NotificacionArchivoTemporal> listaArchivosGuardar = new List<NotificacionArchivoTemporal>();
            int acumulador = 0;
            var output = false;
            try
            {
                //Obtengo el Size total de los archivos relacionados:
                listaArchivosRelacionados = this._NotificacionArchivoTemporalMetadataRepository.GetAllByNotificacioId(input.NotificacionID);
                if (listaArchivosRelacionados.Count > 0)
                {
                    foreach (var elemento in listaArchivosRelacionados)
                    {
                        // Convertir el atributo string a un objeto JSON
                        JsonDocument doc = JsonDocument.Parse(elemento.MetaData);

                        // Obtener el valor del campo "Size"
                        int size = doc.RootElement.GetProperty("Size").GetInt32();

                        // Sumar el valor al acumulador
                        acumulador += size;
                    }
                }

                //Resto el Size de lo que qiero eliminar
                
                if (input.EliminarArchivosID.Length > 0 && input.EliminarArchivosID[0] != 0)
                {
                    listaArchivosEliminar = this._NotificacionArchivoTemporalRepository.GetByArrayId(input.EliminarArchivosID);
                    foreach (var elemento in listaArchivosEliminar)
                    {
                        // Convertir el atributo string a un objeto JSON
                        JsonDocument doc = JsonDocument.Parse(elemento.MetaData);

                        // Obtener el valor del campo "Size"
                        int size = doc.RootElement.GetProperty("Size").GetInt32();

                        // Sumar el valor al acumulador
                        acumulador -= size;
                    }
                }
                //Sumo lo que quiero guardar
                if (input.GuardarArchivosID.Length > 0 && input.GuardarArchivosID[0] != 0)
                {
                    listaArchivosGuardar = this._NotificacionArchivoTemporalRepository.GetByArrayId(input.GuardarArchivosID);
                    foreach (var elemento in listaArchivosGuardar)
                    {
                        // Convertir el atributo string a un objeto JSON
                        JsonDocument doc = JsonDocument.Parse(elemento.MetaData);

                        // Obtener el valor del campo "Size"
                        int size = doc.RootElement.GetProperty("Size").GetInt32();

                        // Sumar el valor al acumulador
                        acumulador += size;
                    }
                }

                if (acumulador < 8000000)
                {
                    output = true;
                }
                //retorno true/false dependiento el Size Aculumado

                this._NotificacionVerigficarMegasPresenterOutputPort.Standard(output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
