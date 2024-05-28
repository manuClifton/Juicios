
using Microsoft.EntityFrameworkCore;
using VEJuicios.Domain;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Infrastructure.Repositories
{
    public class NotificacionConstanciaRepository : IVNotificacionConstanciaRepository
    {
        private readonly SQLServerContext _context;
        public NotificacionConstanciaRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public void Add(VNotificacionConstancia entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(VNotificacionConstancia entity)
        {
            throw new NotImplementedException();
        }

        VNotificacionConstancia IVNotificacionConstanciaRepository.GetById(int Id)
        {
            return this._context.VNotificacionConstancia.SingleOrDefault(x => x.NotificacionId.Equals(Id));
        }
    }
}
