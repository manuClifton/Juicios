using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionMovimientoAltaStorePRepository: INotificacionMovimientoAltaStorePRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;
        public NotificacionMovimientoAltaStorePRepository(DbContextOptions<SQLServerContext> options)
        {
            _options = options;
        }

        public async Task<int> AltaMovimiento(int notificacionId)
        {
            try
            {
                int movimientoId;
                using (var dbContext = new SQLServerContext(_options))
                {
                    var xmlParameter = new SqlParameter("@Xml", SqlDbType.Int) { Value = notificacionId };
                    var nuevoIdParameter = new SqlParameter("@NuevoId", SqlDbType.Int);
                    //var archivoParameter = new SqlParameter("@ArchivoData", SqlDbType.Image) { Value = archivoData ?? (object)DBNull.Value };
                    nuevoIdParameter.Direction = ParameterDirection.Output;

                    var respuesta = await dbContext.Database.ExecuteSqlRawAsync("xmlNotificacionMovimientoAlta @Xml", xmlParameter);
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                throw new Exception(ex.Message);
            }
        }
    }
}
