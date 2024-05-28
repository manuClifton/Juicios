using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class VistaNotificacionArchivoTemporalMetadataRepository : IVNotificacionArchivoTemporalMetadataRepository
    {
        private readonly SQLServerContext _context;

        public VistaNotificacionArchivoTemporalMetadataRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));
        public void Add(VistaNotificacionArchivoTemporalMetadata entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(VistaNotificacionArchivoTemporalMetadata entity)
        {
            throw new NotImplementedException();
        }
        public VistaNotificacionArchivoTemporalMetadata GetByArchivoId(int archivoId)
        {
            return this._context.VistaNotificacionArchivoTemporalesMetadata.FirstOrDefault(x => x.ArchivoId.Equals(archivoId));
        }
        public List<VistaNotificacionArchivoTemporalMetadata> GetAllByNotificacioId(int notificacionId)
        {
            return this._context.VistaNotificacionArchivoTemporalesMetadata.Where(x => x.NotificacionId.Equals(notificacionId)).ToList();
        }
    }
}
