using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class VE_AfipContext : DbContext
    {
        public VE_AfipContext()
        {
        }

        public VE_AfipContext(DbContextOptions<VE_AfipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adjuntos> Adjuntos { get; set; }
        public virtual DbSet<LoginTickets> LoginTickets { get; set; }
        public virtual DbSet<NotificacionAdjuntos> NotificacionAdjuntos { get; set; }
        public virtual DbSet<Notificaciones> Notificaciones { get; set; }
        public virtual DbSet<Notificaciones1> Notificaciones1 { get; set; }
        public virtual DbSet<Sistemas> Sistemas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=S1-DIXX-SQL10;Database=VE_Afip;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjuntos>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Metadata)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginTickets>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ExpirationTime).HasColumnType("datetime");

                entity.Property(e => e.GenerationTime).HasColumnType("datetime");

                entity.Property(e => e.Response).IsUnicode(false);

                entity.Property(e => e.Service)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Sign).IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);
            });

            modelBuilder.Entity<NotificacionAdjuntos>(entity =>
            {
                entity.HasKey(e => new { e.NotificacionId, e.AdjuntoId });

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("Fecha_Creacion")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Notificaciones>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("_Notificaciones");

                entity.Property(e => e.CommId).HasColumnName("CommID");

                entity.Property(e => e.Cuit)
                    .IsRequired()
                    .HasColumnName("CUIT")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEnvio).HasColumnType("datetime");

                entity.Property(e => e.Mensaje).IsUnicode(false);

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Notificaciones1>(entity =>
            {
                entity.ToTable("Notificaciones");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AfipObservacion).IsUnicode(false);

                entity.Property(e => e.Asunto)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CommId).HasColumnName("CommID");

                entity.Property(e => e.Cuit)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("Fecha_Creacion")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaEnvio)
                    .HasColumnName("Fecha_Envio")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaNotificacion)
                    .HasColumnName("Fecha_Notificacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Metadata)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Ref1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ref2)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sistemas>(entity =>
            {
                entity.HasKey(e => e.AfipSistemaId)
                    .HasName("PK_Sistemas_1");

                entity.Property(e => e.AfipSistemaId).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
