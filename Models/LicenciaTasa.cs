using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class LicenciaTasa
    {
        public int Id { get; set; }
        public int ContribuyenteId { get; set; }
        public int? PredioId { get; set; }
        public string Tipo { get; set; } = string.Empty; // "Licencia de Funcionamiento", "Licencia de Construcción", "Certificado ITSE", "Tasa Administrativa"
        public string NumeroExpediente { get; set; } = string.Empty;
        public string Concepto { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] public decimal Monto { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; } = "En Trámite"; // "En Trámite", "Activa", "Vencida", "Revocada"

        [JsonIgnore]
        public Contribuyente? Contribuyente { get; set; }
        
        [JsonIgnore]
        public Predio? Predio { get; set; }

        public Obligacion? ObligacionGenerada { get; set; }
    }
}
