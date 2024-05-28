using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Infractores;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;

namespace VEJuicios.Infraestructure.Repositories
{
    public class InfractorAltaStorePRepository : IInfractorAltaStorePRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;

        public InfractorAltaStorePRepository(DbContextOptions<SQLServerContext> options)
        {
            _options = options;
        }

        public async Task<int> AltaInfractor(string xmlData)
        {
            try
            {
                int infractorId;
                using (var dbContext = new SQLServerContext(_options))
                {
                    var xmlParameter = new SqlParameter("@Xml", SqlDbType.Text) { Value = xmlData };
                    var nuevoIdParameter = new SqlParameter("@NuevoId", SqlDbType.Int);
                    nuevoIdParameter.Direction = ParameterDirection.Output;

                    var respuesta = await dbContext.Database.ExecuteSqlRawAsync("xmlPersona_Alta @Xml, @NuevoId OUTPUT", xmlParameter, nuevoIdParameter);
                    if (respuesta > 0)
                    {
                        infractorId = (int)nuevoIdParameter.Value;
                        return infractorId;
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
