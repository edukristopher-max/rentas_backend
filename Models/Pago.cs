using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int? DeudaId { get; set; }
        public string NumeroRecibo { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = string.Empty; // "Efectivo", "Tarjeta de Crédito", "Transferencia", "Yape/Plin"
        public string Cajero { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        [MaxLength(50)] public string Estado { get; set; } = "Vigente";
        public bool EsHistoricoMigrado { get; set; } = false;

        [JsonIgnore]
        public Deuda? Deuda { get; set; }

        public Recibo? Recibo { get; set; }
        public CajaMovimiento? MovimientoCaja { get; set; }
        public ICollection<PagoDetalle> PagoDetalles { get; set; } = new List<PagoDetalle>();
    }
}
