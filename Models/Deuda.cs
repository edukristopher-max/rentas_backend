using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Deuda
    {
        public int Id { get; set; }
        public int ContribuyenteId { get; set; }
        public int? PredioId { get; set; } // Opcional, por si la deuda es genérica (multa) o atada a un predio
        public string Concepto { get; set; } = string.Empty;
        public string TipoDeuda { get; set; } = string.Empty; // "Impuesto", "Arbitrio", "Multa"
        
        [Column(TypeName = "decimal(18,2)")] public decimal MontoOriginal { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal Mora { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal Descuento { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal TotalPagar { get; set; }

        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Pendiente"; // "Pendiente", "Vencida", "Pagada", "Fraccionada"

        [JsonIgnore]
        public Contribuyente? Contribuyente { get; set; }
        
        [JsonIgnore]
        public Predio? Predio { get; set; }

        public ICollection<Pago>? Pagos { get; set; }
    }
}
