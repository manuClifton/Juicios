using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionAltaStorePRepository : INotificacionAltaStorePRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;

        public NotificacionAltaStorePRepository(DbContextOptions<SQLServerContext> options)
        {
            _options = options;
        }

        public async Task<int> AltaNotificacion(string xmlData, byte[] archivoData = null)
        {
            try
            {
                int notificacionId;
                using (var dbContext = new SQLServerContext(_options))
                {
                    var xmlParameter = new SqlParameter("@Xml", SqlDbType.Text) { Value = xmlData };
                    var nuevoIdParameter = new SqlParameter("@NuevoId", SqlDbType.Int);
                    //var archivoParameter = new SqlParameter("@ArchivoData", SqlDbType.Image) { Value = archivoData ?? (object)DBNull.Value };
                    nuevoIdParameter.Direction = ParameterDirection.Output;

                    var respuesta = await dbContext.Database.ExecuteSqlRawAsync("xmlNotificacion_Alta @Xml, @NuevoId OUTPUT", xmlParameter, nuevoIdParameter);
                    if (respuesta > 0)
                    {
                        notificacionId = (int)nuevoIdParameter.Value;
                        return notificacionId;
                    }
                    else 
                    {
                        return -1;
                    }
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
