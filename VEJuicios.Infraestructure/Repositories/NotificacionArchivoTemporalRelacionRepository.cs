using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionArchivoTemporalRelacionRepository : INotificacionArchivoTemporalRelacionRepository
    {
        private readonly SQLServerContext _context;

        public NotificacionArchivoTemporalRelacionRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(NotificacionArchivoTemporalRelacion entity)
        {
            throw new NotImplementedException();
        }


        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(NotificacionArchivoTemporalRelacion entity)
        {
            throw new NotImplementedException();
        }

        public int AddRelacion(NotificacionArchivoTemporalRelacion entity)
        {
            try
            {
                var res = this._context.NotificacionArchivosTemporalesRelacion.Add(entity);
                this._context.SaveChanges();
                int archivoId = entity.ArchivoId;
                return archivoId;
            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }

        public void DeleteRelacion(NotificacionArchivoTemporalRelacion entity)
        {
            try
            {
                var res = this._context.NotificacionArchivosTemporalesRelacion.Remove(entity); 
                this._context.SaveChanges();

            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }


    }
}
