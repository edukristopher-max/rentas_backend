using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Obligacion
    {
        public int Id { get; set; }
        public int ContribuyenteId { get; set; }
        public int? PredioId { get; set; } 
        
        public int? ImpuestoPredialId { get; set; }
        public int? ArbitrioMunicipalId { get; set; }
        public int? LicenciaTasaId { get; set; }

        [MaxLength(100)] public string OrigenReferencia { get; set; } = string.Empty; 
        [MaxLength(50)] public string Periodo { get; set; } = string.Empty; 
        
        [Column(TypeName = "decimal(18,2)")] public decimal Total { get; set; } 
        [Column(TypeName = "decimal(18,2)")] public decimal Pagado { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal Saldo { get; set; }

        public DateTime? FechaVencimiento { get; set; }
        [MaxLength(50)] public string Estado { get; set; } = "Pendiente"; 
        
        public bool EsHistoricoMigrado { get; set; } = false;

        [Timestamp] public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        public Contribuyente? Contribuyente { get; set; }
        public Predio? Predio { get; set; }
        public ImpuestoPredial? ImpuestoPredial { get; set; }
        public ArbitrioMunicipal? ArbitrioMunicipal { get; set; }
        public LicenciaTasa? LicenciaTasa { get; set; }

        public ICollection<ObligacionDetalle> Detalles { get; set; } = new List<ObligacionDetalle>();
        public ICollection<PagoDetalle> PagosRecibidos { get; set; } = new List<PagoDetalle>();
    }
}
