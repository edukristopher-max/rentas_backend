using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Renta> Rentas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contribuyente> Contribuyentes { get; set; }
        public DbSet<Predio> Predios { get; set; }
        public DbSet<PredioContribuyente> PrediosContribuyentes { get; set; }
        public DbSet<ImpuestoPredial> ImpuestosPrediales { get; set; }
        public DbSet<ArbitrioMunicipal> ArbitriosMunicipales { get; set; }
        public DbSet<Deuda> Deudas { get; set; } // Obsoleto, mantenido por compatibilidad
        public DbSet<Obligacion> Obligaciones { get; set; }
        public DbSet<ObligacionDetalle> ObligacionesDetalles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PagoDetalle> PagoDetalles { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<LicenciaTasa> LicenciasTasas { get; set; }
        public DbSet<CajaApertura> CajasAperturas { get; set; }
        public DbSet<CajaMovimiento> CajasMovimientos { get; set; }
        public DbSet<CajaCierre> CajasCierres { get; set; }
        public DbSet<ReporteGenerado> ReportesGenerados { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<ActividadReciente> ActividadesRecientes { get; set; }
        public DbSet<ConfiguracionSistema> ConfiguracionSistema { get; set; }
        public DbSet<AuditoriaLog> AuditoriaLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // CONFIGURACIÓN DE SECUENCIAS
            // ==========================================
            modelBuilder.HasSequence<int>("Seq_NumeroRecibo")
                .StartsAt(10000)
                .IncrementsBy(1);

            // ==========================================
            // CONFIGURACIÓN DE PRECISIÓN DECIMAL
            // ==========================================
            // Los campos decimales ya tienen anotaciones [Column(TypeName="decimal(18,2)")]
            // PredioContribuyente.PorcentajeParticipacion usa "decimal(5,2)"

            // ==========================================
            // RELACIONES Y RESTRICCIONES (DELETE BEHAVIOR)
            // ==========================================

            modelBuilder.Entity<Obligacion>()
                .HasOne(o => o.ImpuestoPredial)
                .WithOne(ip => ip.ObligacionGenerada)
                .HasForeignKey<Obligacion>(o => o.ImpuestoPredialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Obligacion>()
                .HasOne(o => o.ArbitrioMunicipal)
                .WithOne(am => am.ObligacionGenerada)
                .HasForeignKey<Obligacion>(o => o.ArbitrioMunicipalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Obligacion>()
                .HasOne(o => o.LicenciaTasa)
                .WithOne(lt => lt.ObligacionGenerada)
                .HasForeignKey<Obligacion>(o => o.LicenciaTasaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Obligacion>()
                .HasOne(o => o.Contribuyente)
                .WithMany(c => c.Obligaciones)
                .HasForeignKey(o => o.ContribuyenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Obligacion>()
                .HasOne(o => o.Predio)
                .WithMany()
                .HasForeignKey(o => o.PredioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ObligacionDetalle>()
                .HasOne(od => od.Obligacion)
                .WithMany(o => o.Detalles)
                .HasForeignKey(od => od.ObligacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Deuda)
                .WithMany(d => d.Pagos)
                .HasForeignKey(p => p.DeudaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PagoDetalle>()
                .HasOne(pd => pd.Pago)
                .WithMany(p => p.PagoDetalles)
                .HasForeignKey(pd => pd.PagoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PagoDetalle>()
                .HasOne(pd => pd.Obligacion)
                .WithMany(o => o.PagosRecibidos)
                .HasForeignKey(pd => pd.ObligacionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recibo>()
                .HasOne(r => r.Pago)
                .WithOne(p => p.Recibo)
                .HasForeignKey<Recibo>(r => r.PagoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CajaApertura>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CajaCierre>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioResponsableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CajaMovimiento>()
                .HasOne(cm => cm.Apertura)
                .WithMany(ca => ca.Movimientos)
                .HasForeignKey(cm => cm.CajaAperturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CajaMovimiento>()
                .HasOne(cm => cm.Pago)
                .WithOne(p => p.MovimientoCaja)
                .HasForeignKey<CajaMovimiento>(cm => cm.PagoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CajaCierre>()
                .HasOne(cc => cc.Apertura)
                .WithOne(ca => ca.Cierre)
                .HasForeignKey<CajaCierre>(cc => cc.CajaAperturaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PredioContribuyente>()
                .HasOne(pc => pc.Predio)
                .WithMany(p => p.PrediosContribuyentes)
                .HasForeignKey(pc => pc.PredioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PredioContribuyente>()
                .HasOne(pc => pc.Contribuyente)
                .WithMany(c => c.PrediosContribuyentes)
                .HasForeignKey(pc => pc.ContribuyenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Contribuyente)
                .WithMany(c => c.Notificaciones)
                .HasForeignKey(n => n.ContribuyenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>().HasOne(n => n.Obligacion).WithMany().HasForeignKey(n => n.ObligacionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notificacion>().HasOne(n => n.Pago).WithMany().HasForeignKey(n => n.PagoId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notificacion>().HasOne(n => n.LicenciaTasa).WithMany().HasForeignKey(n => n.LicenciaTasaId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notificacion>().HasOne(n => n.Predio).WithMany().HasForeignKey(n => n.PredioId).OnDelete(DeleteBehavior.Restrict);

            // Fase 1 Entities explicit restrict
            modelBuilder.Entity<ImpuestoPredial>().HasOne(ip => ip.Predio).WithMany(p => p.ImpuestosPrediales).HasForeignKey(ip => ip.PredioId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ArbitrioMunicipal>().HasOne(am => am.Predio).WithMany(p => p.ArbitriosMunicipales).HasForeignKey(am => am.PredioId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Deuda>().HasOne(d => d.Contribuyente).WithMany(c => c.Deudas).HasForeignKey(d => d.ContribuyenteId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Deuda>().HasOne(d => d.Predio).WithMany().HasForeignKey(d => d.PredioId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LicenciaTasa>().HasOne(lt => lt.Contribuyente).WithMany(c => c.LicenciasTasas).HasForeignKey(lt => lt.ContribuyenteId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LicenciaTasa>().HasOne(lt => lt.Predio).WithMany(p => p.LicenciasTasas).HasForeignKey(lt => lt.PredioId).OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // ÍNDICES Y CONCURRENCIA
            // ==========================================

            modelBuilder.Entity<Recibo>()
                .HasIndex(r => r.NumeroComprobante)
                .IsUnique();

            modelBuilder.Entity<CajaApertura>()
                .HasIndex(c => c.UsuarioId)
                .HasFilter("[Estado] = N'Abierta'")
                .IsUnique();

            modelBuilder.Entity<Obligacion>()
                .Property(o => o.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Obligacion>().HasIndex(o => o.ContribuyenteId);
            modelBuilder.Entity<Obligacion>().HasIndex(o => o.PredioId);
            modelBuilder.Entity<Obligacion>().HasIndex(o => o.FechaVencimiento);
            modelBuilder.Entity<Obligacion>().HasIndex(o => o.Estado);
            modelBuilder.Entity<PagoDetalle>().HasIndex(pd => pd.ObligacionId);
            modelBuilder.Entity<PredioContribuyente>().HasIndex(pc => new { pc.PredioId, pc.ContribuyenteId });

            // ==========================================
            // CONSTRAINTS SQL PERSONALIZADOS (CHECK)
            // ==========================================

            modelBuilder.Entity<Obligacion>().ToTable(t => t.HasCheckConstraint("CK_Obligacion_OrigenUnico_O_Historico", 
                @"(EsHistoricoMigrado = 0 AND (
                    (ImpuestoPredialId IS NOT NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NULL) OR
                    (ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NOT NULL AND LicenciaTasaId IS NULL) OR
                    (ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NOT NULL)
                ))
                OR
                (EsHistoricoMigrado = 1 AND ImpuestoPredialId IS NULL AND ArbitrioMunicipalId IS NULL AND LicenciaTasaId IS NULL)"
            ));

            // Seed inicial de configuración
            modelBuilder.Entity<ConfiguracionSistema>().HasData(
                new ConfiguracionSistema
                {
                    Id = 1,
                    NombreMunicipalidad = "Municipalidad Distrital",
                    Ruc = "20123456789",
                    Direccion = "Av. Principal 123",
                    Telefono = "(01) 555-1234",
                    EmailContacto = "contacto@municipalidad.gob.pe",
                    TasaInteresMoratorio = 1.20m, 
                    UITActual = 5150.00m, 
                    Moneda = "PEN"
                }
            );
        }
    }
}
