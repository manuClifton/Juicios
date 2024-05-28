using Microsoft.EntityFrameworkCore;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Infrastructure.Repositories
{
    public class VNotificacionArchivoEnvioWorkerRepository : IVNotificacionArchivoEnvioWorkerRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;
        public VNotificacionArchivoEnvioWorkerRepository(DbContextOptions<SQLServerContext> options)
        {
            _options = options;
        }

        public async Task<List<VNotificacionArchivoEnvioWorker>> GetAllNotificacionArchivoEnvioWorkerAsync()
        {
            using (var dbContext = new SQLServerContext(_options))
            {
                return await dbContext.VNotificacionArchivoEnvioWorker.ToListAsync();
            }
        }

        public async Task<List<VNotificacionArchivoEnvioWorker>> GetNotificacionArchivoEnvioWorkerByNotificacionIdAsync(int id)
        {
            try
            {
                using (var dbContext = new SQLServerContext(_options))
                {
                    return await dbContext.VNotificacionArchivoEnvioWorker
                                    .Where(x => x.NotificacionId == id)
                                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
