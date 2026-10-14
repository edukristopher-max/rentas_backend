using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Predio
    {
        public int Id { get; set; }
        public string CodigoCatastral { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")] public decimal AreaTerreno { get; set; }
        [Column(TypeName = "decimal(10,2)")] public decimal AreaConstruida { get; set; }
        public string UsoPredio { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] public decimal ValorArancelario { get; set; }
        public bool Estado { get; set; } = true;

        // Relación con Contribuyente
        public int ContribuyenteId { get; set; }
        
        [JsonIgnore]
        public Contribuyente? Contribuyente { get; set; }

        public ICollection<ImpuestoPredial>? ImpuestosPrediales { get; set; }
        public ICollection<ArbitrioMunicipal>? ArbitriosMunicipales { get; set; }
        public ICollection<LicenciaTasa>? LicenciasTasas { get; set; }

        public ICollection<PredioContribuyente> PrediosContribuyentes { get; set; } = new List<PredioContribuyente>();
    }
}
