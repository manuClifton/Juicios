using System.Collections.Generic;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionArchivoTemporalRepository : INotificacionArchivoTemporalRepository
    {
        private readonly SQLServerContext _context;

        public NotificacionArchivoTemporalRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(NotificacionArchivoTemporal entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(NotificacionArchivoTemporal entity)
        {
            throw new NotImplementedException();
        }

        public int AddArchivo(NotificacionArchivoTemporal entity)
        {

            _context.NotificacionArchivosTemporales.Add(entity);
            _context.SaveChanges();
            int archivoId = entity.ArchivoId;
            return archivoId;

        }
        public void DeleteArchivo(int id)
        {
            try
            {
                var exist = _context.NotificacionArchivosTemporales.FirstOrDefault(x => x.ArchivoId == id);

                if (exist != null)
                {
                    _context.NotificacionArchivosTemporales.Remove(new NotificacionArchivoTemporal() { ArchivoId = id });
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public NotificacionArchivoTemporal GetById(int archivoId)
        {
            return this._context.NotificacionArchivosTemporales.FirstOrDefault(x => x.ArchivoId.Equals(archivoId));
        }
        public List<NotificacionArchivoTemporal> GetByArrayId(int[] archivosId)
        {
            try
            {
                List<NotificacionArchivoTemporal> lista = new List<NotificacionArchivoTemporal>();
                foreach (int id in archivosId)
                {
                    lista.Add(this._context.NotificacionArchivosTemporales.FirstOrDefault(x => x.ArchivoId.Equals(id)));
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}