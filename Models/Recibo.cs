using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Recibo
    {
        public int Id { get; set; }
        public int PagoId { get; set; }
        [Required, MaxLength(50)] public string NumeroComprobante { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        [MaxLength(50)] public string Estado { get; set; } = "Emitido";
        
        public Pago? Pago { get; set; }
    }
}
