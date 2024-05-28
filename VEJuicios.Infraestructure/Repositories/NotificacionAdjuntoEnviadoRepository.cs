using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionAdjuntoEnviadoRepository : INotificacionAdjuntoEnviadoRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;
        private readonly SQLServerContext _context;

        public NotificacionAdjuntoEnviadoRepository(DbContextOptions<SQLServerContext> options, SQLServerContext context)
        {
            _options = options;
            _context = context;
        }

        public void Add(NotificacionAdjuntoEnviado entity)
        {
            this._context.NotificacionAdjuntoEnviado.Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(NotificacionAdjuntoEnviado entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(NotificacionAdjuntoEnviado entity)
        {
            try
            {
                using (var dbContext = new SQLServerContext(_options))
                {
                    await dbContext.NotificacionAdjuntoEnviado.AddAsync(entity);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Se genero un error en el servicio de Envio de Notificaciones. El error es: " + ex.Message);
            }
        }
    }
}
