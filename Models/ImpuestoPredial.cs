using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ImpuestoPredial
    {
        public int Id { get; set; }
        public int PredioId { get; set; }
        public int AnioFiscal { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal MontoBase { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal ImpuestoCalculado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
        public DateTime? FechaPago { get; set; }

        [JsonIgnore]
        public Predio? Predio { get; set; }

        public Obligacion? ObligacionGenerada { get; set; }
    }
}
