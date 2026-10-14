using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ObligacionDetalle
    {
        public int Id { get; set; }
        public int ObligacionId { get; set; }
        [Required, MaxLength(255)] public string Concepto { get; set; } = string.Empty; 
        [Column(TypeName = "decimal(18,2)")] public decimal Monto { get; set; }

        public Obligacion? Obligacion { get; set; }
    }
}
