using Microsoft.EntityFrameworkCore;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Infrastructure.Repositories
{
    public class VistaNotificacionRepository : IVistaNotificacionRepository
    {
        private readonly SQLServerContext _context;
        public VistaNotificacionRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));
        public async Task<List<VistaNotificacion>> GetAllPendientesEnvio()
        {
            return await this._context.VistaNotificaciones.Where(x => x.NotiEstadoId == Domain.EnumEstadoNotificaciones.PendienteDeEnvio).ToListAsync();
        }
    }
}
