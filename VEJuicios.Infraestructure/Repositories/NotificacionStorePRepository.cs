using Microsoft.EntityFrameworkCore;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Infrastructure;
using Microsoft.Data.SqlClient;
using VEJuicios.Domain;
using Microsoft.Extensions.Logging;

namespace VEJuicios.Infraestructure.Repositories
{
    public class NotificacionStorePRepository : INotificacionStoreRepository
    {
        private readonly DbContextOptions<SQLServerContext> _options;
        private readonly ILogger<NotificacionStorePRepository> _logger;

        public NotificacionStorePRepository(DbContextOptions<SQLServerContext> options, 
                                            ILogger<NotificacionStorePRepository> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task EnvioAfip(int notificacionId, Guid idAfip = new Guid(), string error = "")
        {
            
            try
            {
                using (var dbContext = new SQLServerContext(_options))
                {
                    var notificacionIdParam = new SqlParameter("@NotificacionId", notificacionId);
                    var afipIdParam = new SqlParameter("@AfipId", idAfip);
                    var errorApi = new SqlParameter("@Error", error);

                    await dbContext.Database.ExecuteSqlRawAsync("xmlNotificacionEnviarAfip @NotificacionId, @AfipId, @Error",
                        notificacionIdParam, afipIdParam, errorApi);

                    _logger.LogInformation("EXEC xmlNotificacionEnviarAfip {0},{1},{2}", notificacionIdParam.Value, afipIdParam.Value, errorApi.Value);
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Se genero un error en el servicio de Envio de Notificaciones. El error es: " + ex.Message);
            }
        }

        public async Task GenerarLog(string log)
        {

            try
            {
                using (var dbContext = new SQLServerContext(_options))
                {
                    var logParam = new SqlParameter("@LogText", log);

                    await dbContext.Database.ExecuteSqlRawAsync("xmlNotificacionLog @LogText", logParam);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se genero un error en el servicio de Envio de Notificaciones. El error es: " + ex.Message);
            }
        }

        public async Task RecepcionAfip(int notificacionId, EnumEstadoNotificaciones estadoId, DateTime? fechaEnvio = null, DateTime? fechaRecepcionAfip = null, DateTime? fechaNotificacion = null, DateTime? fechaCancelacion = null)
        {

            try
            {
                using (var dbContext = new SQLServerContext(_options))
                {
                    var notificacionIdParam = new SqlParameter("@NotificacionId", notificacionId);
                    var estadoIdParam = new SqlParameter("@EstadoId", estadoId);
                    var fechaEnvioParam = new SqlParameter("@FechaEnvio", fechaEnvio.HasValue? fechaEnvio : DBNull.Value);
                    var fechaRecepcionAfipParam = new SqlParameter("@FechaRecepcionAfip", fechaRecepcionAfip.HasValue ? fechaRecepcionAfip : DBNull.Value);
                    var fechaNotificacionParam = new SqlParameter("@FechaNotificacion", fechaNotificacion.HasValue ? fechaNotificacion : DBNull.Value);
                    var fechaCancelacionParam = new SqlParameter("@FechaCancelacion", fechaCancelacion.HasValue ? fechaCancelacion : DBNull.Value);

                    await dbContext.Database.ExecuteSqlRawAsync("xmlNotificacionRecibirAfip @NotificacionId, @EstadoId, @FechaEnvio, @FechaRecepcionAfip, @FechaNotificacion, @FechaCancelacion",
                        notificacionIdParam, estadoIdParam, fechaEnvioParam, fechaRecepcionAfipParam, fechaNotificacionParam, fechaCancelacionParam);

                    _logger.LogInformation("EXEC xmlNotificacionRecibirAfip {0},{1},{2},{3},{4},{5}", notificacionIdParam.Value, estadoIdParam.Value, fechaEnvioParam.Value, fechaRecepcionAfipParam.Value, fechaNotificacionParam.Value, fechaCancelacionParam.Value);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se genero un error en el servicio de Envio de Notificaciones. El error es: " + ex.Message);
            }
        }

        public Task BorrarArchivos()
        {
            using (var dbContext = new SQLServerContext(_options))
            {
                dbContext.Database.ExecuteSqlRaw("spNotificacionBorrarArchivosTemporales");
                _logger.LogInformation("EXEC spNotificacionBorrarArchivosTemporales");
            }
            return Task.CompletedTask;
        }
    }
}
