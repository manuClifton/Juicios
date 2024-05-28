using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class vNotificacionPDFEmbargoCuentaSOJRepsitory : IvNotificacionPDFEmbargoCuentaSOJRepository
    {
        private readonly SQLServerContext _context;
        public vNotificacionPDFEmbargoCuentaSOJRepsitory(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(vNotificacionPDFEmbargoCuentaSOJ entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(vNotificacionPDFEmbargoCuentaSOJ entity)
        {
            throw new NotImplementedException();
        }

        public List<vNotificacionPDFEmbargoCuentaSOJ> GetById(int Id)
        {
            try
            {
                // SqlConnection.ClearAllPools();
                var elemento = this._context.VNotificacionPDFEmbargoCuentaSOJ.Where(x => x.NotificacionId.Equals(Id)).ToList();
                return elemento;
            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }
    }
}
