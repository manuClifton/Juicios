using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infrastructure
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SQLServerContext).Assembly);
        }
        public DbSet<NotificacionAdjuntoEnviado> NotificacionAdjuntoEnviado { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<VistaNotificacion> VistaNotificaciones { get; set; }
        public DbSet<VistaNotificacionPDF> VistaNotificacionesPDF { get; set; }
        public DbSet<vNotificacionPDFEmbargoCuentaSOJ> VNotificacionPDFEmbargoCuentaSOJ { get; set; }
        public DbSet<VistaNotificacionArchivoTemporalMetadata> VistaNotificacionArchivoTemporalesMetadata { get; set; }
        public DbSet<NotificacionArchivoTemporal> NotificacionArchivosTemporales { get; set; }
        public DbSet<NotificacionArchivoTemporalRelacion> NotificacionArchivosTemporalesRelacion { get; set; }
        public DbSet<VNotificacionArchivoEnvioWorker> VNotificacionArchivoEnvioWorker { get; set; }
        public DbSet<vNotificacionesAConfirmar> VNotificacionesAConfirmar { get; set; }
        public DbSet<VNotificacionDatosParaGenerarCedula> VNotificacionDatosParaGenerarCedula { get; set; }
        public DbSet<VNotificacionConstancia> VNotificacionConstancia { get; set; }

    }
}