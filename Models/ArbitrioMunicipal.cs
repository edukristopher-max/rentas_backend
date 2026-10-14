using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ArbitrioMunicipal
    {
        public int Id { get; set; }
        public int PredioId { get; set; }
        public int AnioFiscal { get; set; }
        public string Periodo { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] public decimal MontoLimpieza { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal MontoParques { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal MontoSerenazgo { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal MontoTotal { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
        public DateTime? FechaPago { get; set; }

        [JsonIgnore]
        public Predio? Predio { get; set; }

        public Obligacion? ObligacionGenerada { get; set; }
    }
}
