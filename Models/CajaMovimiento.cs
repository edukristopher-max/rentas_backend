using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CajaMovimiento
    {
        public int Id { get; set; }
        public int CajaAperturaId { get; set; }
        public int? PagoId { get; set; } 
        
        [Required, MaxLength(50)] public string Tipo { get; set; } = "Ingreso"; 
        [Column(TypeName = "decimal(18,2)")] public decimal Monto { get; set; } 
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;
        [MaxLength(50)] public string MetodoPago { get; set; } = string.Empty;
        
        public CajaApertura? Apertura { get; set; }
        public Pago? Pago { get; set; }
    }
}
