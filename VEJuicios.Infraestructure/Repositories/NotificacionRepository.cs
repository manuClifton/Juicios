
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Entity.Core.Objects;
using VEJuicios.Domain;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Infrastructure.Repositories
{
    public class NotificacionRepository : INotificacionRepository
    {
        private readonly SQLServerContext _context;
        public NotificacionRepository(SQLServerContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<VistaNotificacion>> GetAllWaitingAfipResponse()
        {
            return await _context.VistaNotificaciones.Where(x => x.EstadoId == EnumEstadoNotificaciones.EnviadoWorker || x.EstadoId == EnumEstadoNotificaciones.EnviadoAfip || x.EstadoId == EnumEstadoNotificaciones.Recibido || x.EstadoId == EnumEstadoNotificaciones.PublicadoVE).ToListAsync();
        }

        public void ConfirmarNotificacion(int notificacionId, int userId)
        {
            try
            {
                var notificacion = _context.Notificaciones.Find(notificacionId);
                if (notificacion != null)
                {

                    notificacion.EstadoId = EnumEstadoNotificaciones.PendienteDeEnvio;
                    notificacion.FechaConfirmacion = DateTime.Now;
                    notificacion.FechaUpdate = DateTime.Now;
                    notificacion.UsuarioUpdateId = userId;
                    
                    _context.Notificaciones.Update(notificacion);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            
           
        }

        public List<int> GetNumerosInternosNotificaciones(List<int> notificacionesId)
        {
            for (int i = 0; i<notificacionesId.Count(); i++)
            {
                int id = notificacionesId[i];
                var notificacion = _context.Notificaciones.FirstOrDefault(x => x.NotificacionId == id);
                notificacionesId[i] = (int)notificacion.NumeroInterno;
            }
            return notificacionesId;
        }
    }
}
